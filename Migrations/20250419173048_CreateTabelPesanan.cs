using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTabelPesanan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pesanan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TanggalPesan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MejaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesanan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pesanan_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pesanan_Meja_MejaId",
                        column: x => x.MejaId,
                        principalTable: "Meja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdukPesanan",
                columns: table => new
                {
                    PesananId = table.Column<int>(type: "int", nullable: false),
                    ProdukId = table.Column<int>(type: "int", nullable: false),
                    Jumlah = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdukPesanan", x => new { x.PesananId, x.ProdukId });
                    table.ForeignKey(
                        name: "FK_ProdukPesanan_Pesanan_PesananId",
                        column: x => x.PesananId,
                        principalTable: "Pesanan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdukPesanan_Produk_ProdukId",
                        column: x => x.ProdukId,
                        principalTable: "Produk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pesanan_MejaId",
                table: "Pesanan",
                column: "MejaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pesanan_UserId",
                table: "Pesanan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdukPesanan_ProdukId",
                table: "ProdukPesanan",
                column: "ProdukId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdukPesanan");

            migrationBuilder.DropTable(
                name: "Pesanan");
        }
    }
}
