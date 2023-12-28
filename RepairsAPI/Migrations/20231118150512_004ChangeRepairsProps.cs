using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairs.API.Migrations
{
    /// <inheritdoc />
    public partial class _004ChangeRepairsProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_IdentityUser_IssuedById",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_IdentityUser_RepairManId",
                table: "Repairs");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_IssuedById",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RepairManId",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "RepairManId",
                table: "Repairs");

            migrationBuilder.AlterColumn<Guid>(
                name: "IssuedById",
                table: "Repairs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FixedById",
                table: "Repairs",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedById",
                table: "Repairs");

            migrationBuilder.AlterColumn<string>(
                name: "IssuedById",
                table: "Repairs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepairManId",
                table: "Repairs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_IssuedById",
                table: "Repairs",
                column: "IssuedById");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RepairManId",
                table: "Repairs",
                column: "RepairManId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_IdentityUser_IssuedById",
                table: "Repairs",
                column: "IssuedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_IdentityUser_RepairManId",
                table: "Repairs",
                column: "RepairManId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
