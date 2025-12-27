using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantaGift.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPasswordtoRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPassword",
                table: "registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPassword",
                table: "registers");
        }
    }
}
