using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class promotorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Promotor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Promotor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Promotor",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PromotorEvento",
                columns: table => new
                {
                    PromotorFK = table.Column<int>(nullable: false),
                    EventoFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotorEvento", x => new { x.PromotorFK, x.EventoFK });
                    table.ForeignKey(
                        name: "FK_PromotorEvento_Evento_EventoFK",
                        column: x => x.EventoFK,
                        principalTable: "Evento",
                        principalColumn: "EvId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotorEvento_Promotor_PromotorFK",
                        column: x => x.PromotorFK,
                        principalTable: "Promotor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotorEvento_EventoFK",
                table: "PromotorEvento",
                column: "EventoFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotorEvento");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Promotor");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Promotor");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Promotor");
        }
    }
}
