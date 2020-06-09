using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore.Migrations
{
    public partial class addward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    SurName = table.Column<string>(maxLength: 30, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 30, nullable: true),
                    PostCode = table.Column<string>(maxLength: 6, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Region = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Area = table.Column<string>(maxLength: 50, nullable: true),
                    Street = table.Column<string>(maxLength: 50, nullable: true),
                    House = table.Column<string>(maxLength: 30, nullable: true),
                    Flat = table.Column<string>(maxLength: 10, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Phone = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WardCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WardCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WardCategory_WardCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "WardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WardCategories",
                columns: table => new
                {
                    WardId = table.Column<int>(nullable: false),
                    WardCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WardCategories", x => new { x.WardId, x.WardCategoryId });
                    table.ForeignKey(
                        name: "FK_WardCategories_WardCategory_WardCategoryId",
                        column: x => x.WardCategoryId,
                        principalTable: "WardCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WardCategories_Ward_WardId",
                        column: x => x.WardId,
                        principalTable: "Ward",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WardCategories_WardCategoryId",
                table: "WardCategories",
                column: "WardCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WardCategory_ParentId",
                table: "WardCategory",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WardCategories");

            migrationBuilder.DropTable(
                name: "WardCategory");

            migrationBuilder.DropTable(
                name: "Ward");
        }
    }
}
