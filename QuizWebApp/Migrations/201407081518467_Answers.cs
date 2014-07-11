namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Answers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Option1Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Option2Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Option3Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Option4Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Option5Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "Option6Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "DistributePoint", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "Point", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Point");
            DropColumn("dbo.Questions", "DistributePoint");
            DropColumn("dbo.Questions", "Option6Count");
            DropColumn("dbo.Questions", "Option5Count");
            DropColumn("dbo.Questions", "Option4Count");
            DropColumn("dbo.Questions", "Option3Count");
            DropColumn("dbo.Questions", "Option2Count");
            DropColumn("dbo.Questions", "Option1Count");
        }
    }
}
