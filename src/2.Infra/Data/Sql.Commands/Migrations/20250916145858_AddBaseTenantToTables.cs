using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Master.Data.Infra.Data.Sql.Commands.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseTenantToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantBusinessId",
                table: "TravelPassengerCountTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "TravelPassengerCountTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantBusinessId",
                table: "TravelDurationTypes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                table: "TravelDurationTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantBusinessId",
                table: "TravelPassengerCountTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TravelPassengerCountTypes");

            migrationBuilder.DropColumn(
                name: "TenantBusinessId",
                table: "TravelDurationTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "TravelDurationTypes");
        }
    }
}
