using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Evento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Evento");
        }
    }
}
