using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VGCManagement.VMC.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendanceAndEnrolmentNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_Enrolments_CourseEnrolmentId",
                table: "AttendanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_Courses_CourseId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolments_StudentProfiles_StudentProfileId",
                table: "Enrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrolments",
                table: "Enrolments");

            migrationBuilder.RenameTable(
                name: "Enrolments",
                newName: "CourseEnrolments");

            migrationBuilder.RenameIndex(
                name: "IX_Enrolments_StudentProfileId",
                table: "CourseEnrolments",
                newName: "IX_CourseEnrolments_StudentProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrolments_CourseId",
                table: "CourseEnrolments",
                newName: "IX_CourseEnrolments_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEnrolments",
                table: "CourseEnrolments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_CourseEnrolments_CourseEnrolmentId",
                table: "AttendanceRecords",
                column: "CourseEnrolmentId",
                principalTable: "CourseEnrolments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrolments_Courses_CourseId",
                table: "CourseEnrolments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrolments_StudentProfiles_StudentProfileId",
                table: "CourseEnrolments",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecords_CourseEnrolments_CourseEnrolmentId",
                table: "AttendanceRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrolments_Courses_CourseId",
                table: "CourseEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrolments_StudentProfiles_StudentProfileId",
                table: "CourseEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEnrolments",
                table: "CourseEnrolments");

            migrationBuilder.RenameTable(
                name: "CourseEnrolments",
                newName: "Enrolments");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrolments_StudentProfileId",
                table: "Enrolments",
                newName: "IX_Enrolments_StudentProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrolments_CourseId",
                table: "Enrolments",
                newName: "IX_Enrolments_CourseId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrolments",
                table: "Enrolments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecords_Enrolments_CourseEnrolmentId",
                table: "AttendanceRecords",
                column: "CourseEnrolmentId",
                principalTable: "Enrolments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_Courses_CourseId",
                table: "Enrolments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolments_StudentProfiles_StudentProfileId",
                table: "Enrolments",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id");
        }
    }
}
