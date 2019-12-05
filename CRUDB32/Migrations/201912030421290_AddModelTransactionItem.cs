namespace CRUDB32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelTransactionItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_m_transactionItem", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_m_transactionItem", "Quantity", c => c.String());
        }
    }
}
