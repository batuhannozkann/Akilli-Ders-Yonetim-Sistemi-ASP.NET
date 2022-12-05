using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adys.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Academicians",
                columns: new[] { "Id", "CreatedTime", "FirstName", "LastName", "Title", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Local), "Erkan", "Özhan", "Dr.Ogr.Uyesi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CreatedTime", "FirstName", "LastName", "StudentNumber", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Local), "Batuhan", "Özkan", 2180656011L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "AcademicianId", "CreatedTime", "Description", "LessonCode", "Name", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Veri Madenciligi ", "BMSB403", "Veri Madenciliği", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Makine ögrenmesine giriş", "BMSB406", "Makine Öğrenmesine Giriş", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "LessonStudent",
                columns: new[] { "LessonId", "StudentId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LessonStudent",
                keyColumns: new[] { "LessonId", "StudentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "LessonStudent",
                keyColumns: new[] { "LessonId", "StudentId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Academicians",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
