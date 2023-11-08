using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIDOM.Migrations
{
    public partial class RolesSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bd048320-44d1-4976-9959-832a7afeb331", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e64f048c-8849-4f6f-b60a-9bb7ba6cac21", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd048320-44d1-4976-9959-832a7afeb331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e64f048c-8849-4f6f-b60a-9bb7ba6cac21");
        }
    }
}
