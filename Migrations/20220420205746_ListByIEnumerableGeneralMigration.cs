using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CNaturalApi.Migrations
{
    public partial class ListByIEnumerableGeneralMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accountancies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 4, 20, 16, 57, 46, 250, DateTimeKind.Local).AddTicks(8998));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accountancies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 4, 18, 22, 50, 27, 9, DateTimeKind.Local).AddTicks(5842));
        }
    }
}
