using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddAdminUserAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddAdminUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddAdminDown();
        }
    }
}
