using FluentMigrator;


namespace TasksLibrary.DB.Migrations
{
    [Migration(202305250924)]
    public class AddNote : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Notes")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("Modified").AsDateTime().NotNullable()
                .WithColumn("Task").AsString(450).NotNullable()
                .WithColumn("Description").AsString(450).Nullable()
                .WithColumn("User_id").AsGuid().NotNullable();

            Create.ForeignKey("Fk_Notes_User")
                .FromTable("Notes").ForeignColumn("User_id")
                .ToTable("Users").PrimaryColumn("Id");
        }
    }
}
