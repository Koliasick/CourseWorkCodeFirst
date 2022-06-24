using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class spAddAdminRights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPAddAdminRightsUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPAddAdminRightsDown();
        }
    }
}
