namespace QuizWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdProviderName = c.String(),
                        Name = c.String(),
                        AttendAsPlayerAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        OwnerUserId = c.String(),
                        Body = c.String(nullable: false),
                        BodyFormat = c.Int(nullable: false),
                        Option1 = c.String(nullable: false),
                        OptionImage1 = c.String(),
                        Option2 = c.String(nullable: false),
                        OptionImage2 = c.String(),
                        Option3 = c.String(),
                        OptionImage3 = c.String(),
                        Option4 = c.String(),
                        OptionImage4 = c.String(),
                        Option5 = c.String(),
                        OptionImage5 = c.String(),
                        Option6 = c.String(),
                        OptionImage6 = c.String(),
                        IndexOfCorrectOption = c.Int(nullable: false),
                        Comment = c.String(),
                        CommentFormat = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        PlayerID = c.String(),
                        QuestionID = c.Int(nullable: false),
                        ChoosedOptionIndex = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerID);
            
            CreateTable(
                "dbo.Contexts",
                c => new
                    {
                        ContextID = c.Int(nullable: false, identity: true),
                        CurrentQuestionID = c.Int(nullable: false),
                        CurrentState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContextID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contexts");
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Users");
        }
    }
}
