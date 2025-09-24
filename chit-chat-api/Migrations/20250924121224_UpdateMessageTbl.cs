using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chit_chat_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMessageTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_read",
                table: "Messages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receiver_id",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sender_id",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_user_id",
                table: "Messages",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_user_id",
                table: "Messages",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_user_id",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_user_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "is_read",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "message",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "receiver_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "sender_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Messages");
        }
    }
}
