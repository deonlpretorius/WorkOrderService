using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderEvents");

            migrationBuilder.DropTable(
                name: "WorkOrderHistories");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "SiteCodes",
                newName: "SiteCode");

            migrationBuilder.RenameColumn(
                name: "SiteCodeId",
                table: "SiteCodes",
                newName: "SiteId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalSystems");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SiteCodes");

            migrationBuilder.RenameColumn(
                name: "SiteCode",
                table: "SiteCodes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "SiteCodes",
                newName: "SiteCodeId");

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SiteCodeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WorkOrderStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    WorkOrderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderId);
                    table.ForeignKey(
                        name: "FK_WorkOrders_SiteCodes_SiteCodeId",
                        column: x => x.SiteCodeId,
                        principalTable: "SiteCodes",
                        principalColumn: "SiteCodeId");
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderStatuses_WorkOrderStatusId",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "WorkOrderStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "WorkOrderHistories",
                columns: table => new
                {
                    WorkOrderHistoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderHistories", x => x.WorkOrderHistoryId);
                    table.ForeignKey(
                        name: "FK_WorkOrderHistories_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_WorkOrderId",
                table: "WorkOrderEvents",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderHistories_WorkOrderId",
                table: "WorkOrderHistories",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SiteCodeId",
                table: "WorkOrders",
                column: "SiteCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId");
        }
    }
}
