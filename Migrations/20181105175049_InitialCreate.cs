using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    TargetWeight = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    BF = table.Column<int>(nullable: false),
                    TargetBF = table.Column<int>(nullable: false),
                    BicepCircuit = table.Column<int>(nullable: false),
                    ForearmCircuit = table.Column<int>(nullable: false),
                    ChestCircuit = table.Column<int>(nullable: false),
                    HipCircuit = table.Column<int>(nullable: false),
                    WaistCircuit = table.Column<int>(nullable: false),
                    CalfCircuit = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
