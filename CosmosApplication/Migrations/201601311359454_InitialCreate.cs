namespace CosmosApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNo = c.String(),
                        Name = c.String(),
                        FatherName = c.String(),
                        FatherCNIC = c.String(),
                        RollNo = c.String(),
                        Gender = c.String(),
                        Religion = c.String(),
                        Dob = c.DateTime(nullable: false),
                        HomeNumber = c.String(),
                        FatherNo = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FingerCode = c.Binary(),
                        ClassSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassSections", t => t.ClassSection_Id)
                .Index(t => t.ClassSection_Id);
            
            CreateTable(
                "dbo.StudentGardians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GuardianId = c.Int(),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guardians", t => t.GuardianId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.GuardianId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Guardians",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CNIC = c.String(),
                        Relation = c.String(),
                        Gender = c.String(),
                        GuardianNo = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        FingerCode = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GuardianTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        GuardianId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guardians", t => t.GuardianId)
                .Index(t => t.GuardianId);
            
            CreateTable(
                "dbo.StudentTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        StudentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ClassSection_Id", "dbo.ClassSections");
            DropForeignKey("dbo.StudentTimes", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentGardians", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentGardians", "GuardianId", "dbo.Guardians");
            DropForeignKey("dbo.GuardianTimes", "GuardianId", "dbo.Guardians");
            DropForeignKey("dbo.ClassSections", "ClassId", "dbo.Classes");
            DropIndex("dbo.StudentTimes", new[] { "StudentId" });
            DropIndex("dbo.GuardianTimes", new[] { "GuardianId" });
            DropIndex("dbo.StudentGardians", new[] { "StudentId" });
            DropIndex("dbo.StudentGardians", new[] { "GuardianId" });
            DropIndex("dbo.Students", new[] { "ClassSection_Id" });
            DropIndex("dbo.ClassSections", new[] { "ClassId" });
            DropTable("dbo.StudentTimes");
            DropTable("dbo.GuardianTimes");
            DropTable("dbo.Guardians");
            DropTable("dbo.StudentGardians");
            DropTable("dbo.Students");
            DropTable("dbo.ClassSections");
            DropTable("dbo.Classes");
        }
    }
}
