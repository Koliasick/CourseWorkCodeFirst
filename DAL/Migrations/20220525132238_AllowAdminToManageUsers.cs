using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AllowAdminToManageUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowAdminToManageUsersUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowAdminToManageUsersDown();
        }
    }
}
