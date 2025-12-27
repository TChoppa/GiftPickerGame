using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SantaGift.Migrations
{
    /// <inheritdoc />
    public partial class GiftSponser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiftSponsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sponser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picker = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isParticipant = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftSponsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftSponsers");
        }
    }
}
