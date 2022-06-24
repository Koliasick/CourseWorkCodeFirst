using CourseWorkCodeFirst;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class MigrationBuilderExtentions
    {
        private static string GetFile(string filename) 
        {
            string path = ProjectSourcePath.Value + "/SQL/" + filename;
            return File.ReadAllText(path);
        }

        public static void AddAdminUp(this MigrationBuilder migrationBuilder) 
        {
            string sql = GetFile("AddAdminUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AddAdminDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AddAdminDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowAdminToManageUsersDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowAdminToManageUsersDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowAdminToManageUsersUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowAdminToManageUsersUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowModifingBasicUserDataDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowModifingBasicUserDataDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowModifingBasicUserDataUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowModifingBasicUserDataUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowUsersToReportHoursUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowUsersToReportHoursUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void AllowUsersToReportHoursDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("AllowUsersToReportHoursDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPAddAdminRightsDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPAddAdminRightsDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPAddAdminRightsUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPAddAdminRightsUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPAddUserDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPAddUserDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPAddUserUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPAddUserUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPModifyBasicUserDataDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPModifyBasicUserDataDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPModifyBasicUserDataUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPModifyBasicUserDataUp.sql");

            migrationBuilder.Sql(sql);
        }
        public static void SPRemoveAdminRightsDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPRemoveAdminRightsDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPRemoveAdminRightsUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPRemoveAdminRightsUp.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPReportHoursDown(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPReportHoursDown.sql");

            migrationBuilder.Sql(sql);
        }

        public static void SPReportHoursUp(this MigrationBuilder migrationBuilder)
        {
            string sql = GetFile("SPReportHoursUp.sql");

            migrationBuilder.Sql(sql);
        }
    }
}
