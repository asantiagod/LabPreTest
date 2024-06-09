using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabPreTest.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addimages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionImage_Section_SectionId",
                table: "SectionImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionImage",
                table: "SectionImage");

            migrationBuilder.RenameTable(
                name: "SectionImage",
                newName: "SectionImages");

            migrationBuilder.RenameIndex(
                name: "IX_SectionImage_SectionId",
                table: "SectionImages",
                newName: "IX_SectionImages_SectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionImages",
                table: "SectionImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionImages_Section_SectionId",
                table: "SectionImages",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SectionImages_Section_SectionId",
                table: "SectionImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SectionImages",
                table: "SectionImages");

            migrationBuilder.RenameTable(
                name: "SectionImages",
                newName: "SectionImage");

            migrationBuilder.RenameIndex(
                name: "IX_SectionImages_SectionId",
                table: "SectionImage",
                newName: "IX_SectionImage_SectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SectionImage",
                table: "SectionImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SectionImage_Section_SectionId",
                table: "SectionImage",
                column: "SectionId",
                principalTable: "Section",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
