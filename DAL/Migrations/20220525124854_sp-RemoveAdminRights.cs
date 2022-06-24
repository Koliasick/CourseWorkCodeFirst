using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class spRemoveAdminRights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPRemoveAdminRightsUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPRemoveAdminRightsDown();
        }
    }
}
