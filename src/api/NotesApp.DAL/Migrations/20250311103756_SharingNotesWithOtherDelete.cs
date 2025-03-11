using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SharingNotesWithOtherDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteUser",
                columns: table => new
                {
                    AddedUsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllowedNotesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteUser", x => new { x.AddedUsersId, x.AllowedNotesId });
                    table.ForeignKey(
                        name: "FK_NoteUser_Notes_AllowedNotesId",
                        column: x => x.AllowedNotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteUser_Users_AddedUsersId",
                        column: x => x.AddedUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteUser_AllowedNotesId",
                table: "NoteUser",
                column: "AllowedNotesId");
        }
    }
}
