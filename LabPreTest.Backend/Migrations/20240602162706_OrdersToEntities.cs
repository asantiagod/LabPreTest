using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class OrdersToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MedicId",
                table: "Orders",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_patientId",
                table: "Orders",
                column: "patientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Medicians_MedicId",
                table: "Orders",
                column: "MedicId",
                principalTable: "Medicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Patients_patientId",
                table: "Orders",
                column: "patientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Medicians_MedicId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Patients_patientId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MedicId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_patientId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MedicId",
                table: "Orders");
        }
    }
}
