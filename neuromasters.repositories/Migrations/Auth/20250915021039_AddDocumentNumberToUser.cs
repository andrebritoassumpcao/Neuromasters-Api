using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace neuromasters.repositories.Migrations.Auth
{
    /// <inheritdoc />
    public partial class AddDocumentNumberToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Users");
        }
    }
}
