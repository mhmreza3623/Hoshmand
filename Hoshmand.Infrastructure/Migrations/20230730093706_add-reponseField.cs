using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoshmand.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addreponseField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "OrderRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponsDate",
                table: "OrderRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "NumPhoneRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponsDate",
                table: "NumPhoneRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "IdCardRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponsDate",
                table: "IdCardRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "CheckCodeRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponsDate",
                table: "CheckCodeRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "OrderRequests");

            migrationBuilder.DropColumn(
                name: "ResponsDate",
                table: "OrderRequests");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "NumPhoneRequests");

            migrationBuilder.DropColumn(
                name: "ResponsDate",
                table: "NumPhoneRequests");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "IdCardRequests");

            migrationBuilder.DropColumn(
                name: "ResponsDate",
                table: "IdCardRequests");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "CheckCodeRequests");

            migrationBuilder.DropColumn(
                name: "ResponsDate",
                table: "CheckCodeRequests");
        }
    }
}
