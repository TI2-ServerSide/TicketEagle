using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketEagle.Migrations
{
    public partial class migracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeLocal = table.Column<string>(nullable: true),
                    Descrição = table.Column<string>(nullable: true),
                    Capacidade = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Promotor",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Bilhete",
                columns: table => new
                {
                    TicketID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(nullable: true),
                    Descrição = table.Column<string>(nullable: true),
                    DataCompra = table.Column<DateTime>(nullable: false),
                    IDFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bilhete", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Bilhete_Utilizador_IDFK",
                        column: x => x.IDFK,
                        principalTable: "Utilizador",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    EventoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    TicketFK = table.Column<int>(nullable: false),
                    LocalFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.EventoID);
                    table.ForeignKey(
                        name: "FK_Evento_Local_LocalFK",
                        column: x => x.LocalFK,
                        principalTable: "Local",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evento_Bilhete_TicketFK",
                        column: x => x.TicketFK,
                        principalTable: "Bilhete",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bilhete_IDFK",
                table: "Bilhete",
                column: "IDFK");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_LocalFK",
                table: "Evento",
                column: "LocalFK");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_TicketFK",
                table: "Evento",
                column: "TicketFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Promotor");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "Bilhete");

            migrationBuilder.DropTable(
                name: "Utilizador");
        }
    }
}
