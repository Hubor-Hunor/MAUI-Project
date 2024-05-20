using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkinOut.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "PasswordSalt", "SecurityStamp" },
                values: new object[] { new Guid("9d410699-42a8-4574-94fb-8accb1f38a1a"), "46821227-ff22-475c-bba4-67b0c42b0300", "AQAAAAIAAYagAAAAENX5iyH/nBeDK9O8yAEh0seK6NSRuZM/kUB/a4OwIALUPiQ1jSys9k3ig+8b/PCa+Q==", "", "18fc0faf-adf7-4a8b-845d-504a98445726" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "153aa2f9-3536-469d-81a4-6878448434a5",
                columns: new[] { "AccountId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { new Guid("4efb838c-bdde-4fb6-8ab1-68eee31e1429"), "0d663583-0f5f-4d86-a6fa-574ce54d9746", "AQAAAAIAAYagAAAAEO1Gt+IY7EAutoHchLE8ba2y4CPFFceQYIOJNHFVfYfPtVZHeg77SfpOsZ6CqmJPqg==", "144dccda-4dbf-494b-b4b1-d0684f629d04" });
        }
    }
}
