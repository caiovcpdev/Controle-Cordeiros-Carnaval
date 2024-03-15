using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleCordeirosCarnaval.Migrations
{
    /// <inheritdoc />
    public partial class Correção_Tabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cordeiroModels",
                table: "cordeiroModels");

            migrationBuilder.RenameTable(
                name: "cordeiroModels",
                newName: "cordeiro");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cordeiro",
                table: "cordeiro",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cordeiro",
                table: "cordeiro");

            migrationBuilder.RenameTable(
                name: "cordeiro",
                newName: "cordeiroModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cordeiroModels",
                table: "cordeiroModels",
                column: "Id");
        }
    }
}
