using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    DonationType = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FundRegistration",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MaxLifeTime = table.Column<TimeSpan>(nullable: false),
                    SucceededAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundRegistration", x => x.Id);
                });

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
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commodity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: true),
                    DonationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commodity_Donation_DonationId",
                        column: x => x.DonationId,
                        principalTable: "Donation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Commodity_DonationId",
                table: "Commodity",
                column: "DonationId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSession");

            migrationBuilder.DropTable(
                name: "Commodity");

            migrationBuilder.DropTable(
                name: "FundRegistration");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "PasswordConfig");
        }
    }
}
