namespace NewsFeedVn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Img", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Img");
        }
    }
}
