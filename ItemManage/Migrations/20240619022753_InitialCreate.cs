using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemManage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inbound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ItemCode = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inbound", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Code = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    Origin = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    Specification = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    ImagePath = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Outbound",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ItemCode = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    ApplyDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outbound", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
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
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inbound");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Outbound");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
