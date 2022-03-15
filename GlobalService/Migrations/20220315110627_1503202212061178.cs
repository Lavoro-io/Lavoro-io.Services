using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalService.Migrations
{
    public partial class _1503202212061178 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Enabled",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "Enabled");
        }
    }
}
