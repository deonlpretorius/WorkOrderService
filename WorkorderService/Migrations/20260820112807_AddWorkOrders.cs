using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkOrderDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalSystemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SiteCodeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderId);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ExternalSystems_ExternalSystemId",
                        column: x => x.ExternalSystemId,
                        principalTable: "ExternalSystems",
                        principalColumn: "ExternalSystemId");
                    table.ForeignKey(
                        name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                        column: x => x.SiteCodeId,
                        principalTable: "SiteCodes",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "WorkOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ExternalSystemId",
                table: "WorkOrders",
                column: "ExternalSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SiteCodeId",
                table: "WorkOrders",
                column: "SiteCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}
