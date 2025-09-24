using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chit_chat_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUser_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_user_id1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_user_id1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "user_id1",
                table: "Messages");

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Messages",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_user_id1",
                table: "Messages",
                column: "user_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_user_id1",
                table: "Messages",
                column: "user_id1",
                principalTable: "Users",
                principalColumn: "user_id");
        }
    }
}
