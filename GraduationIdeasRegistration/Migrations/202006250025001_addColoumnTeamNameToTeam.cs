namespace GraduationIdeasRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColoumnTeamNameToTeam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "TeamName", c => c.String());
            DropColumn("dbo.Teams", "StudentIdeaID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "StudentIdeaID", c => c.Int(nullable: false));
            DropColumn("dbo.Teams", "TeamName");
        }
    }
}
