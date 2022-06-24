using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new 
                {
                Id = 1,
                Name = "Admin",
                Surname = "Admin",
                Middlename = "Admin",
                Address = "Admin address",
                BirthdayDate = new DateTime(2022,04,21),
                PlaceOfBirth = "Uknown",
                AdditionalPassportData = "No data",
                RegistrationNumber = 16868,
                Username = "AdminUser",
                Role = Roles.Admin
                }
            );

            modelBuilder.Entity<EmployeePosition>().HasData(
                new
                {
                    Id = 1,
                    EmployeeId = 1,
                    PositionId = 1,
                }
            );

            modelBuilder.Entity<HourlyRate>().HasData(
                new {
                    Id = 1,
                    HourlyPayment = 10.0f,
                    PositionId = 1
                },
                new 
                {
                    Id = -1,
                    HourlyPayment = 0f,
                    PositionId = -1
                }
            );

            modelBuilder.Entity<Position>().HasData(
                new
                {
                    Id = 1,
                    Name = "SysAdmin",
                    DepartmentId = 1
                },
                new 
                {
                    Id = -1,
                    Name = "Fired",
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new {
                    Id = 1,
                    Name = "It",
                    HeadId = 1,
                    Code = 858848
                }
            );

            modelBuilder.Entity<WorkTime>().HasData(
                new {
                    Id = 1,
                    EmployeeId = 1,
                    Date = new DateTime(2022, 04, 21),
                    WorkDuration = new DateTime(2022, 04, 21) + new TimeSpan(0,3,12,0)
                }
            );

            var yesterday = new DateTime(2022, 04, 19);
            yesterday = yesterday - new TimeSpan(1,0,0,0);
            modelBuilder.Entity<WorkTime>().HasData(
                new
                {
                    Id = 2,
                    EmployeeId = 1,
                    Date = yesterday,
                    WorkDuration = yesterday
                }
            );
        }
    }
}
