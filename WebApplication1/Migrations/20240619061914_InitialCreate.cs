using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserId = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "TeachingInfo",
                columns: table => new
                {
                    TeachID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    School = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Subject = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    mem = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    maxmem = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingInfo", x => x.TeachID);
                });

            migrationBuilder.CreateTable(
                name: "TeachingRecord",
                columns: table => new
                {
                    RecordID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TeachID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    StudentID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Evaluation = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingRecord", x => x.RecordID);
                    table.ForeignKey(
                        name: "FK_TeachingRecord_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeachingRecord_TeachingInfo_TeachID",
                        column: x => x.TeachID,
                        principalTable: "TeachingInfo",
                        principalColumn: "TeachID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingRecord_StudentID",
                table: "TeachingRecord",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingRecord_TeachID",
                table: "TeachingRecord",
                column: "TeachID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachingRecord");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TeachingInfo");
        }
    }
}
