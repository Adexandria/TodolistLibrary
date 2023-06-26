using FluentMigrator;


namespace TasksLibrary.DB.Migrations
{
    [Migration(202302281002)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("AccessToken_id").AsGuid().Nullable()
                .WithColumn("PasswordHash").AsString(255).NotNullable()
                .WithColumn("Salt").AsString(255).NotNullable()
                .WithColumn("RefreshToken_id").AsGuid().Nullable();

            Create.Table("AccessTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(255).NotNullable()
                .WithColumn("User_id").AsGuid().Nullable();

            Create.Table("RefreshTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(255).NotNullable()
                .WithColumn("Expires").AsDateTime().NotNullable()
                .WithColumn("IsRevoked").AsDateTime().NotNullable()
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
