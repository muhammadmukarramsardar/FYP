namespace CosmosApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBChange1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Students", name: "ClassSection_Id", newName: "SectionId");
            RenameIndex(table: "dbo.Students", name: "IX_ClassSection_Id", newName: "IX_SectionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Students", name: "IX_SectionId", newName: "IX_ClassSection_Id");
            RenameColumn(table: "dbo.Students", name: "SectionId", newName: "ClassSection_Id");
        }
    }
}
