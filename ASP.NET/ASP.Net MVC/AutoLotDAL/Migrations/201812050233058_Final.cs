namespace AutoLotDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "CustId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");

            RenameColumn(table: "dbo.Orders", name: "CustId", newName: "CustomerId");

            RenameIndex(table: "dbo.Orders", name: "IX_CustId", newName: "IX_CustomerId");

            DropPrimaryKey("dbo.CreditRisks");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Inventory");

            DropColumn("dbo.CreditRisks", "CustId");
            DropColumn("dbo.Customers", "CustId");
            DropColumn("dbo.Orders", "OrderId");
            DropColumn("dbo.Inventory", "CarId");

            AddColumn("dbo.CreditRisks", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Inventory", "Id", c => c.Int(nullable: false, identity: true));

            //AlterColumn("dbo.CreditRisks", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            //AlterColumn("dbo.Customers", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            //AlterColumn("dbo.Orders", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            //AlterColumn("dbo.Inventory", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));

            AddPrimaryKey("dbo.CreditRisks", "Id");
            AddPrimaryKey("dbo.Customers", "Id");
            AddPrimaryKey("dbo.Orders", "Id");
            AddPrimaryKey("dbo.Inventory", "Id");

            CreateIndex("dbo.CreditRisks", new[] { "LastName", "FirstName" }, unique: true, name: "IDX_CreditRisk_Name");

            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "Id");
            //DropColumn("dbo.CreditRisks", "CustId");
            //DropColumn("dbo.Customers", "CustId");
            //DropColumn("dbo.Orders", "OrderId");
            //DropColumn("dbo.Inventory", "CarId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CarId", "dbo.Inventory");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");

            DropPrimaryKey("dbo.Inventory");
            DropPrimaryKey("dbo.Orders");
            DropPrimaryKey("dbo.Customers");
            DropPrimaryKey("dbo.CreditRisks");

            DropColumn("dbo.Inventory", "Id");
            DropColumn("dbo.Orders", "Id");
            DropColumn("dbo.Customers", "Id");
            DropColumn("dbo.CreditRisks", "Id");

            DropIndex("dbo.CreditRisks", "IDX_CreditRisk_Name");

            AddColumn("dbo.Inventory", "CarId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Orders", "OrderId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Customers", "CustId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CreditRisks", "CustId", c => c.Int(nullable: false, identity: true));

            //AlterColumn("dbo.Inventory", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"));
            //AlterColumn("dbo.Orders", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"));
            //AlterColumn("dbo.Customers", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"));
            //AlterColumn("dbo.CreditRisks", "Timestamp", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "timestamp"));

            //DropColumn("dbo.Inventory", "Id");
            //DropColumn("dbo.Orders", "Id");
            //DropColumn("dbo.Customers", "Id");
            //DropColumn("dbo.CreditRisks", "Id");

            AddPrimaryKey("dbo.Inventory", "CarId");
            AddPrimaryKey("dbo.Orders", "OrderId");
            AddPrimaryKey("dbo.Customers", "CustId");
            AddPrimaryKey("dbo.CreditRisks", "CustId");

            RenameIndex(table: "dbo.Orders", name: "IX_CustomerId", newName: "IX_CustId");

            RenameColumn(table: "dbo.Orders", name: "CustomerId", newName: "CustId");

            AddForeignKey("dbo.Orders", "CarId", "dbo.Inventory", "CarId");
            AddForeignKey("dbo.Orders", "CustId", "dbo.Customers", "CustId");
        }
    }
}
