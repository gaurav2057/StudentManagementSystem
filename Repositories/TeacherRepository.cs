using Dapper;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly DapperContext _context;

    public TeacherRepository(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<Teacher> GetAllTeachers()
    {
        using var connection = _context.CreateConnection();

        return connection.Query<Teacher>(
            @"SELECT
                t.*,
                c.CourseName
              FROM Teachers t
              INNER JOIN Courses c
              ON t.CourseId=c.CourseId");
    }

    public Teacher? GetTeacherById(int id)
    {
        using var connection = _context.CreateConnection();

        return connection.QuerySingleOrDefault<Teacher>(
            @"SELECT
                t.*,
                c.CourseName
              FROM Teachers t
              INNER JOIN Courses c
              ON t.CourseId=c.CourseId
              WHERE TeacherId=@Id",
            new { Id = id });
    }

    public void AddTeacher(Teacher teacher)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"INSERT INTO Teachers
            (Name,Department,Qualification,Email,CourseId)
            VALUES
            (@Name,@Department,@Qualification,@Email,@CourseId)",
            teacher);
    }

    public void UpdateTeacher(Teacher teacher)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"UPDATE Teachers
              SET Name=@Name,
                  Department=@Department,
                  Qualification=@Qualification,
                  Email=@Email,
                  CourseId=@CourseId
              WHERE TeacherId=@TeacherId",
            teacher);
    }

    public void DeleteTeacher(int id)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            "DELETE FROM Teachers WHERE TeacherId=@Id",
            new { Id = id });
    }
}