namespace NewsFeedVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sources", "Title_selector", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sources", "Title_selector");
        }
    }
}
