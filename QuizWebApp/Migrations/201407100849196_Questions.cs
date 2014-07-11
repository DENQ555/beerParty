namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "BodyImage", c => c.String());
            DropColumn("dbo.Questions", "OptionImage1");
            DropColumn("dbo.Questions", "OptionImage2");
            DropColumn("dbo.Questions", "Option3");
            DropColumn("dbo.Questions", "OptionImage3");
            DropColumn("dbo.Questions", "Option3Count");
            DropColumn("dbo.Questions", "Option4");
            DropColumn("dbo.Questions", "OptionImage4");
            DropColumn("dbo.Questions", "Option4Count");
            DropColumn("dbo.Questions", "Option5");
            DropColumn("dbo.Questions", "OptionImage5");
            DropColumn("dbo.Questions", "Option5Count");
            DropColumn("dbo.Questions", "Option6");
            DropColumn("dbo.Questions", "OptionImage6");
            DropColumn("dbo.Questions", "Option6Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Option6Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "OptionImage6", c => c.String());
            AddColumn("dbo.Questions", "Option6", c => c.String());
            AddColumn("dbo.Questions", "Option5Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "OptionImage5", c => c.String());
            AddColumn("dbo.Questions", "Option5", c => c.String());
            AddColumn("dbo.Questions", "Option4Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "OptionImage4", c => c.String());
            AddColumn("dbo.Questions", "Option4", c => c.String());
            AddColumn("dbo.Questions", "Option3Count", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "OptionImage3", c => c.String());
            AddColumn("dbo.Questions", "Option3", c => c.String());
            AddColumn("dbo.Questions", "OptionImage2", c => c.String());
            AddColumn("dbo.Questions", "OptionImage1", c => c.String());
            DropColumn("dbo.Questions", "BodyImage");
        }
    }
}
