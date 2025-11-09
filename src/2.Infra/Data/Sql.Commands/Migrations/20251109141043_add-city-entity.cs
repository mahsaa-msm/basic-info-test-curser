using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addcityentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Provinces_TenantId_CoreId",
                table: "Provinces",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProvinceCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Provinces_TenantId_ProvinceCoreId",
                        columns: x => new { x.TenantId, x.ProvinceCoreId },
                        principalTable: "Provinces",
                        principalColumns: new[] { "TenantId", "CoreId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_BusinessId",
                table: "Cities",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CoreId",
                table: "Cities",
                column: "CoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceCoreId",
                table: "Cities",
                column: "ProvinceCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TenantId_CoreId",
                table: "Cities",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TenantId_ProvinceCoreId",
                table: "Cities",
                columns: new[] { "TenantId", "ProvinceCoreId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Provinces_TenantId_CoreId",
                table: "Provinces");
        }
    }
}
