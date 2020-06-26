namespace GraduationIdeasRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptID = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DeptID);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        ProfID = c.String(nullable: false, maxLength: 128),
                        ProfName = c.String(nullable: false),
                        DeptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfID)
                .ForeignKey("dbo.Departments", t => t.DeptID, cascadeDelete: true)
                .Index(t => t.DeptID);
            
            CreateTable(
                "dbo.ApprovedIdeas",
                c => new
                    {
                        ProfID = c.String(nullable: false, maxLength: 128),
                        IdeaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfID, t.IdeaID })
                .ForeignKey("dbo.Professors", t => t.ProfID, cascadeDelete: true)
                .Index(t => t.ProfID);
            
            CreateTable(
                "dbo.Ideas",
                c => new
                    {
                        IdeaID = c.Int(nullable: false, identity: true),
                        IdeaName = c.String(nullable: false, maxLength: 50),
                        IdeaState = c.Int(nullable: false),
                        IdeaDescription = c.String(nullable: false),
                        TeamID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdeaID)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.String(nullable: false, maxLength: 128),
                        StudentIdeaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeamID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.Int(nullable: false, identity: true),
                        StudName = c.String(nullable: false, maxLength: 15),
                        GPA = c.Single(nullable: false),
                        Transcript = c.Binary(),
                        TeamID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Teams", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.TeamID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdeasWithRegisteredProfessors",
                c => new
                    {
                        ProfID = c.String(nullable: false, maxLength: 128),
                        StudID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfID, t.StudID })
                .ForeignKey("dbo.Professors", t => t.ProfID, cascadeDelete: true)
                .ForeignKey("dbo.Ideas", t => t.StudID, cascadeDelete: true)
                .Index(t => t.ProfID)
                .Index(t => t.StudID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Professors", "DeptID", "dbo.Departments");
            DropForeignKey("dbo.IdeasWithRegisteredProfessors", "StudID", "dbo.Ideas");
            DropForeignKey("dbo.IdeasWithRegisteredProfessors", "ProfID", "dbo.Professors");
            DropForeignKey("dbo.Students", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Ideas", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.ApprovedIdeas", "ProfID", "dbo.Professors");
            DropIndex("dbo.IdeasWithRegisteredProfessors", new[] { "StudID" });
            DropIndex("dbo.IdeasWithRegisteredProfessors", new[] { "ProfID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Students", new[] { "TeamID" });
            DropIndex("dbo.Ideas", new[] { "TeamID" });
            DropIndex("dbo.ApprovedIdeas", new[] { "ProfID" });
            DropIndex("dbo.Professors", new[] { "DeptID" });
            DropTable("dbo.IdeasWithRegisteredProfessors");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.Teams");
            DropTable("dbo.Ideas");
            DropTable("dbo.ApprovedIdeas");
            DropTable("dbo.Professors");
            DropTable("dbo.Departments");
        }
    }
}
