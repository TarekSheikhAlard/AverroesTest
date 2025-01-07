using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Authentication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05861c25-3c6e-417b-b2b3-8c9dbcf82dcb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51a9724d-b7c4-48a0-84c2-914c4312d28c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9808a7f4-6da7-4072-b97e-b06a69950711");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd14d332-04a0-4316-895f-7ce3458a0a1d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13fa00c2-5f0d-427a-8a8a-b0d01d0b1f66", null, "Customer", "CUSTOMER" },
                    { "32b1bb5e-a23c-4161-95a9-3a8e7d17b7d7", null, "Driver", "DRIVER" },
                    { "95a3f73f-bc21-49cd-b49e-032a435ec888", null, "Admin", "ADMIN" },
                    { "e66d84c9-200c-4ea4-8b91-d39ab14c8b57", null, "CustomerService", "CUSTOMERSERVICE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13fa00c2-5f0d-427a-8a8a-b0d01d0b1f66");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32b1bb5e-a23c-4161-95a9-3a8e7d17b7d7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95a3f73f-bc21-49cd-b49e-032a435ec888");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e66d84c9-200c-4ea4-8b91-d39ab14c8b57");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05861c25-3c6e-417b-b2b3-8c9dbcf82dcb", null, "CustomerService", "CUSTOMERSERVICE" },
                    { "51a9724d-b7c4-48a0-84c2-914c4312d28c", null, "Admin", "ADMIN" },
                    { "9808a7f4-6da7-4072-b97e-b06a69950711", null, "Driver", "DRIVER" },
                    { "bd14d332-04a0-4316-895f-7ce3458a0a1d", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
