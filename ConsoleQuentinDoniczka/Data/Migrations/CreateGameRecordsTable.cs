using FluentMigrator;

namespace ConsoleQuentinDoniczka.Data.Migrations;

[Migration(1)]
public class CreateGameRecordsTable : Migration
{
    public override void Up()
    {
        Create.Table("game_records")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("result").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("game_records");
    }
}
