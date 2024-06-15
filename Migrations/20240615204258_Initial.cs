using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RealTime.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(25)", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateSent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualMessage = table.Column<string>(type: "character varying(283)", maxLength: 283, nullable: true),
                    SenderId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    ReceipientId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    Read = table.Column<bool>(type: "boolean", nullable: false),
                    DateRead = table.Column<bool>(type: "boolean", nullable: true),
                    ChattingId = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "char(25)", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "char(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "char(25)", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    NumberOfMembers = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatorId = table.Column<string>(type: "char(25)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Question = table.Column<string>(type: "text", nullable: true),
                    OptionA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OptionB = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OptionC = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OptionD = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OptionACount = table.Column<int>(type: "integer", nullable: false),
                    OptionBCount = table.Column<int>(type: "integer", nullable: false),
                    OptionCCount = table.Column<int>(type: "integer", nullable: false),
                    OptionDCount = table.Column<int>(type: "integer", nullable: false),
                    CreatorId = table.Column<string>(type: "char(25)", nullable: true),
                    For = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<string>(type: "char(25)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_AppUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "char(25)", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserDMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LatestMessageId = table.Column<int>(type: "integer", nullable: true),
                    PrincipalUserId = table.Column<string>(type: "char(25)", nullable: true),
                    OtherUserId = table.Column<string>(type: "char(25)", nullable: true),
                    ChattingId = table.Column<string>(type: "text", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserDMs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserDMs_AppUsers_OtherUserId",
                        column: x => x.OtherUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserToUserDMs_AppUsers_PrincipalUserId",
                        column: x => x.PrincipalUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserToUserDMs_DirectMessages_LatestMessageId",
                        column: x => x.LatestMessageId,
                        principalTable: "DirectMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    SenderId = table.Column<string>(type: "char(25)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessages_AppUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TopicId = table.Column<int>(type: "integer", nullable: true),
                    Message = table.Column<string>(type: "character varying(183)", maxLength: 183, nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicChatters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicChatters_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsersInGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<string>(type: "char(25)", nullable: true),
                    GroupMessagesId = table.Column<int>(type: "integer", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInGroups_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersInGroups_GroupMessages_GroupMessagesId",
                        column: x => x.GroupMessagesId,
                        principalTable: "GroupMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersInGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_GroupId",
                table: "GroupMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_SenderId",
                table: "GroupMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatorId",
                table: "Polls",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicChatters_TopicId",
                table: "TopicChatters",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatorId",
                table: "Topics",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroups_GroupId",
                table: "UsersInGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroups_GroupMessagesId",
                table: "UsersInGroups",
                column: "GroupMessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInGroups_UserId",
                table: "UsersInGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserDMs_LatestMessageId",
                table: "UserToUserDMs",
                column: "LatestMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserDMs_OtherUserId",
                table: "UserToUserDMs",
                column: "OtherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserDMs_PrincipalUserId",
                table: "UserToUserDMs",
                column: "PrincipalUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "TopicChatters");

            migrationBuilder.DropTable(
                name: "UsersInGroups");

            migrationBuilder.DropTable(
                name: "UserToUserDMs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "GroupMessages");

            migrationBuilder.DropTable(
                name: "DirectMessages");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
