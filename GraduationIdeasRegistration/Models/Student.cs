using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        [MinLength(4),MaxLength(15)]
        public string StudName { get; set; }
        [Required]
        public float GPA { get; set; }
        public byte[] Transcript { get; set; }
        public string TeamID { get; set; }
        public Team Team { get; set; }
    }
}