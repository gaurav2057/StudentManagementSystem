using Dapper;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly DapperContext _context;

    public StudentRepository(DapperContext context)
    {
        _context = context;
    }

    public IEnumerable<Student> GetAllStudents()
    {
        using var connection = _context.CreateConnection();

        return connection.Query<Student>(
            @"SELECT
                s.StudentId,
                s.Name,
                s.Email,
                s.Age,
                s.City,
                s.CourseId,
                c.CourseName
              FROM Students s
              INNER JOIN Courses c
              ON s.CourseId = c.CourseId");
    }

    public IEnumerable<Student> SearchStudents(string searchTerm)
    {
        using var connection = _context.CreateConnection();

        return connection.Query<Student>(
            @"SELECT
                s.StudentId,
                s.Name,
                s.Email,
                s.Age,
                s.City,
                s.CourseId,
                c.CourseName
              FROM Students s
              INNER JOIN Courses c
              ON s.CourseId = c.CourseId
              WHERE s.Name LIKE @Search
                 OR s.Email LIKE @Search
                 OR c.CourseName LIKE @Search
                 OR s.City LIKE @Search",
            new
            {
                Search = $"%{searchTerm}%"
            });
    }

    public Student? GetStudentById(int id)
    {
        using var connection = _context.CreateConnection();

        return connection.QuerySingleOrDefault<Student>(
            @"SELECT
                s.StudentId,
                s.Name,
                s.Email,
                s.Age,
                s.City,
                s.CourseId,
                c.CourseName
              FROM Students s
              INNER JOIN Courses c
              ON s.CourseId = c.CourseId
              WHERE s.StudentId=@Id",
            new { Id = id });
    }

    public void AddStudent(Student student)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"INSERT INTO Students
            (Name,Email,Age,City,CourseId)
            VALUES
            (@Name,@Email,@Age,@City,@CourseId)",
            student);
    }

    public void UpdateStudent(Student student)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"UPDATE Students
              SET Name=@Name,
                  Email=@Email,
                  Age=@Age,
                  City=@City,
                  CourseId=@CourseId
              WHERE StudentId=@StudentId",
            student);
    }

    public void DeleteStudent(int id)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            "DELETE FROM Students WHERE StudentId=@Id",
            new { Id = id });
    }

    public DashboardViewModel GetDashboardData()
    {
        using var connection = _context.CreateConnection();

        return new DashboardViewModel
        {
            TotalStudents =
                connection.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM Students"),

            TotalCourses =
                connection.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM Courses"),

            TotalCities =
                connection.ExecuteScalar<int>(
                    "SELECT COUNT(DISTINCT City) FROM Students")
        };
    }
}