using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addinsurancetypecoreservicefeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanViewHistory",
                table: "ServiceFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceTypeCoreId",
                table: "ServiceFeatures",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIssuable",
                table: "ServiceFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanViewHistory",
                table: "ServiceFeatures");

            migrationBuilder.DropColumn(
                name: "InsuranceTypeCoreId",
                table: "ServiceFeatures");

            migrationBuilder.DropColumn(
                name: "IsIssuable",
                table: "ServiceFeatures");
        }
    }
}
