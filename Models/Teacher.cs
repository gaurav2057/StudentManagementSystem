using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Qualification { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string? PhotoUrl { get; set; }

        public int? Experience { get; set; }

        public string? About { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        // Filled from JOIN
        public string? CourseName { get; set; }
    }
}