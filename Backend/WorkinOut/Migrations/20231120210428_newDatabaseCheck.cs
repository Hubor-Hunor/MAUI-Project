using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkinOut.Migrations
{
    /// <inheritdoc />
    public partial class newDatabaseCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("070a5c41-e29b-4f16-8cce-df42489bd17b"), "ef238955-2d0c-4c53-9055-d773451758a3", "AQAAAAIAAYagAAAAEJ1mQzJvi2UKaPZV0YoqDXmTjC9MRMjkJCcGkn3nbS8UhJAvfHhwvlt78JoHmoJJog==", "0f38580e-486a-4148-b2dd-b854f3942899" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("b9eb6bd3-b387-44f1-b58d-38e5fab7cf92"), "841205c3-43fa-4032-a70a-eb77df084015", "AQAAAAIAAYagAAAAEIon/bm/6ENS9rLKSOhnZaluxU37wRtTm0Vb5BtZBVDklgRlubrHdRVH1rNqnqZz2Q==", "ad5a13bd-0f69-42ba-ad89-d8fb10b161e4" });
        }
    }
}
