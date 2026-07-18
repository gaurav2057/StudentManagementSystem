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
        return connection.Query<Student>("SELECT * FROM Students");
    }

    public IEnumerable<Student> SearchStudents(string searchTerm)
    {
        using var connection = _context.CreateConnection();

        return connection.Query<Student>(
            @"SELECT * FROM Students
              WHERE Name LIKE @Search
                 OR Email LIKE @Search
                 OR Course LIKE @Search
                 OR City LIKE @Search",
            new
            {
                Search = $"%{searchTerm}%"
            });
    }

    public Student? GetStudentById(int id)
    {
        using var connection = _context.CreateConnection();

        return connection.QuerySingleOrDefault<Student>(
            "SELECT * FROM Students WHERE StudentId = @Id",
            new { Id = id });
    }

    public void AddStudent(Student student)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"INSERT INTO Students (Name, Email, Age, Course, City)
              VALUES (@Name, @Email, @Age, @Course, @City)",
            student);
    }

    public void UpdateStudent(Student student)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            @"UPDATE Students
              SET Name = @Name,
                  Email = @Email,
                  Age = @Age,
                  Course = @Course,
                  City = @City
              WHERE StudentId = @StudentId",
            student);
    }

    public void DeleteStudent(int id)
    {
        using var connection = _context.CreateConnection();

        connection.Execute(
            "DELETE FROM Students WHERE StudentId = @Id",
            new { Id = id });
    }

    public DashboardViewModel GetDashboardData()
    {
        using var connection = _context.CreateConnection();

        return new DashboardViewModel
        {
            TotalStudents = connection.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM Students"),

            TotalCourses = connection.ExecuteScalar<int>(
                "SELECT COUNT(DISTINCT Course) FROM Students"),

            TotalCities = connection.ExecuteScalar<int>(
                "SELECT COUNT(DISTINCT City) FROM Students")
        };
    }
}