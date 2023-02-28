using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adys.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedFileLesson2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fileUrl",
                table: "LessonFile",
                newName: "FileUrl");

            migrationBuilder.RenameColumn(
                name: "fileName",
                table: "LessonFile",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileUrl",
                table: "LessonFile",
                newName: "fileUrl");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "LessonFile",
                newName: "fileName");
        }
    }
}
