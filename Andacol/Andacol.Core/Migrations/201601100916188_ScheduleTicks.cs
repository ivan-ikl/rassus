namespace Andacol.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleTicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("andacol.Questions", "ScheduleTicks", c => c.Long(nullable: false));
            DropColumn("andacol.Questions", "Schedule");
        }
        
        public override void Down()
        {
            AddColumn("andacol.Questions", "Schedule", c => c.Time(nullable: false, precision: 7));
            DropColumn("andacol.Questions", "ScheduleTicks");
        }
    }
}
