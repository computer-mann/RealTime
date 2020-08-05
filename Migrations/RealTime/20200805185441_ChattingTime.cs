using Microsoft.EntityFrameworkCore.Migrations;

namespace RealTime.Migrations.RealTime
{
    public partial class ChattingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserToUserDMs_ChattingId",
                table: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_ChattingId",
                table: "DirectMessages");

            migrationBuilder.AlterColumn<string>(
                name: "ChattingId",
                table: "DirectMessages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ChattingId",
                table: "DirectMessages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_ChattingId",
                table: "DirectMessages",
                column: "ChattingId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_UserToUserDMs_ChattingId",
                table: "DirectMessages",
                column: "ChattingId",
                principalTable: "UserToUserDMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
