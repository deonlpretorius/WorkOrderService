using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkOrderHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders");

            migrationBuilder.CreateTable(
                name: "WorkOrderHistories",
                columns: table => new
                {
                    WorkOrderHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderHistories", x => x.WorkOrderHistoryId);
                    table.ForeignKey(
                        name: "FK_WorkOrderHistories_WorkOrderStatuses_WorkOrderStatusId",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "WorkOrderStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderHistories_WorkOrders_WorkOrderHistoryId",
                        column: x => x.WorkOrderHistoryId,
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderHistories_WorkOrderStatusId",
                table: "WorkOrderHistories",
                column: "WorkOrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "ExternalSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrders");

            migrationBuilder.DropTable(
                name: "WorkOrderHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId",
                principalTable: "ExternalSystems",
                principalColumn: "ExternalSystemId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders",
                column: "SiteId",
                principalTable: "Sites",
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
    }
}
