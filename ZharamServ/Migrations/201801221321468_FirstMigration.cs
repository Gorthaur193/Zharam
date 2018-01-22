namespace ZharamServ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonalMessage",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        SenderId = c.Guid(nullable: false),
                        ReceiverId = c.Guid(nullable: false),
                        ContentJson = c.String(),
                        FixedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.User", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.SenderId, cascadeDelete: true)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Bio = c.String(),
                        Login = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PhoneConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LastActiveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomId = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomMessage",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                        ContentJson = c.String(),
                        FixedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.UserRoom",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoomId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalMessage", "SenderId", "dbo.User");
            DropForeignKey("dbo.PersonalMessage", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.UserRoom", "RoomId", "dbo.Room");
            DropForeignKey("dbo.UserRoom", "UserId", "dbo.User");
            DropForeignKey("dbo.RoomMessage", "UserId", "dbo.User");
            DropForeignKey("dbo.RoomMessage", "RoomId", "dbo.Room");
            DropIndex("dbo.UserRoom", new[] { "RoomId" });
            DropIndex("dbo.UserRoom", new[] { "UserId" });
            DropIndex("dbo.RoomMessage", new[] { "RoomId" });
            DropIndex("dbo.RoomMessage", new[] { "UserId" });
            DropIndex("dbo.PersonalMessage", new[] { "ReceiverId" });
            DropIndex("dbo.PersonalMessage", new[] { "SenderId" });
            DropTable("dbo.UserRoom");
            DropTable("dbo.RoomMessage");
            DropTable("dbo.Room");
            DropTable("dbo.User");
            DropTable("dbo.PersonalMessage");
        }
    }
}
