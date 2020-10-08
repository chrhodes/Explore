namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthandDeathDatesToNinja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ninjas", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ninjas", "DateOfDeath", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ninjas", "DateOfDeath");
            DropColumn("dbo.Ninjas", "DateOfBirth");
        }
    }
}
