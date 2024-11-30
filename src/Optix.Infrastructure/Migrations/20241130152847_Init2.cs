using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optix.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "UserProfiles",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "UserProfiles",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "UserProfiles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "OpenTime",
                table: "Trades",
                newName: "OpenedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Trades",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Trades",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Trades",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CloseTime",
                table: "Trades",
                newName: "ClosedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Ticks",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Ticks",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Ticks",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Ticks",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Assets",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Assets",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Assets",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Accounts",
                newName: "ModifiedAt");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Accounts",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Accounts",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Trigger = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "UserProfiles",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "UserProfiles",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "UserProfiles",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "OpenedAt",
                table: "Trades",
                newName: "OpenTime");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Trades",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Trades",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Trades",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "ClosedAt",
                table: "Trades",
                newName: "CloseTime");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Ticks",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Ticks",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Ticks",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Ticks",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Assets",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Assets",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Assets",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "Accounts",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Accounts",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Accounts",
                newName: "CreatedDate");
        }
    }
}
