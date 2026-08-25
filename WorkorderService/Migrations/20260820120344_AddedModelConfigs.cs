using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "SiteCodeId",
                table: "WorkOrders",
                newName: "SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_SiteCodeId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "ExternalSystemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteId",
                table: "WorkOrders",
                column: "SiteId",
                principalTable: "SiteCodes",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "WorkOrders",
                newName: "SiteCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_SiteId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_SiteCodeId");

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
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
