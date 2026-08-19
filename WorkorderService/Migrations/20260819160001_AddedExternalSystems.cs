using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkorderService.Migrations
{
    /// <inheritdoc />
    public partial class AddedExternalSystems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderHistories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderEvents");

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

            migrationBuilder.AddColumn<string>(
                name: "ExternalSystemId",
                table: "WorkOrderEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SiteCodeId",
                table: "WorkOrderEvents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderStatusId",
                table: "WorkOrderEvents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExternalSystems",
                columns: table => new
                {
                    ExternalSystemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalSystemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalSystemDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_ExternalSystemId",
                table: "WorkOrderEvents",
                column: "ExternalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_SiteCodeId",
                table: "WorkOrderEvents",
                column: "SiteCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_WorkOrderStatusId",
                table: "WorkOrderEvents",
                column: "WorkOrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEvents_ExternalSystems_ExternalSystemId",
                table: "WorkOrderEvents",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "ExternalSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEvents_SiteCodes_SiteCodeId",
                table: "WorkOrderEvents",
                column: "SiteCodeId",
                principalTable: "SiteCodes",
                principalColumn: "SiteCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEvents_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderEvents",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_WorkOrderEvents_ExternalSystems_ExternalSystemId",
                table: "WorkOrderEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEvents_SiteCodes_SiteCodeId",
                table: "WorkOrderEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEvents_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderEvents");

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

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderEvents_ExternalSystemId",
                table: "WorkOrderEvents");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderEvents_SiteCodeId",
                table: "WorkOrderEvents");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderEvents_WorkOrderStatusId",
                table: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkOrderStatusId",
                table: "WorkOrderHistories");

            migrationBuilder.DropColumn(
                name: "ExternalSystemId",
                table: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "SiteCodeId",
                table: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "WorkOrderStatusId",
                table: "WorkOrderEvents");

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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WorkOrderEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders",
                column: "SiteCodeId",
                principalTable: "SiteCodes",
                principalColumn: "SiteCodeId");
        }
    }
}
