using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class MoveToConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Description", "IsAvailable", "PricePerNight", "RoomNumber", "RoomType" },
                values: new object[,]
                {
                    { "room-1", null, true, 100m, "101", 2 },
                    { "room-2", null, false, 200m, "102", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "user-1", 0, "a920f4e5-7352-4ff7-8887-abf4a0afa7e6", new DateTime(2025, 6, 19, 12, 4, 50, 617, DateTimeKind.Utc).AddTicks(1920), "elizachigir@gmail.com", false, "Elizaveta", "Chigir", false, null, null, null, "123456", null, false, "Customer", "e1d661cc-c4fe-44c4-a1e8-50c94464531f", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: "room-1");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: "room-2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "user-1");
        }
    }
}
