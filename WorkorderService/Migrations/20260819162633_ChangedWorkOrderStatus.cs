using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkorderService.Migrations
{
    /// <inheritdoc />
    public partial class ChangedWorkOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WorkOrderEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
