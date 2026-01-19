using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class agreementobligationentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgreementObligations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AgreementCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrepaymentPercentage = table.Column<double>(type: "float", nullable: true),
                    FirstInstallmentDeadline = table.Column<int>(type: "int", nullable: true),
                    InstallmentsCount = table.Column<int>(type: "int", nullable: true),
                    InstallmentInterval = table.Column<int>(type: "int", nullable: true),
                    AgreementObligationNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgreementNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsuranceTypeCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SalesType = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IssuanceSchemeCoreIds = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
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
                    table.PrimaryKey("PK_AgreementObligations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementObligations_AgreementCoreId",
                table: "AgreementObligations",
                column: "AgreementCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementObligations_BusinessId",
                table: "AgreementObligations",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgreementObligations_CoreId",
                table: "AgreementObligations",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementObligations_InsuranceTypeCoreId",
                table: "AgreementObligations",
                column: "InsuranceTypeCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementObligations_TenantId_CoreId",
                table: "AgreementObligations",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementObligations");
        }
    }
}
