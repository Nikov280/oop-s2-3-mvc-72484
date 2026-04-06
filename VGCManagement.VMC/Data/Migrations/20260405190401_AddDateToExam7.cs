using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGCManagement.VMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToExam7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Enrolments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Enrolments");
        }
    }
}
