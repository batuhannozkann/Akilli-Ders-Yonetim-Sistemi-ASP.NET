using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adys.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addedLessonFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonFile_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Academicians",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_LessonFile_LessonId",
                table: "LessonFile",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonFile");

            migrationBuilder.UpdateData(
                table: "Academicians",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedTime",
                value: new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
