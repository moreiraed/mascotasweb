using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MascotasWeb.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCamposMascota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Especie",
                table: "Mascotas",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FechaNacimiento",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Raza",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "Raza",
                table: "Mascotas");

            migrationBuilder.AlterColumn<string>(
                name: "Especie",
                table: "Mascotas",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
