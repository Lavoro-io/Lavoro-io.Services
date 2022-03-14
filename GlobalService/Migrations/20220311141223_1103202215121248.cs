using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalService.Migrations
{
    public partial class _1103202215121248 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChatDALChatId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatDALChatId",
                table: "Users",
                column: "ChatDALChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chats_ChatDALChatId",
                table: "Users",
                column: "ChatDALChatId",
                principalTable: "Chats",
                principalColumn: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chats_ChatDALChatId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatDALChatId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChatDALChatId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");
        }
    }
}
