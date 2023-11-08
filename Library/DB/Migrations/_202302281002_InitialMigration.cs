using FluentMigrator;


namespace TasksLibrary.DB.Migrations
{
    [Migration(202302281002,"Create_database_objects")]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Email").AsString(int.MaxValue).NotNullable()
                .WithColumn("Name").AsString(int.MaxValue).NotNullable()
                .WithColumn("AccessToken_id").AsGuid().Nullable()
                .WithColumn("PasswordHash").AsString(int.MaxValue).NotNullable()
                .WithColumn("Salt").AsString(int.MaxValue).NotNullable()
                .WithColumn("RefreshToken_id").AsGuid().Nullable();

            Create.Table("AccessTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(int.MaxValue).NotNullable()
                .WithColumn("User_id").AsGuid().Nullable();

            Create.Table("RefreshTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(int.MaxValue).NotNullable()
                .WithColumn("Expires").AsDateTime().NotNullable()
                .WithColumn("IsRevoked").AsBoolean().NotNullable()
                .WithColumn("User_id").AsGuid().Nullable();

            Create.ForeignKey("FK_User_AccessToken")
                .FromTable("Users").ForeignColumn("AccessToken_id")
                .ToTable("AccessTokens").PrimaryColumn("Id");
            
            Create.ForeignKey("FK_User_RefreshToken")
                .FromTable("Users").ForeignColumn("RefreshToken_id")
                  .ToTable("RefreshTokens").PrimaryColumn("Id");
        }
    }
}
