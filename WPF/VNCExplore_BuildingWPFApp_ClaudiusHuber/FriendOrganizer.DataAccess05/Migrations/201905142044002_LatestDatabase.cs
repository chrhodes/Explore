namespace FriendOrganizer.DataAccess05.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendPhoneNumber13",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        FriendId = c.Int(nullable: false),
                        Friend15_Id = c.Int(),
                        Friend19_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friend13", t => t.FriendId, cascadeDelete: true)
                .ForeignKey("dbo.Friend15", t => t.Friend15_Id)
                .ForeignKey("dbo.Friend19", t => t.Friend19_Id)
                .Index(t => t.FriendId)
                .Index(t => t.Friend15_Id)
                .Index(t => t.Friend19_Id);
            
            CreateTable(
                "dbo.Friend13",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FavoriteLanguageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.FavoriteLanguageId)
                .Index(t => t.FavoriteLanguageId);
            
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friend05",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FavoriteLanguageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.FavoriteLanguageId)
                .Index(t => t.FavoriteLanguageId);
            
            CreateTable(
                "dbo.Friend12",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FavoriteLanguageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.FavoriteLanguageId)
                .Index(t => t.FavoriteLanguageId);
            
            CreateTable(
                "dbo.Friend15",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FavoriteLanguageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.FavoriteLanguageId)
                .Index(t => t.FavoriteLanguageId);
            
            CreateTable(
                "dbo.Meeting15",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        Friend19_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friend19", t => t.Friend19_Id)
                .Index(t => t.Friend19_Id);
            
            CreateTable(
                "dbo.Friend19",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FavoriteLanguageId = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Meeting19_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProgrammingLanguage", t => t.FavoriteLanguageId)
                .ForeignKey("dbo.Meeting19", t => t.Meeting19_Id)
                .Index(t => t.FavoriteLanguageId)
                .Index(t => t.Meeting19_Id);
            
            CreateTable(
                "dbo.Meeting19",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Meeting15Friend15",
                c => new
                    {
                        Meeting15_Id = c.Int(nullable: false),
                        Friend15_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meeting15_Id, t.Friend15_Id })
                .ForeignKey("dbo.Meeting15", t => t.Meeting15_Id, cascadeDelete: true)
                .ForeignKey("dbo.Friend15", t => t.Friend15_Id, cascadeDelete: true)
                .Index(t => t.Meeting15_Id)
                .Index(t => t.Friend15_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend19", "Meeting19_Id", "dbo.Meeting19");
            DropForeignKey("dbo.FriendPhoneNumber13", "Friend19_Id", "dbo.Friend19");
            DropForeignKey("dbo.Meeting15", "Friend19_Id", "dbo.Friend19");
            DropForeignKey("dbo.Friend19", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropForeignKey("dbo.FriendPhoneNumber13", "Friend15_Id", "dbo.Friend15");
            DropForeignKey("dbo.Meeting15Friend15", "Friend15_Id", "dbo.Friend15");
            DropForeignKey("dbo.Meeting15Friend15", "Meeting15_Id", "dbo.Meeting15");
            DropForeignKey("dbo.Friend15", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropForeignKey("dbo.Friend12", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropForeignKey("dbo.Friend05", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropForeignKey("dbo.FriendPhoneNumber13", "FriendId", "dbo.Friend13");
            DropForeignKey("dbo.Friend13", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropIndex("dbo.Meeting15Friend15", new[] { "Friend15_Id" });
            DropIndex("dbo.Meeting15Friend15", new[] { "Meeting15_Id" });
            DropIndex("dbo.Friend19", new[] { "Meeting19_Id" });
            DropIndex("dbo.Friend19", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.Meeting15", new[] { "Friend19_Id" });
            DropIndex("dbo.Friend15", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.Friend12", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.Friend05", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.Friend13", new[] { "FavoriteLanguageId" });
            DropIndex("dbo.FriendPhoneNumber13", new[] { "Friend19_Id" });
            DropIndex("dbo.FriendPhoneNumber13", new[] { "Friend15_Id" });
            DropIndex("dbo.FriendPhoneNumber13", new[] { "FriendId" });
            DropTable("dbo.Meeting15Friend15");
            DropTable("dbo.Meeting19");
            DropTable("dbo.Friend19");
            DropTable("dbo.Meeting15");
            DropTable("dbo.Friend15");
            DropTable("dbo.Friend12");
            DropTable("dbo.Friend05");
            DropTable("dbo.ProgrammingLanguage");
            DropTable("dbo.Friend13");
            DropTable("dbo.FriendPhoneNumber13");
        }
    }
}
