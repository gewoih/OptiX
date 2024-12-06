using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class e2rfaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Ticks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticks",
                table: "Ticks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks",
                columns: new[] { "Symbol", "TimeStamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticks",
                table: "Ticks");

            migrationBuilder.DropIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ticks");

            migrationBuilder.CreateIndex(
                name: "IX_Ticks_Symbol_TimeStamp",
                table: "Ticks",
                columns: new[] { "Symbol", "TimeStamp" },
                unique: true);
        }
    }
}
