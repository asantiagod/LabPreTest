using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class wiptestentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Tests");

            migrationBuilder.AddColumn<int>(
                name: "SectionId1",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TestConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestConditions_PreanalyticConditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "PreanalyticConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestConditions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_SectionId1",
                table: "Tests",
                column: "SectionId1");

            migrationBuilder.CreateIndex(
                name: "IX_TestConditions_ConditionId",
                table: "TestConditions",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestConditions_TestId",
                table: "TestConditions",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Section_SectionId1",
                table: "Tests",
                column: "SectionId1",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Section_SectionId1",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "TestConditions");

            migrationBuilder.DropIndex(
                name: "IX_Tests_SectionId1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "SectionId1",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Tests",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
