using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class insuranceunitentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cities_TenantId_CoreId",
                table: "Cities",
                columns: new[] { "TenantId", "CoreId" });

            migrationBuilder.CreateTable(
                name: "InsuranceUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DisplayTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityCoreId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Location = table.Column<Point>(type: "geometry", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_InsuranceUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceUnits_Cities_TenantId_CityCoreId",
                        columns: x => new { x.TenantId, x.CityCoreId },
                        principalTable: "Cities",
                        principalColumns: new[] { "TenantId", "CoreId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_BusinessId",
                table: "InsuranceUnits",
                column: "BusinessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_CityCoreId",
                table: "InsuranceUnits",
                column: "CityCoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_CoreId",
                table: "InsuranceUnits",
                column: "CoreId");

            migrationBuilder.Sql(@"
                CREATE SPATIAL INDEX [IX_InsuranceUnits_Location_Spatial] 
                ON [InsuranceUnits] ([Location])
                WITH (BOUNDING_BOX = (-180, -90, 180, 90))");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_TenantId_CityCoreId",
                table: "InsuranceUnits",
                columns: new[] { "TenantId", "CityCoreId" });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceUnits_TenantId_CoreId",
                table: "InsuranceUnits",
                columns: new[] { "TenantId", "CoreId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP INDEX [IX_InsuranceUnits_Location_Spatial] ON [InsuranceUnits]");

            migrationBuilder.DropTable(
                name: "InsuranceUnits");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cities_TenantId_CoreId",
                table: "Cities");
        }
    }
}

