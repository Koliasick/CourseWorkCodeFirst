using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class spAddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPAddUserUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPAddUserDown();
        }
    }
}
