namespace CRUDB32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelSupplierItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stock = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Supplier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_m_supplier", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.tb_m_supplier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_items", "Supplier_Id", "dbo.tb_m_supplier");
            DropIndex("dbo.tb_m_items", new[] { "Supplier_Id" });
            DropTable("dbo.tb_m_supplier");
            DropTable("dbo.tb_m_items");
        }
    }
}
