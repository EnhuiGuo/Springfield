namespace Springfield.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCatalog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catalogue",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Items", "CatalogId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Items", "CatalogId");
            AddForeignKey("dbo.Items", "CatalogId", "dbo.Catalogue", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "CatalogId", "dbo.Catalogue");
            DropIndex("dbo.Items", new[] { "CatalogId" });
            DropColumn("dbo.Items", "CatalogId");
            DropTable("dbo.Catalogue");
        }
    }
}
