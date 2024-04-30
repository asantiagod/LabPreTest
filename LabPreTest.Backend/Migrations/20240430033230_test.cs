using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "medicId",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "medicName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "patientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id",
                table: "Orders",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_Id",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "medicName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "patientName",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "medicId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
