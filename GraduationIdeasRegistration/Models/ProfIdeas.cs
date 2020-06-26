using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public class ProfIdeas
    {
        public int IdeaID { get; set; }
        public string ProfID { get; set; }
        public Professor Professor { get; set; }
    }
}