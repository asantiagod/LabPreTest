using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class temporalorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Section_SectionId1",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "SectionId1",
                table: "Tests",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_SectionId1",
                table: "Tests",
                newName: "IX_Tests_SectionId");

            migrationBuilder.CreateTable(
                name: "TemporalOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    MedicId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporalOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporalOrders_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalOrders_Medicians_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalOrders_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TemporalOrders_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_MedicId",
                table: "TemporalOrders",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_PatientId",
                table: "TemporalOrders",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_TestId",
                table: "TemporalOrders",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_UserId1",
                table: "TemporalOrders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Section_SectionId",
                table: "Tests",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Section_SectionId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "TemporalOrders");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Tests",
                newName: "SectionId1");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_SectionId",
                table: "Tests",
                newName: "IX_Tests_SectionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Section_SectionId1",
                table: "Tests",
                column: "SectionId1",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
