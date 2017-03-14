namespace LMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CurriculumRoot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurriculumUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurriculumId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curriculum", t => t.CurriculumId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CurriculumId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Curriculum",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        Recurrence_Type = c.String(maxLength: 100),
                        Recurrence_StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Recurrence_DaysAfterDueDateToRepeat = c.Int(nullable: false),
                        Recurrence_IsRecurring = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "DOB", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.User", "HireDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CurriculumUser", "UserId", "dbo.User");
            DropForeignKey("dbo.CurriculumUser", "CurriculumId", "dbo.Curriculum");
            DropIndex("dbo.CurriculumUser", new[] { "UserId" });
            DropIndex("dbo.CurriculumUser", new[] { "CurriculumId" });
            DropColumn("dbo.User", "HireDate");
            DropColumn("dbo.User", "DOB");
            DropTable("dbo.Curriculum");
            DropTable("dbo.CurriculumUser");
        }
    }
}
