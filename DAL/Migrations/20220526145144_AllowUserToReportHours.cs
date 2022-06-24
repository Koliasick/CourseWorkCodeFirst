using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AllowUserToReportHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowUsersToReportHoursUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AllowUsersToReportHoursDown();
        }
    }
}
