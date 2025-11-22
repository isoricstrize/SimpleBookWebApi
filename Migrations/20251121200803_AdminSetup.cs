using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBookWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AdminSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("6d67338d-54a0-463c-b5fb-18e1af3df682"), "AQAAAAIAAYagAAAAEH/2Th5V7nKAS9isjhJHTzDY9Fk6Z7WGSC79koe6ZhOp33vbZvbxlDm63In2eqrakg==", "Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d67338d-54a0-463c-b5fb-18e1af3df682"));
        }
    }
}
