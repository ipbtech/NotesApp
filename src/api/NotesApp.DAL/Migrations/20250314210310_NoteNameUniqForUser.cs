using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NoteNameUniqForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId_Name",
                table: "Notes",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notes_UserId_Name",
                table: "Notes");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");
        }
    }
}
