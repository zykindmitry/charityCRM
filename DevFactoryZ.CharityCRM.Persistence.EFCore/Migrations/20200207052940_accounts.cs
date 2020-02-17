using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Migrations
{
    public partial class accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PasswordConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxLifeTime = table.Column<int>(nullable: false),
                    MinLength = table.Column<int>(nullable: false),
                    SaltLength = table.Column<int>(nullable: false),
                    SpecialSymbols = table.Column<string>(nullable: false),
                    UseDigits = table.Column<bool>(nullable: false),
                    UseSpecialSymbols = table.Column<bool>(nullable: false),
                    UseUpperCase = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 15, nullable: false),
                    Password_PasswordConfigId = table.Column<int>(nullable: true),
                    Password_RawSalt = table.Column<byte[]>(nullable: true),
                    Password_RawHash = table.Column<byte[]>(nullable: true),
                    Password_ChangedAt = table.Column<DateTime>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_PasswordConfig_Password_PasswordConfigId",
                        column: x => x.Password_PasswordConfigId,
                        principalTable: "PasswordConfig",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    UserAgent = table.Column<string>(nullable: false),
                    IPAddress = table.Column<string>(nullable: false),
                    ExpiredAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSession_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Password_PasswordConfigId",
                table: "Account",
                column: "Password_PasswordConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSession_AccountId",
                table: "AccountSession",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSession");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "PasswordConfig");
        }
    }
}
