using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalTwin.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenamedSiteCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteId",
                table: "WorkOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SiteCodes",
                table: "SiteCodes");

            migrationBuilder.RenameTable(
                name: "SiteCodes",
                newName: "Sites");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Sites_SiteId",
                table: "WorkOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.RenameTable(
                name: "Sites",
                newName: "SiteCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SiteCodes",
                table: "SiteCodes",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_SiteCodes_SiteId",
                table: "WorkOrders",
                column: "SiteId",
                principalTable: "SiteCodes",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
