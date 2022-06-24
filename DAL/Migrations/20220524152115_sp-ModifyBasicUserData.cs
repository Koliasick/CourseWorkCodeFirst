using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class spModifyBasicUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPModifyBasicUserDataUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPModifyBasicUserDataDown();
        }
    }
}
