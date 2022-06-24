using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddedUsersAbilityToCallModifyBasicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowModifingBasicUserDataUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowModifingBasicUserDataUp();
        }
    }
}
