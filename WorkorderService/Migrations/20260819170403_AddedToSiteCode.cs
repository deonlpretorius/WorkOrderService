using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkorderService.Migrations
{
    /// <inheritdoc />
    public partial class AddedToSiteCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WorkOrderEvents");

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderStatusId",
                table: "WorkOrderEvents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SiteCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderEvents_WorkOrderStatusId",
                table: "WorkOrderEvents",
                column: "WorkOrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEvents_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderEvents",
                column: "WorkOrderStatusId",
                principalTable: "WorkOrderStatuses",
                principalColumn: "WorkOrderStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEvents_WorkOrderStatuses_WorkOrderStatusId",
                table: "WorkOrderEvents");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderEvents_WorkOrderStatusId",
                table: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "WorkOrderStatusId",
                table: "WorkOrderEvents");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SiteCodes");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WorkOrderEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
