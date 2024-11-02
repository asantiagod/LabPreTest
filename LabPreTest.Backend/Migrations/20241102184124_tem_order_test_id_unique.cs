using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class tem_order_test_id_unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemporalOrders_TestId",
                table: "TemporalOrders");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_TestId",
                table: "TemporalOrders",
                column: "TestId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemporalOrders_TestId",
                table: "TemporalOrders");

            migrationBuilder.CreateIndex(
                name: "IX_TemporalOrders_TestId",
                table: "TemporalOrders",
                column: "TestId");
        }
    }
}
