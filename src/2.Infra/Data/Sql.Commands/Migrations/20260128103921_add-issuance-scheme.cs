using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addissuancescheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IssuanceSchemes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    CoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FromStartDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToStartDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromIssueDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToIssueDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuranceTypeCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdjustmentType = table.Column<int>(type: "int", nullable: true),
                    AdjustmentPercent = table.Column<double>(type: "float", nullable: true),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedByUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    TenantBusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuanceSchemes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssuanceSchemes_BusinessId",
                table: "IssuanceSchemes",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IssuanceSchemes_CoreId",
                table: "IssuanceSchemes",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuanceSchemes_InsuranceTypeCoreId",
                table: "IssuanceSchemes",
                column: "InsuranceTypeCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_IssuanceSchemes_TenantId_CoreId",
                table: "IssuanceSchemes",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssuanceSchemes");
        }
    }
}

