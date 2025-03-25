using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleBookWebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling" },
                    { 2, "George R.R. Martin" },
                    { 3, "J.R.R. Tolkien" },
                    { 4, "Harper Lee" },
                    { 5, "F. Scott Fitzgerald" }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Drama" },
                    { 3, "Adventure" },
                    { 4, "Classic" },
                    { 5, "Mystery" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "AuthorId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Harry Potter and the Sorcerer's Stone" },
                    { 2, 2, "A Game of Thrones" },
                    { 3, 3, "The Hobbit" },
                    { 4, 4, "To Kill a Mockingbird" },
                    { 5, 5, "The Great Gatsby" }
                });

            migrationBuilder.InsertData(
                table: "BookDetails",
                columns: new[] { "Id", "BookId", "Description", "PublishedDate", "TotalPages" },
                values: new object[,]
                {
                    { 1, 1, "A young wizard begins his magical journey.", new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 309 },
                    { 2, 2, "Noble families vie for control of the Iron Throne.", new DateTime(1996, 8, 6, 0, 0, 0, 0, DateTimeKind.Utc), 694 },
                    { 3, 3, "A hobbit sets out on a perilous quest to reclaim treasure.", new DateTime(1937, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc), 310 },
                    { 4, 4, "A deep exploration of racism and justice.", new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Utc), 281 },
                    { 5, 5, "A mysterious millionaire and the American dream.", new DateTime(1925, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 180 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookDetails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genre",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
