using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkinOut.Migrations
{
    /// <inheritdoc />
    public partial class newDatabaseCheckagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("4efb838c-bdde-4fb6-8ab1-68eee31e1429"), "0d663583-0f5f-4d86-a6fa-574ce54d9746", "AQAAAAIAAYagAAAAEO1Gt+IY7EAutoHchLE8ba2y4CPFFceQYIOJNHFVfYfPtVZHeg77SfpOsZ6CqmJPqg==", "144dccda-4dbf-494b-b4b1-d0684f629d04" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("070a5c41-e29b-4f16-8cce-df42489bd17b"), "ef238955-2d0c-4c53-9055-d773451758a3", "AQAAAAIAAYagAAAAEJ1mQzJvi2UKaPZV0YoqDXmTjC9MRMjkJCcGkn3nbS8UhJAvfHhwvlt78JoHmoJJog==", "0f38580e-486a-4148-b2dd-b854f3942899" });
        }
    }
}
