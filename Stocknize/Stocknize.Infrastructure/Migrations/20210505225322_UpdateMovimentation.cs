using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocknize.Infrastructure.Migrations
{
    public partial class UpdateMovimentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Movimentation",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Movimentation");
        }
    }
}
