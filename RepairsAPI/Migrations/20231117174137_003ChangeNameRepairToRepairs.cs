using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairs.API.Migrations
{
    /// <inheritdoc />
    public partial class _003ChangeNameRepairToRepairs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repair_IdentityUser_IssuedById",
                table: "Repair");

            migrationBuilder.DropForeignKey(
                name: "FK_Repair_IdentityUser_RepairManId",
                table: "Repair");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repair",
                table: "Repair");

            migrationBuilder.RenameTable(
                name: "Repair",
                newName: "Repairs");

            migrationBuilder.RenameIndex(
                name: "IX_Repair_RepairManId",
                table: "Repairs",
                newName: "IX_Repairs_RepairManId");

            migrationBuilder.RenameIndex(
                name: "IX_Repair_IssuedById",
                table: "Repairs",
                newName: "IX_Repairs_IssuedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_IdentityUser_IssuedById",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_IdentityUser_RepairManId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.RenameTable(
                name: "Repairs",
                newName: "Repair");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_RepairManId",
                table: "Repair",
                newName: "IX_Repair_RepairManId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_IssuedById",
                table: "Repair",
                newName: "IX_Repair_IssuedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repair",
                table: "Repair",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repair_IdentityUser_IssuedById",
                table: "Repair",
                column: "IssuedById",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repair_IdentityUser_RepairManId",
                table: "Repair",
                column: "RepairManId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }
    }
}
