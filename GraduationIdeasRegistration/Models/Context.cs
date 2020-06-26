using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GraduationIdeasRegistration.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static Context Create()
        {
            return new Context();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(s => s.Students)
                .WithRequired(s => s.Team)
                .HasForeignKey(s => s.TeamID);

            modelBuilder.Entity<Team>()
                .HasMany(s => s.StudentIdea)
                .WithRequired(s => s.Team)
                .HasForeignKey(s => s.TeamID);

            modelBuilder.Entity<Department>()
                .HasMany(s => s.Professors)
                .WithRequired(s => s.Department)
                .HasForeignKey(s => s.DeptID);

            modelBuilder.Entity<Professor>()
                .HasMany(s => s.StudentIdea)
                .WithMany(s => s.Professors)
                .Map(m =>
                {
                    m.ToTable("IdeasWithRegisteredProfessors");
                    m.MapLeftKey("ProfID");
                    m.MapRightKey("StudID");
                });

            modelBuilder.Entity<StudentIdea>()
                .ToTable("Ideas");

            modelBuilder.Entity<ProfIdeas>()
                .ToTable("ApprovedIdeas")
                .HasKey(s => new { s.ProfID, s.IdeaID })
                .HasRequired(s => s.Professor)
                .WithMany(s => s.ProfIdeas)
                .HasForeignKey(s => s.ProfID);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<StudentIdea> Ideas { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Professor> Professors { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<ProfIdeas> ProfIdeas { get; set; }
    }
}