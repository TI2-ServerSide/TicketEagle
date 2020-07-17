using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class updateModelo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BilheteUtilizador");

            migrationBuilder.AddColumn<int>(
                name: "IDFK",
                table: "Bilhete",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bilhete_IDFK",
                table: "Bilhete",
                column: "IDFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Bilhete_Utilizador_IDFK",
                table: "Bilhete",
                column: "IDFK",
                principalTable: "Utilizador",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bilhete_Utilizador_IDFK",
                table: "Bilhete");

            migrationBuilder.DropIndex(
                name: "IX_Bilhete_IDFK",
                table: "Bilhete");

            migrationBuilder.DropColumn(
                name: "IDFK",
                table: "Bilhete");

            migrationBuilder.CreateTable(
                name: "BilheteUtilizador",
                columns: table => new
                {
                    TicketFK = table.Column<int>(type: "int", nullable: false),
                    IDFK = table.Column<int>(type: "int", nullable: false)
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
    }
}
