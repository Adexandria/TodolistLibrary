using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.NHibernate.Migrations
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
                .WithColumn("AccessTokenId").AsGuid().Nullable()
                .WithColumn("RefreshTokenId").AsGuid().Nullable();

            Create.Table("AccessTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(255).NotNullable()
                .WithColumn("ExpirationDate").AsDateTime().NotNullable()
                .WithColumn("UserId").AsGuid().Nullable();

            Create.Table("RefreshTokens")
                .WithColumn("Id").AsGuid().PrimaryKey().NotNullable().WithDefault(SystemMethods.NewGuid)
                .WithColumn("Token").AsString(255).NotNullable()
                .WithColumn("ExpirationDate").AsDateTime().NotNullable()
                .WithColumn("UserId").AsGuid().Nullable();

            Create.ForeignKey("FK_User_AccessToken")
                .FromTable("User").ForeignColumn("AccessTokenId")
                .ToTable("AccessToken").PrimaryColumn("Id");
            
            Create.ForeignKey("FK_User_RefreshToken")
                .FromTable("User").ForeignColumn("RefreshTokenId")
                  .ToTable("RefreshToken").PrimaryColumn("Id");
        }
    }
}
