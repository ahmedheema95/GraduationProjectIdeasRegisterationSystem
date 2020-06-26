using GraduationIdeasRegistration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public class Team
    {
        [Key]
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public List<Student> Students { get; set; }
        public List<StudentIdea> StudentIdea { get; set; }
    }
}