using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkorderService.Migrations
{
    /// <inheritdoc />
    public partial class RemovedWorkOrderEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderHistories");

            migrationBuilder.AlterColumn<string>(
                name: "SiteCodeId",
                table: "WorkOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalSystemId",
                table: "WorkOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderStatusId",
                table: "WorkOrderHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SiteCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ExternalSystems",
                columns: table => new
                {
                    ExternalSystemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalSystemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalSystemDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ExternalSystemCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSystems", x => x.ExternalSystemId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderHistories_WorkOrderStatusId",
                table: "WorkOrderHistories",
                column: "WorkOrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderHistories_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderHistories",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "ExternalSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders",
                column: "SiteCodeId",
                principalTable: "SiteCodes",
                principalColumn: "SiteCodeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderHistories_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "ExternalSystems");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderHistories_WorkOrderStatusId",
                table: "WorkOrderHistories");

            migrationBuilder.DropColumn(
                name: "ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkOrderStatusId",
                table: "WorkOrderHistories");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SiteCodes");

            migrationBuilder.AlterColumn<string>(
                name: "SiteCodeId",
                table: "WorkOrders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "WorkOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WorkOrderHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkOrderEvents",
                columns: table => new
                {
                    WorkOrderEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WorkOrderExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderEvents", x => x.WorkOrderEventId);
                    table.ForeignKey(
                        name: "FK_WorkOrderEvents_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_WorkOrderId",
                table: "WorkOrderEvents",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders",
                column: "SiteCodeId",
                principalTable: "SiteCodes",
                principalColumn: "SiteCodeId");
        }
    }
}
