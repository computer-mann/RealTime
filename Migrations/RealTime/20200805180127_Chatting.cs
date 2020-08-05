using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RealTime.Migrations.RealTime
{
    public partial class Chatting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NumberOfMembers = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.CheckConstraint("CannotExceed283Members", "NumberOfMembers <= 283");
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Question = table.Column<string>(nullable: true),
                    OptionA = table.Column<string>(maxLength: 100, nullable: true),
                    OptionB = table.Column<string>(maxLength: 100, nullable: true),
                    OptionC = table.Column<string>(maxLength: 100, nullable: true),
                    OptionD = table.Column<string>(maxLength: 100, nullable: true),
                    OptionACount = table.Column<int>(nullable: false),
                    OptionBCount = table.Column<int>(nullable: false),
                    OptionCCount = table.Column<int>(nullable: false),
                    OptionDCount = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    For = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    SenderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessages_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicChatters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TopicId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 183, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicChatters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicChatters_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersInGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupMessagesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersInGroups_GroupMessages_GroupMessagesId",
                        column: x => x.GroupMessagesId,
                        principalTable: "GroupMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserDMs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true),
                    LatestMessageId = table.Column<int>(nullable: true),
                    PrincipalUserId = table.Column<Guid>(nullable: false),
                    OtherUserId = table.Column<Guid>(nullable: false),
                    ChattingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserDMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateSent = table.Column<DateTime>(nullable: false),
                    ActualMessage = table.Column<string>(maxLength: 283, nullable: true),
                    SourceID = table.Column<Guid>(maxLength: 50, nullable: false),
                    TargetID = table.Column<Guid>(maxLength: 50, nullable: false),
                    Read = table.Column<bool>(nullable: false),
                    DateRead = table.Column<bool>(nullable: true),
                    ChattingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectMessages_UserToUserDMs_ChattingId",
                        column: x => x.ChattingId,
                        principalTable: "UserToUserDMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_ChattingId",
                table: "DirectMessages",
                column: "ChattingId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_GroupId",
                table: "GroupMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicChatters_TopicId",
                table: "TopicChatters",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroups_GroupId",
                table: "UsersInGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroups_GroupMessagesId",
                table: "UsersInGroups",
                column: "GroupMessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserDMs_LatestMessageId",
                table: "UserToUserDMs",
                column: "LatestMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToUserDMs_DirectMessages_LatestMessageId",
                table: "UserToUserDMs",
                column: "LatestMessageId",
                principalTable: "DirectMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserToUserDMs_ChattingId",
                table: "DirectMessages");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "TopicChatters");

            migrationBuilder.DropTable(
                name: "UsersInGroups");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "UserToUserDMs");

            migrationBuilder.DropTable(
                name: "DirectMessages");
        }
    }
}
