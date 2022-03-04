namespace DivishRam_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameArticles",
                c => new
                    {
                        ArticleId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseYear = c.Int(nullable: false),
                        Rating = c.Single(nullable: false),
                        Author = c.String(),
                        Summary = c.String(),
                        PublisherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArticleId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(),
                        Founder = c.String(),
                        Country = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                    })
                .PrimaryKey(t => t.StoreId);
            
            CreateTable(
                "dbo.StoreGameArticles",
                c => new
                    {
                        Store_StoreId = c.Int(nullable: false),
                        GameArticle_ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Store_StoreId, t.GameArticle_ArticleId })
                .ForeignKey("dbo.Stores", t => t.Store_StoreId, cascadeDelete: true)
                .ForeignKey("dbo.GameArticles", t => t.GameArticle_ArticleId, cascadeDelete: true)
                .Index(t => t.Store_StoreId)
                .Index(t => t.GameArticle_ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreGameArticles", "GameArticle_ArticleId", "dbo.GameArticles");
            DropForeignKey("dbo.StoreGameArticles", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.GameArticles", "PublisherId", "dbo.Publishers");
            DropIndex("dbo.StoreGameArticles", new[] { "GameArticle_ArticleId" });
            DropIndex("dbo.StoreGameArticles", new[] { "Store_StoreId" });
            DropIndex("dbo.GameArticles", new[] { "PublisherId" });
            DropTable("dbo.StoreGameArticles");
            DropTable("dbo.Stores");
            DropTable("dbo.Publishers");
            DropTable("dbo.GameArticles");
        }
    }
}
