namespace FastFood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsersTbl", "password", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
