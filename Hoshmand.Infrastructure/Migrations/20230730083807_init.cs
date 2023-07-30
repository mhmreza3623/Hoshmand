using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hoshmand.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckCodeRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderRequestId = table.Column<int>(type: "int", nullable: false),
                    MessageCodeInput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckCodeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckCodeRequests_OrderRequests_OrderRequestId",
                        column: x => x.OrderRequestId,
                        principalTable: "OrderRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdCardRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderRequestId = table.Column<int>(type: "int", nullable: false),
                    ImageId1 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageId2 = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdCardRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdCardRequests_OrderRequests_OrderRequestId",
                        column: x => x.OrderRequestId,
                        principalTable: "OrderRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumPhoneRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderRequestId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumPhoneRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumPhoneRequests_OrderRequests_OrderRequestId",
                        column: x => x.OrderRequestId,
                        principalTable: "OrderRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckCodeRequests_OrderRequestId",
                table: "CheckCodeRequests",
                column: "OrderRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_IdCardRequests_OrderRequestId",
                table: "IdCardRequests",
                column: "OrderRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_NumPhoneRequests_OrderRequestId",
                table: "NumPhoneRequests",
                column: "OrderRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckCodeRequests");

            migrationBuilder.DropTable(
                name: "IdCardRequests");

            migrationBuilder.DropTable(
                name: "NumPhoneRequests");

            migrationBuilder.DropTable(
                name: "OrderRequests");
        }
    }
}
