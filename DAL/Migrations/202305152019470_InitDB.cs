namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false, maxLength: 50),
                        Photo = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Dob = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false, maxLength: 50),
                        Url = c.String(nullable: false, maxLength: 150),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Waranty = c.String(nullable: false, maxLength: 20),
                        Price = c.Double(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SellerId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        Amount = c.Double(nullable: false),
                        PlacedAt = c.DateTime(nullable: false),
                        DeliveredAt = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false, maxLength: 50),
                        Photo = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Dob = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(nullable: false, maxLength: 50),
                        ExpiredAt = c.DateTime(nullable: false),
                        PaymentId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Payments", t => t.PaymentId, cascadeDelete: true)
                .Index(t => t.PaymentId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OneTimePasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtpCode = c.String(nullable: false, maxLength: 10),
                        CreatedAt = c.DateTime(nullable: false),
                        ExpiredAt = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Double(nullable: false),
                        Message = c.String(nullable: false, maxLength: 200),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(nullable: false, maxLength: 50),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false, maxLength: 50),
                        Photo = c.String(maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Dob = c.DateTime(nullable: false),
                        Address = c.String(maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(nullable: false, maxLength: 100),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                        CompanyLogo = c.String(nullable: false, maxLength: 50),
                        Status = c.String(nullable: false, maxLength: 30),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SellerId", "dbo.Sellers");
            DropForeignKey("dbo.ProductPhotoes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Reviews", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OneTimePasswords", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerPayments", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.CustomerPayments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProductPhotoes", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropIndex("dbo.Reviews", new[] { "CustomerId" });
            DropIndex("dbo.OneTimePasswords", new[] { "CustomerId" });
            DropIndex("dbo.CustomerPayments", new[] { "CustomerId" });
            DropIndex("dbo.CustomerPayments", new[] { "PaymentId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "SellerId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Sellers");
            DropTable("dbo.ProductPhotoes");
            DropTable("dbo.Reviews");
            DropTable("dbo.OneTimePasswords");
            DropTable("dbo.Payments");
            DropTable("dbo.CustomerPayments");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.Admins");
        }
    }
}
