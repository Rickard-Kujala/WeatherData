using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherData.Model.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Sensors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataId",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
