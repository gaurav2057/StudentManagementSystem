using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Duration { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Navigation data (loaded manually with Dapper)
        public List<Student> Students { get; set; } = new();

        public List<Teacher> Teachers { get; set; } = new();
    }
}