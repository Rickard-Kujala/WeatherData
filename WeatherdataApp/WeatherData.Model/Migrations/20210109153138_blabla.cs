using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherData.Model.Migrations
{
    public partial class blabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Datas_Sensors_SensorId",
                table: "Datas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Datas",
                table: "Datas");

            migrationBuilder.RenameTable(
                name: "Datas",
                newName: "Data");

            migrationBuilder.RenameIndex(
                name: "IX_Datas_SensorId",
                table: "Data",
                newName: "IX_Data_SensorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Data",
                table: "Data",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Data_Sensors_SensorId",
                table: "Data",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Data_Sensors_SensorId",
                table: "Data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Data",
                table: "Data");

            migrationBuilder.RenameTable(
                name: "Data",
                newName: "Datas");

            migrationBuilder.RenameIndex(
                name: "IX_Data_SensorId",
                table: "Datas",
                newName: "IX_Datas_SensorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Datas",
                table: "Datas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Datas_Sensors_SensorId",
                table: "Datas",
                column: "SensorId",
                principalTable: "Sensors",
                principalColumn: "SensorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
