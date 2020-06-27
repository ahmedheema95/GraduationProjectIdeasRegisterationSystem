namespace GraduationIdeasRegistration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColNameInIdeaProfTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.IdeasWithRegisteredProfessors", name: "StudID", newName: "IdeaID");
            RenameIndex(table: "dbo.IdeasWithRegisteredProfessors", name: "IX_StudID", newName: "IX_IdeaID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.IdeasWithRegisteredProfessors", name: "IX_IdeaID", newName: "IX_StudID");
            RenameColumn(table: "dbo.IdeasWithRegisteredProfessors", name: "IdeaID", newName: "StudID");
        }
    }
}
