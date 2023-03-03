using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoList.DatabaseManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "todos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    is_done = table.Column<bool>(type: "boolean", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_todos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    todo_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_todos_todo_id",
                        column: x => x.todo_id,
                        principalTable: "todos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "todos",
                columns: new[] { "id", "category", "color", "is_done", "title" },
                values: new object[,]
                {
                    { 1, "Analytics", "Red", false, "Create a ticket" },
                    { 2, "Bookkeeping", "Green", false, "Request information" }
                });

            migrationBuilder.InsertData(
                table: "comments",
                columns: new[] { "id", "text", "todo_id" },
                values: new object[,]
                {
                    { 1, "comment 1", 1 },
                    { 2, "comment 2", 1 },
                    { 3, "comment 3", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_comments_todo_id",
                table: "comments",
                column: "todo_id");

            migrationBuilder.CreateIndex(
                name: "ix_todos_title_category",
                table: "todos",
                columns: new[] { "title", "category" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "todos");
        }
    }
}
