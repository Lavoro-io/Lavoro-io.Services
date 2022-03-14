using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalService.Migrations
{
    public partial class _1103202216441408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ChatCode",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ChatCode",
                table: "Chats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatCode",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ChatCode",
                table: "Chats");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatDALChatId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
