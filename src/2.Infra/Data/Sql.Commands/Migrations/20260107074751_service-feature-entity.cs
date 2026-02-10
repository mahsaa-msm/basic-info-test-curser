using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class servicefeatureentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceFeatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FeatureName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_ServiceFeatures", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeatures_BusinessId",
                table: "ServiceFeatures",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeatures_Key",
                table: "ServiceFeatures",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeatures_TenantId_Key",
                table: "ServiceFeatures",
                columns: new[] { "TenantId", "Key" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceFeatures");
        }
    }
}
