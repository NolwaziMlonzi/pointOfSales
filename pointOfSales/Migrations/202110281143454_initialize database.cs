namespace pointOfSales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initializedatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceID);
            
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        CostPerItem = c.Double(nullable: false),
                        TotalCost = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        Invoice_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        CostPerItem = c.Double(nullable: false),
                        ProductsInStock = c.Int(nullable: false),
                        InvoiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.ProductsSolds",
                c => new
                    {
                        ProductSoldID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        sold = c.Double(nullable: false),
                        InStock = c.Double(nullable: false),
                        ReStock = c.Boolean(nullable: false),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductSoldID)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .Index(t => t.Product_ProductID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        UserType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Password = c.String(),
                        RetypePassword = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsSolds", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.ProductsSolds", new[] { "Product_ProductID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.ProductsSolds");
            DropTable("dbo.Products");
            DropTable("dbo.ProductItems");
            DropTable("dbo.Invoices");
        }
    }
}
