using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkorderService.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkOrderEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrderEvents",
                columns: table => new
                {
                    WorkOrderEventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderExternalId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalSystemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderStatusId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkOrderId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderEvents", x => x.WorkOrderEventId);
                    table.ForeignKey(
                        name: "FK_WorkOrderEvents_ExternalSystems_WorkOrderEventId",
                        column: x => x.WorkOrderEventId,
                        principalTable: "ExternalSystems",
                        principalColumn: "ExternalSystemId");
                    table.ForeignKey(
                        name: "FK_WorkOrderEvents_Sites_WorkOrderEventId",
                        column: x => x.WorkOrderEventId,
                        principalTable: "Sites",
                        principalColumn: "SiteId");
                    table.ForeignKey(
                        name: "FK_WorkOrderEvents_WorkOrderStatuses_WorkOrderEventId",
                        column: x => x.WorkOrderEventId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "WorkOrderStatusId");
                    table.ForeignKey(
                        name: "FK_WorkOrderEvents_WorkOrders_WorkOrderEventId",
                        column: x => x.WorkOrderEventId,
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderId");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderEvents");
        }
    }
}
