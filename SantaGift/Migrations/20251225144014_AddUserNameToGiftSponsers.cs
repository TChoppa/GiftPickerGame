using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantaGift.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameToGiftSponsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UseName",
                table: "GiftSponsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseName",
                table: "GiftSponsers");
        }
    }
}
