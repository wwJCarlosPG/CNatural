using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNaturalApi.Migrations
{
    public partial class SeedingDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accountancies",
                columns: new[] { "Id", "Date", "EarnedMoney", "InvestedMoney" },
                values: new object[] { 1, new DateTime(2022, 4, 8, 2, 44, 25, 834, DateTimeKind.Local).AddTicks(7165), 0.0, 0.0 });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "Address", "IsDeleted", "Mobile", "Name" },
                values: new object[] { 1, "Calle 28", false, "54401444", "Juan" });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "Address", "IsDeleted", "Mobile", "Name" },
                values: new object[] { 2, "Calle 23", false, "53464918", "Luis" });

            migrationBuilder.InsertData(
                table: "Buyers",
                columns: new[] { "Id", "Address", "IsDeleted", "Mobile", "Name" },
                values: new object[] { 3, "Calle 26", false, "52269101", "Maria" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Count", "Design", "IsDeleted", "Name" },
                values: new object[] { 2, 0, null, false, "Samsung Galaxy A12" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Count", "Design", "IsDeleted", "Name" },
                values: new object[] { 3, 0, null, false, "OnePlus Nord 2" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Count", "Design", "IsDeleted", "Name" },
                values: new object[] { 4, 0, null, false, "Oppo Find X5 Pro" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Count", "Design", "IsDeleted", "Name" },
                values: new object[] { 5, 0, null, false, "iPhone 12 Pro Max" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accountancies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Buyers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
