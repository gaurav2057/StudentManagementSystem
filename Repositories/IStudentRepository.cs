using StudentManagement.Models;

namespace StudentManagement.Repositories;

public interface IStudentRepository
{
    IEnumerable<Student> GetAllStudents();

    IEnumerable<Student> SearchStudents(string searchTerm);

    Student? GetStudentById(int id);

    void AddStudent(Student student);

    void UpdateStudent(Student student);

    void DeleteStudent(int id);

    DashboardViewModel GetDashboardData();
}