using Microsoft.EntityFrameworkCore.Migrations;

namespace FitCenter.Data.Migrations
{
    public partial class UserModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BF",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TargetWeight",
                table: "Users",
                newName: "TotalDailyEnergyExpenditure");

            migrationBuilder.RenameColumn(
                name: "TargetBF",
                table: "Users",
                newName: "Age");

            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeightTarget",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WeightTarget",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TotalDailyEnergyExpenditure",
                table: "Users",
                newName: "TargetWeight");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "TargetBF");

            migrationBuilder.AddColumn<int>(
                name: "BF",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }
    }
}
