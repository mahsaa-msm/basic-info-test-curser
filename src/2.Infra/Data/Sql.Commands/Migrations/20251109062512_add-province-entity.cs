using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addprovinceentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Countries_TenantId_CoreId",
                table: "Countries",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_TenantId_CountryCoreId",
                        columns: x => new { x.TenantId, x.CountryCoreId },
                        principalTable: "Countries",
                        principalColumns: new[] { "TenantId", "CoreId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_TenantId_CoreId",
                table: "Countries",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_BusinessId",
                table: "Provinces",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CoreId",
                table: "Provinces",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryCoreId",
                table: "Provinces",
                column: "CountryCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_TenantId_CoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_TenantId_CountryCoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CountryCoreId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Countries_TenantId_CoreId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_TenantId_CoreId",
                table: "Countries");
        }
    }
}
