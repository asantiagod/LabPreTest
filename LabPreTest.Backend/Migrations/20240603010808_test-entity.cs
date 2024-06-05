using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class testentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestTubeId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestTubeId",
                table: "Tests",
                column: "TestTubeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestTubes_TestTubeId",
                table: "Tests",
                column: "TestTubeId",
                principalTable: "TestTubes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestTubes_TestTubeId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestTubeId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestTubeId",
                table: "Tests");
        }
    }
}
