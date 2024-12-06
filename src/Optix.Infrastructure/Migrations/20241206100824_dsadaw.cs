using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dsadaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnixDate",
                table: "Ticks",
                newName: "TimeStamp");

            migrationBuilder.CreateIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks",
                columns: new[] { "Symbol", "TimeStamp" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Ticks",
                newName: "UnixDate");
        }
    }
}
