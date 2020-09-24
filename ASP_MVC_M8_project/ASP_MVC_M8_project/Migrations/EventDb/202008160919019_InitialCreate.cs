namespace ASP_MVC_M8_project.Migrations.EventDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 300),
                        Mobile = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScheduleEvents",
                c => new
                    {
                        BookedEventId = c.Int(nullable: false, identity: true),
                        EventTypeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookedEventId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Event_Type", t => t.EventTypeId, cascadeDelete: true)
                .Index(t => t.EventTypeId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Event_Type",
                c => new
                    {
                        EventTypeId = c.Int(nullable: false, identity: true),
                        EventType = c.String(nullable: false, maxLength: 25),
                        EventTypeImage = c.String(),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        tblUserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.tblUserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ScheduleEvents", "EventTypeId", "dbo.Event_Type");
            DropForeignKey("dbo.ScheduleEvents", "CustomerId", "dbo.Customers");
            DropIndex("dbo.tblUsers", new[] { "RoleId" });
            DropIndex("dbo.ScheduleEvents", new[] { "CustomerId" });
            DropIndex("dbo.ScheduleEvents", new[] { "EventTypeId" });
            DropTable("dbo.tblUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Event_Type");
            DropTable("dbo.ScheduleEvents");
            DropTable("dbo.Customers");
        }
    }
}
