namespace CRUDB32.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelUserRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tb_m_users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_m_roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_users", "Role_Id", "dbo.tb_m_roles");
            DropIndex("dbo.tb_m_users", new[] { "Role_Id" });
            DropTable("dbo.tb_m_users");
            DropTable("dbo.tb_m_roles");
        }
    }
}
