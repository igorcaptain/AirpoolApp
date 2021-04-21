using Microsoft.EntityFrameworkCore.Migrations;

namespace Airpool.Scanner.Infrastructure.Migrations
{
    public partial class LocationUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminalName",
                table: "Locations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TerminalName",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
