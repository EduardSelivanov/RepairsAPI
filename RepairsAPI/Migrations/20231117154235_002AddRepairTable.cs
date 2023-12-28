using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairs.API.Migrations
{
    /// <inheritdoc />
    public partial class _002AddRepairTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RepairName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionRepair = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RepairManId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repair_IdentityUser_IssuedById",
                        column: x => x.IssuedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Repair_IdentityUser_RepairManId",
                        column: x => x.RepairManId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repair_IssuedById",
                table: "Repair",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_RepairManId",
                table: "Repair",
                column: "RepairManId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
