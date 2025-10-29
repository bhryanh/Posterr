using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Posterr.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserAndPostTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(777)", maxLength: 777, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsRepost = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalPostId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Posts_OriginalPostId",
                        column: x => x.OriginalPostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Utc), "bhryan" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), "barney" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "sergey" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 12, 17, 0, 0, 0, 0, DateTimeKind.Utc), "daisy" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "IsRepost", "OriginalPostId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), "Hello Posterr! This is my first post.", new DateTime(2024, 12, 31, 19, 0, 0, 0, DateTimeKind.Utc), false, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("22222222-2222-2222-2222-222222222222"), "Just joined Posterr! Excited to be here.", new DateTime(2024, 12, 31, 20, 0, 0, 0, DateTimeKind.Utc), false, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333"), "This is a great platform!", new DateTime(2024, 12, 31, 21, 0, 0, 0, DateTimeKind.Utc), false, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("44444444-4444-4444-4444-444444444444"), "Hello Posterr! This is my first post.", new DateTime(2024, 12, 31, 22, 0, 0, 0, DateTimeKind.Utc), true, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OriginalPostId",
                table: "Posts",
                column: "OriginalPostId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
