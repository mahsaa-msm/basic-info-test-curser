using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class servicefeaturekeytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Key",
                table: "ServiceFeatures",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Key",
                table: "ServiceFeatures",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
