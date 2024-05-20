using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkinOut.Migrations
{
    /// <inheritdoc />
    public partial class check2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "053aa2f9-5e15-4cbe-81a4-518f320b10fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83ac2b19-50eb-469d-9be1-c007048cccbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0219923-3536-4da3-aeca-6878448434a5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("b9eb6bd3-b387-44f1-b58d-38e5fab7cf92"), "841205c3-43fa-4032-a70a-eb77df084015", "AQAAAAIAAYagAAAAEIon/bm/6ENS9rLKSOhnZaluxU37wRtTm0Vb5BtZBVDklgRlubrHdRVH1rNqnqZz2Q==", "ad5a13bd-0f69-42ba-ad89-d8fb10b161e4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "053aa2f9-5e15-4cbe-81a4-518f320b10fa", null, "Trainer", "TRAINER" },
                    { "83ac2b19-50eb-469d-9be1-c007048cccbf", null, "Admin", "ADMIN" },
                    { "b0219923-3536-4da3-aeca-6878448434a5", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("ccb787fc-874f-4565-b77d-197f6d53284e"), "7a4b6abd-67ae-4d7f-8475-2ad23fdc67e5", "AQAAAAIAAYagAAAAEMuAHs5Wli2a+YYzhrfeKAFbW0aj6LLbdBEaHsq7SLcb9WZkST2B/Ve4VxRke9wvnw==", "58cb10de-8863-4bb3-be11-c0f2f7a187b7" });
        }
    }
}
