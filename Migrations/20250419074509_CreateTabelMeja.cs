using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTabelMeja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nomor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTersedia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meja", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meja");
        }
    }
}
