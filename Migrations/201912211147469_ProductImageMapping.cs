namespace OnlineLibrary1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImageMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImageMaps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                        ImageNumber = c.Int(nullable: false),
                        ProductImage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.ProductImages", t => t.ProductImage_ID)
                .Index(t => t.ProductID)
                .Index(t => t.ProductImage_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImageMaps", "ProductImage_ID", "dbo.ProductImages");
            DropForeignKey("dbo.ProductImageMaps", "ProductID", "dbo.Products");
            DropIndex("dbo.ProductImageMaps", new[] { "ProductImage_ID" });
            DropIndex("dbo.ProductImageMaps", new[] { "ProductID" });
            DropTable("dbo.ProductImageMaps");
        }
    }
}
