using GraduationIdeasRegistration.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public enum IdeaState
    {
        Pending,
        Reviewed
    }

    public class StudentIdea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdeaID { get; set; }
        [Required]
        [MinLength(5),MaxLength(50)]
        public string IdeaName { get; set; }
        [Required]
        public IdeaState IdeaState { get; set; }
        [Required]
        public string IdeaDescription { get; set; }
        public string TeamID { get; set; }
        public List<Professor> Professors { get; set; }
        public Team Team { get; set; }
    }

    public class TempIdea
    {
        public string TeamID { get; set; }
        public string IdeaName { get; set; }
        public string IdeaDescription { get; set; }
        public string[] Professors { get; set; }

    }

}