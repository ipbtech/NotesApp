using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseLike_Comment_CommentId",
                table: "BaseLike");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseLike_Note_NoteId",
                table: "BaseLike");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseLike_User_UserId",
                table: "BaseLike");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseSubscription_Tag_TagId",
                table: "BaseSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseSubscription_User_TargetUserId",
                table: "BaseSubscription");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseSubscription_User_UserId",
                table: "BaseSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseSubscription",
                table: "BaseSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseLike",
                table: "BaseLike");

            migrationBuilder.RenameTable(
                name: "BaseSubscription",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "BaseLike",
                newName: "Like");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSubscription_UserId",
                table: "Subscription",
                newName: "IX_Subscription_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSubscription_TargetUserId",
                table: "Subscription",
                newName: "IX_Subscription_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseSubscription_TagId",
                table: "Subscription",
                newName: "IX_Subscription_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseLike_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseLike_NoteId",
                table: "Like",
                newName: "IX_Like_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseLike_CommentId",
                table: "Like",
                newName: "IX_Like_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Comment_CommentId",
                table: "Like",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Note_NoteId",
                table: "Like",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Tag_TagId",
                table: "Subscription",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_TargetUserId",
                table: "Subscription",
                column: "TargetUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_User_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_Comment_CommentId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Note_NoteId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_User_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Tag_TagId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_TargetUserId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_User_UserId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "BaseSubscription");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "BaseLike");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_UserId",
                table: "BaseSubscription",
                newName: "IX_BaseSubscription_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_TargetUserId",
                table: "BaseSubscription",
                newName: "IX_BaseSubscription_TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_TagId",
                table: "BaseSubscription",
                newName: "IX_BaseSubscription_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "BaseLike",
                newName: "IX_BaseLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_NoteId",
                table: "BaseLike",
                newName: "IX_BaseLike_NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_CommentId",
                table: "BaseLike",
                newName: "IX_BaseLike_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseSubscription",
                table: "BaseSubscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseLike",
                table: "BaseLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseLike_Comment_CommentId",
                table: "BaseLike",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseLike_Note_NoteId",
                table: "BaseLike",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseLike_User_UserId",
                table: "BaseLike",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSubscription_Tag_TagId",
                table: "BaseSubscription",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSubscription_User_TargetUserId",
                table: "BaseSubscription",
                column: "TargetUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSubscription_User_UserId",
                table: "BaseSubscription",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
