namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clans", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Clans", "DateModified", c => c.DateTime());
            AddColumn("dbo.Ninjas", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Ninjas", "DateModified", c => c.DateTime());
            AddColumn("dbo.NinjaEquipments", "DateCreated", c => c.DateTime());
            AddColumn("dbo.NinjaEquipments", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NinjaEquipments", "DateModified");
            DropColumn("dbo.NinjaEquipments", "DateCreated");
            DropColumn("dbo.Ninjas", "DateModified");
            DropColumn("dbo.Ninjas", "DateCreated");
            DropColumn("dbo.Clans", "DateModified");
            DropColumn("dbo.Clans", "DateCreated");
        }
    }
}
