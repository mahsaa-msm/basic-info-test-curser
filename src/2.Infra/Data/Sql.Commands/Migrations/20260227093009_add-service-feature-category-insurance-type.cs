using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addservicefeaturecategoryinsurancetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServiceFeatureCategory",
                table: "InsuranceTypes",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceFeatureCategory",
                table: "InsuranceTypes");
        }
    }
}

