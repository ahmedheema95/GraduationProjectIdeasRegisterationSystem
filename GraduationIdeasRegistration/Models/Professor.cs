using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public class Professor
    {
        [Key]
        public string ProfID { get; set; }
        [Required]
        public string ProfName { get; set; }
        public List<StudentIdea> StudentIdea { get; set; }
        public List<ProfIdeas> ProfIdeas { get; set; }
        public int DeptID { get; set; }
        public Department Department { get; set; }
    }
}