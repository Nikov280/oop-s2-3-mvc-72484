using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGCManagement.VMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToExam8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Enrolments",
                newName: "StudentName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentName",
                table: "Enrolments",
                newName: "FullName");
        }
    }
}
