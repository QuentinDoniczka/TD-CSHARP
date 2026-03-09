using FluentMigrator;

namespace ConsoleQuentinDoniczka.Data.Migrations;

[Migration(2)]
public class CreateGameSavesTable : Migration
{
    public override void Up()
    {
        Create.Table("game_saves")
            .WithColumn("id").AsInt32().PrimaryKey()
            .WithColumn("grid_state").AsString(9).NotNullable()
            .WithColumn("current_symbol").AsFixedLengthAnsiString(1).NotNullable()
            .WithColumn("saved_at").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime);
    }

    public override void Down()
    {
        Delete.Table("game_saves");
    }
}
