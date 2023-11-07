﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogsMonitor.DataAccess.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class LogsNumberPrefixIsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "LogNumberCounters",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LogNumberCounters_Prefix",
                table: "LogNumberCounters",
                column: "Prefix",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogNumberCounters_Prefix",
                table: "LogNumberCounters");

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "LogNumberCounters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
