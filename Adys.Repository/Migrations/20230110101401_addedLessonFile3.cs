using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adys.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedLessonFile3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "LessonFile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "LessonFile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonFile_Lessons_LessonId",
                table: "LessonFile",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
