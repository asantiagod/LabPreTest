using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class fix_userid_in_temporalorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporalOrders_AspNetUsers_UserId1",
                table: "TemporalOrders");

            migrationBuilder.DropIndex(
                name: "IX_TemporalOrders_UserId1",
                table: "TemporalOrders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TemporalOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TemporalOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_UserId",
                table: "TemporalOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporalOrders_AspNetUsers_UserId",
                table: "TemporalOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemporalOrders_AspNetUsers_UserId",
                table: "TemporalOrders");

            migrationBuilder.DropIndex(
                name: "IX_TemporalOrders_UserId",
                table: "TemporalOrders");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TemporalOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TemporalOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_UserId1",
                table: "TemporalOrders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TemporalOrders_AspNetUsers_UserId1",
                table: "TemporalOrders",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
