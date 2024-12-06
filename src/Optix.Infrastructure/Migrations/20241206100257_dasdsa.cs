using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dasdsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticks_Symbol_Date",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Ticks");

            migrationBuilder.AddColumn<long>(
                name: "UnixDate",
                table: "Ticks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnixDate",
                table: "Ticks");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Ticks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Ticks_Symbol_Date",
                table: "Ticks",
                columns: new[] { "Symbol", "Date" },
                unique: true);
        }
    }
}
