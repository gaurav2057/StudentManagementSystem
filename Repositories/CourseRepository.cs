using Dapper;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly DapperContext _context;

    public CourseRepository(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<Course> GetAllCourses()
    {
        using var connection = _context.CreateConnection();

        return connection.Query<Course>(
            @"SELECT *
              FROM Courses
              ORDER BY CourseName");
    }

    public Course? GetCourseById(int id)
    {
        using var connection = _context.CreateConnection();

        return connection.QuerySingleOrDefault<Course>(
            @"SELECT *
              FROM Courses
              WHERE CourseId=@Id",
            new { Id = id });
    }

    public Course? GetCourseDetails(int id)
    {
        using var connection = _context.CreateConnection();

        string sql = @"

SELECT *
FROM Courses
WHERE CourseId=@Id;

SELECT
s.StudentId,
s.Name,
s.Email,
s.Age,
s.City,
s.CourseId,
c.CourseName
FROM Students s
INNER JOIN Courses c
ON s.CourseId=c.CourseId
WHERE s.CourseId=@Id;

SELECT
t.TeacherId,
t.Name,
t.Department,
t.Qualification,
t.Email,
t.PhotoUrl,
t.Experience,
t.About,
t.CourseId,
c.CourseName
FROM Teachers t
INNER JOIN Courses c
ON t.CourseId=c.CourseId
WHERE t.CourseId=@Id;

";

        using var multi = connection.QueryMultiple(sql, new { Id = id });

        var course = multi.Read<Course>().FirstOrDefault();

        if (course == null)
            return null;

        course.Students = multi.Read<Student>().ToList();

        course.Teachers = multi.Read<Teacher>().ToList();

        return course;
    }

    public void AddCourse(Course course)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"INSERT INTO Courses
            (CourseName,Department,Duration,Description)
            VALUES
            (@CourseName,@Department,@Duration,@Description)",
            course);
    }

    public void UpdateCourse(Course course)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"UPDATE Courses
              SET CourseName=@CourseName,
                  Department=@Department,
                  Duration=@Duration,
                  Description=@Description
              WHERE CourseId=@CourseId",
            course);
    }

    public void DeleteCourse(int id)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            "DELETE FROM Courses WHERE CourseId=@Id",
            new { Id = id });
    }
}