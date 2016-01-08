namespace Andacol.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "andacol.AnswerOptions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        OptionalQuestion_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("andacol.Questions", t => t.OptionalQuestion_Id, cascadeDelete: true)
                .Index(t => t.OptionalQuestion_Id);
            
            CreateTable(
                "andacol.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateExpired = c.DateTime(nullable: false),
                        DateStarted = c.DateTime(nullable: false),
                        NextDue = c.DateTime(nullable: false),
                        Schedule = c.Time(nullable: false, precision: 7),
                        QuestionText = c.String(),
                        Min = c.Int(),
                        Max = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DateExpired)
                .Index(t => t.DateStarted)
                .Index(t => t.NextDue);
            
            CreateTable(
                "andacol.AskedQuestions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateAsked = c.DateTime(nullable: false),
                        Question_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("andacol.Questions", t => t.Question_Id, cascadeDelete: true)
                .Index(t => t.DateAsked)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "andacol.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Identifier = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "andacol.Answers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Score = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AskedQuestion_Id = c.Long(nullable: false),
                        AnswerOption_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("andacol.AskedQuestions", t => t.AskedQuestion_Id, cascadeDelete: true)
                .ForeignKey("andacol.AnswerOptions", t => t.AnswerOption_Id)
                .Index(t => t.AskedQuestion_Id)
                .Index(t => t.AnswerOption_Id);
            
            CreateTable(
                "andacol.UserAskedQuestions",
                c => new
                    {
                        User_Id = c.Long(nullable: false),
                        AskedQuestion_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.AskedQuestion_Id })
                .ForeignKey("andacol.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("andacol.AskedQuestions", t => t.AskedQuestion_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.AskedQuestion_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("andacol.AnswerOptions", "OptionalQuestion_Id", "andacol.Questions");
            DropForeignKey("andacol.AskedQuestions", "Question_Id", "andacol.Questions");
            DropForeignKey("andacol.Answers", "AnswerOption_Id", "andacol.AnswerOptions");
            DropForeignKey("andacol.Answers", "AskedQuestion_Id", "andacol.AskedQuestions");
            DropForeignKey("andacol.UserAskedQuestions", "AskedQuestion_Id", "andacol.AskedQuestions");
            DropForeignKey("andacol.UserAskedQuestions", "User_Id", "andacol.Users");
            DropIndex("andacol.UserAskedQuestions", new[] { "AskedQuestion_Id" });
            DropIndex("andacol.UserAskedQuestions", new[] { "User_Id" });
            DropIndex("andacol.Answers", new[] { "AnswerOption_Id" });
            DropIndex("andacol.Answers", new[] { "AskedQuestion_Id" });
            DropIndex("andacol.AskedQuestions", new[] { "Question_Id" });
            DropIndex("andacol.AskedQuestions", new[] { "DateAsked" });
            DropIndex("andacol.Questions", new[] { "NextDue" });
            DropIndex("andacol.Questions", new[] { "DateStarted" });
            DropIndex("andacol.Questions", new[] { "DateExpired" });
            DropIndex("andacol.AnswerOptions", new[] { "OptionalQuestion_Id" });
            DropTable("andacol.UserAskedQuestions");
            DropTable("andacol.Answers");
            DropTable("andacol.Users");
            DropTable("andacol.AskedQuestions");
            DropTable("andacol.Questions");
            DropTable("andacol.AnswerOptions");
        }
    }
}
