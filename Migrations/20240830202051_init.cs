using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { new Guid("0d473d3d-aead-45c9-8401-453ce9f52600"), "Toyota", "Corolla", 20000L, 2020 },
                    { new Guid("0efd3ea5-96f3-4b0f-8c6f-ee3120911730"), "BMW", "X5", 60000L, 2020 },
                    { new Guid("16205268-2849-4c47-a666-4f11ee7ee7a4"), "Honda", "Accord", 30000L, 2020 },
                    { new Guid("17370ada-d939-4740-90ea-629aeea1ec7e"), "Honda", "Civic", 25000L, 2020 },
                    { new Guid("29491c70-d077-4bcc-ab40-c92f6882563c"), "Toyota", "Camry", 25000L, 2020 },
                    { new Guid("340f9688-e61c-4636-bea6-10fdd7859c89"), "Volkswagen", "Golf", 22000L, 2020 },
                    { new Guid("52f58da2-778d-49a7-a1ec-19c072ba7b68"), "Chevrolet", "Malibu", 28000L, 2020 },
                    { new Guid("5a29df10-ee73-4156-8dba-6c093b731a63"), "BMW", "X6", 65000L, 2020 },
                    { new Guid("921e45a8-47ed-4855-931e-9663f84d91c5"), "Volkswagen", "Jetta", 25000L, 2020 },
                    { new Guid("a9d7bf01-f7f6-4931-9b03-fcf770cefee2"), "Ford", "Fusion", 25000L, 2020 },
                    { new Guid("ae9ff982-79cd-485f-bb80-da1495199bdc"), "Ford", "Focus", 22000L, 2020 },
                    { new Guid("f213cbe1-c3ae-493f-a9bb-89cc073950c6"), "Chevrolet", "Cruze", 23000L, 2020 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
