namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaltColumnAddedInUsersTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Salt", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Customers", "Salt", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Sellers", "Salt", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sellers", "Salt");
            DropColumn("dbo.Customers", "Salt");
            DropColumn("dbo.Admins", "Salt");
        }
    }
}
