using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tick_UniqueIndex_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ticks_Symbol_Date",
                table: "Ticks",
                columns: new[] { "Symbol", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticks_Symbol_Date",
                table: "Ticks");
        }
    }
}
