using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Tick_Structure_Reworked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticks_Assets_AssetId",
                table: "Ticks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticks",
                table: "Ticks");

            migrationBuilder.DropIndex(
                name: "IX_Ticks_AssetId",
                table: "Ticks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Ticks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Trades",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Ticks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Symbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Ticks");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "Trades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Ticks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "Ticks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Ticks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Ticks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ticks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Ticks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Assets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Assets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Assets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Assets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Assets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticks",
                table: "Ticks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticks_AssetId",
                table: "Ticks",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticks_Assets_AssetId",
                table: "Ticks",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
