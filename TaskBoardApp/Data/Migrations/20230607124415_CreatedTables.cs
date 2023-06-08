using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class CreatedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9caffa99-6ecf-4220-aa4b-1ecc4d015c82", 0, "a4a013b3-578f-4a08-b490-7d8c91412e03", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEKkBSgaCalyGCglJfRF1vBhvR/fKZS4EXHpHhsiv4vl37fnZfxPmQAvXSrQp1hdknw==", null, false, "5aef583f-3ab2-4561-b7e4-6cd0eafb5f65", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 11, 19, 15, 44, 15, 83, DateTimeKind.Local).AddTicks(6828), "Implement better styling for all public pages", "9caffa99-6ecf-4220-aa4b-1ecc4d015c82", "Improve CSS Styles" },
                    { 2, 1, new DateTime(2023, 6, 2, 15, 44, 15, 83, DateTimeKind.Local).AddTicks(6867), "Create Android Client App for the TaskBoard RESTful API", "9caffa99-6ecf-4220-aa4b-1ecc4d015c82", "Android Client App" },
                    { 3, 2, new DateTime(2023, 6, 6, 15, 44, 15, 83, DateTimeKind.Local).AddTicks(6870), "Create Windows Forms desktop app client for the TaskBoard RESTfull API", "9caffa99-6ecf-4220-aa4b-1ecc4d015c82", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 6, 6, 15, 44, 15, 83, DateTimeKind.Local).AddTicks(6871), "Implement [Create Task] page for adding new tasks", "9caffa99-6ecf-4220-aa4b-1ecc4d015c82", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9caffa99-6ecf-4220-aa4b-1ecc4d015c82");
        }
    }
}
