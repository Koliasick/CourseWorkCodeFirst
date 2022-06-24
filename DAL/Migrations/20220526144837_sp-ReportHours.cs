using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class spReportHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPReportHoursUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SPReportHoursDown();
        }
    }
}
