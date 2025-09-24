using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chit_chat_api.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeColumnInMessageTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Messages");
        }
    }
}
