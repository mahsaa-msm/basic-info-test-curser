using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class addtypetopatterncatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PatternCatalogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatternCatalogs_TenantId_Key_Type",
                table: "PatternCatalogs",
                columns: new[] { "TenantId", "Key", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatternCatalogs_TenantId_Key_Type",
                table: "PatternCatalogs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PatternCatalogs");
        }
    }
}
