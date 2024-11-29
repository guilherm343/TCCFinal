using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCCTrabalho3.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCaminhoImagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemCaminho",
                table: "CursosModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCaminho",
                table: "CursosModel");
        }
    }
}
