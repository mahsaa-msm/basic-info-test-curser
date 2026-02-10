using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class revertaggregaterelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_TenantId_ProvinceCoreId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceUnits_Cities_TenantId_CityCoreId",
                table: "InsuranceUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_TenantId_CountryCoreId",
                table: "Provinces");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Provinces_TenantId_CoreId",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Provinces_TenantId_CountryCoreId",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceUnits_TenantId_CityCoreId",
                table: "InsuranceUnits");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Countries_TenantId_CoreId",
                table: "Countries");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cities_TenantId_CoreId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_TenantId_ProvinceCoreId",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Provinces_TenantId_CoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Countries_TenantId_CoreId",
                table: "Countries",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cities_TenantId_CoreId",
                table: "Cities",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_TenantId_CountryCoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CountryCoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_TenantId_CityCoreId",
                table: "InsuranceUnits",
                columns: new[] { "TenantId", "CityCoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TenantId_ProvinceCoreId",
                table: "Cities",
                columns: new[] { "TenantId", "ProvinceCoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_TenantId_ProvinceCoreId",
                table: "Cities",
                columns: new[] { "TenantId", "ProvinceCoreId" },
                principalTable: "Provinces",
                principalColumns: new[] { "TenantId", "CoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceUnits_Cities_TenantId_CityCoreId",
                table: "InsuranceUnits",
                columns: new[] { "TenantId", "CityCoreId" },
                principalTable: "Cities",
                principalColumns: new[] { "TenantId", "CoreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_TenantId_CountryCoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CountryCoreId" },
                principalTable: "Countries",
                principalColumns: new[] { "TenantId", "CoreId" });
        }
    }
}
