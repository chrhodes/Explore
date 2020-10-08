namespace MvcSalesApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnknownChange : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CustFromIEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustFromIEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
