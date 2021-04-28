using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocknize.Infrastructure.Migrations
{
    public partial class AddingUserToMovimantion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Movimentation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Credentials",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentation_UserId",
                table: "Movimentation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentation_User_UserId",
                table: "Movimentation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentation_User_UserId",
                table: "Movimentation");

            migrationBuilder.DropIndex(
                name: "IX_Movimentation_UserId",
                table: "Movimentation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movimentation");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);
        }
    }
}
