using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class updateModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bilhete_Utilizador_UtilizadorUserID",
                table: "Bilhete");

            migrationBuilder.DropIndex(
                name: "IX_Bilhete_UtilizadorUserID",
                table: "Bilhete");

            migrationBuilder.DropColumn(
                name: "UtilizadorUserID",
                table: "Bilhete");

            migrationBuilder.CreateTable(
                name: "BilheteUtilizador",
                columns: table => new
                {
                    TicketFK = table.Column<int>(nullable: false),
                    IDFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BilheteUtilizador", x => x.TicketFK);
                    table.ForeignKey(
                        name: "FK_BilheteUtilizador_Utilizador_IDFK",
                        column: x => x.IDFK,
                        principalTable: "Utilizador",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BilheteUtilizador_Bilhete_TicketFK",
                        column: x => x.TicketFK,
                        principalTable: "Bilhete",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BilheteUtilizador_IDFK",
                table: "BilheteUtilizador",
                column: "IDFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BilheteUtilizador");

            migrationBuilder.AddColumn<int>(
                name: "UtilizadorUserID",
                table: "Bilhete",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bilhete_UtilizadorUserID",
                table: "Bilhete",
                column: "UtilizadorUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bilhete_Utilizador_UtilizadorUserID",
                table: "Bilhete",
                column: "UtilizadorUserID",
                principalTable: "Utilizador",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
