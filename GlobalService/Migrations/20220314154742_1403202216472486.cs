using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalService.Migrations
{
    public partial class _1403202216472486 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatType",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatType",
                table: "Chats");
        }
    }
}
