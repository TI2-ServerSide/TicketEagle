using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "Bilhete",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Bilhete");
        }
    }
}
