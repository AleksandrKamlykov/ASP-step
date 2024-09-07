using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Category", "Title" },
                values: new object[,]
                {
                    { new Guid("063f7a3c-598c-42ee-9381-92a42453d596"), "Jane Austen", "Novel", "Pride and Prejudice" },
                    { new Guid("0c6a48c5-e5a8-4c57-bd7e-83b5ffcdb51b"), "J. R. R. Tolkien", "Fantasy", "The Hobbit" },
                    { new Guid("0f837890-cea3-41aa-a1e1-62290e1b774d"), "Dan Brown", "Mystery", "Angels & Demons" },
                    { new Guid("1d1a0641-32d3-4dc6-ae02-70d81d0c3e5a"), "Charlotte Bronte", "Novel", "Jane Eyre" },
                    { new Guid("1e71a2a1-2e76-4869-b6b8-27cbef202b13"), "Dan Brown", "Mystery", "The Da Vinci Code" },
                    { new Guid("35e281d5-933c-4609-bb6e-faf5d3a05e12"), "J. R. R. Tolkien", "Fantasy", "The Lord of the Rings" },
                    { new Guid("5a5395eb-c69c-4c36-810c-941d12e66074"), "George Orwell", "Novel", "1984" },
                    { new Guid("6162134c-3c7d-40cf-9cbc-b6fe05482bfe"), "J. D. Salinger", "Novel", "The Catcher in the Rye" },
                    { new Guid("7e2ab259-b14b-457a-8b0a-d65fa2fc6d11"), "C. S. Lewis", "Fantasy", "The Lion, the Witch and the Wardrobe" },
                    { new Guid("81b0839d-c80f-4f08-967f-7a5c0f307265"), "Harper Lee", "Novel", "To Kill a Mockingbird" },
                    { new Guid("f3c88511-64f1-43ea-9943-4d2b019c0fe8"), "Emily Bronte", "Novel", "Wuthering Heights" },
                    { new Guid("f5ab19b6-3c4e-44d1-8bc9-012be5105b29"), "F. Scott Fitzgerald", "Novel", "The Great Gatsby" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Cc", "Rate", "Text" },
                values: new object[,]
                {
                    { new Guid("015cb52c-285a-410e-92fd-7b017b43dd53"), "ILS", 11.3011f, "Новий ізраїльський шекель" },
                    { new Guid("02ab21a1-6b36-46ed-a14d-4aa530621836"), "AZN", 24.1539f, "Азербайджанський манат" },
                    { new Guid("0be052d1-96e8-4d73-a378-da34f64c52c2"), "NOK", 3.9006f, "Норвезька крона" },
                    { new Guid("0d7ae1b7-e52f-410f-b0b9-7f55f250e8b4"), "MDL", 2.3611f, "Молдовський лей" },
                    { new Guid("1f6f872b-f999-4490-a2fe-caa1d563176b"), "THB", 1.21201f, "Бат" },
                    { new Guid("31d1b090-283a-4784-a43b-13fbfac5e6f4"), "IDR", 0.0026562f, "Рупія" },
                    { new Guid("4c5b0c7b-5ac9-44e3-81f9-711a9863b1c8"), "EUR", 45.5018f, "Євро" },
                    { new Guid("4f883754-89bb-4ca7-a7d5-1fb66f924e01"), "JPY", 0.28234f, "Єна" },
                    { new Guid("509be7ed-c39b-4003-b79a-e80af6616f92"), "DOP", 0.69143f, "Домініканське песо" },
                    { new Guid("51d373d0-1716-4cb0-83c6-c4de3494c14f"), "LBP", 0.000459f, "Ліванський фунт" },
                    { new Guid("5a9e7b69-f875-422b-b2de-db3e6defebc0"), "USD", 41.0592f, "Долар США" },
                    { new Guid("5d2d6ada-e568-4180-957c-6afc71b581b1"), "GBP", 54.1181f, "Фунт стерлінгів" },
                    { new Guid("6296d867-8e4e-4d08-9ce1-e1bf11dd0d50"), "CNY", 5.7923f, "Юань Женьміньбі" },
                    { new Guid("63082b2e-cf42-4b9d-8cc6-eb6a95155db4"), "AED", 11.179f, "Дирхам ОАЕ" },
                    { new Guid("875bdd53-d35d-4156-b4a4-f5bcc22ea0fe"), "PKR", 0.14738f, "Пакистанська рупія" },
                    { new Guid("89db4e6b-fd6c-4b05-8cdf-26f5fb28131d"), "PLN", 10.6354f, "Злотий" },
                    { new Guid("92be0ad0-f658-4bac-9637-337d2ecc2600"), "CZK", 1.8184f, "Чеська крона" },
                    { new Guid("956d4454-c7b2-46cb-8640-f4a7f50ae423"), "NZD", 25.6948f, "Новозеландський долар" },
                    { new Guid("977786d2-c4c3-436d-8413-93948767d5b9"), "KZT", 0.085217f, "Теньге" },
                    { new Guid("aa25607d-f23f-478e-977e-e32621efa0e1"), "AMD", 0.105951f, "Вірменський драм" },
                    { new Guid("bff0ba98-78f3-4cb1-9b58-9778c041b23f"), "SEK", 4.012f, "Шведська крона" },
                    { new Guid("c13757c2-048e-4468-ac78-c69b1251dd71"), "HKD", 5.2657f, "Гонконгівський долар" },
                    { new Guid("c3ff3cd6-19e3-4bc2-822c-046928324ab8"), "CHF", 48.3362f, "Швейцарський франк" },
                    { new Guid("d6e5a0d2-8625-41f7-8345-7a97d91d326b"), "SAR", 10.9412f, "Саудівський ріял" },
                    { new Guid("daec79e8-7e05-40ac-878d-cc50beb2ce4d"), "DKK", 6.1005f, "Данська крона" },
                    { new Guid("e18025aa-e7db-4fda-86bb-850be5559090"), "SGD", 31.5185f, "Сінгапурський долар" },
                    { new Guid("e9a8bd02-b4d3-4c3d-ab56-1ef48b51efed"), "TRY", 1.2053f, "Турецька ліра" },
                    { new Guid("fa24f63e-481a-4bc2-88ba-3431cbc8f1ce"), "MXN", 2.0898f, "Мексиканське песо" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
