using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<HourlyRate> HourlyRates { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }

        public ApplicationDataContext() : base()
        {
        }

        public ApplicationDataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne((d) => d.Head)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Seed();
        }

        public bool ReportHours(DateTime date, DateTime duration) 
        {
            var dateParameter = new SqlParameter("@Date", System.Data.SqlDbType.DateTime2);
            dateParameter.Value = date;
            var durationParameter = new SqlParameter("@Duration", System.Data.SqlDbType.DateTime2);
            durationParameter.Value = duration;
            var resultParameter = new SqlParameter("@Result", System.Data.SqlDbType.Bit);
            resultParameter.Direction = System.Data.ParameterDirection.Output;


            using (var sqlCommand = this.Database.GetDbConnection().CreateCommand()){

                sqlCommand.CommandText = "ReportHours";
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(dateParameter);
                sqlCommand.Parameters.Add(durationParameter);
                sqlCommand.Parameters.Add(resultParameter);

                this.Database.OpenConnection();
                sqlCommand.ExecuteNonQuery();
            }

            return (bool)resultParameter.Value;
        }

        public void AddAdminRights(string username) 
        {
            var usernameParameter = new SqlParameter("@Username", System.Data.SqlDbType.NVarChar);
            usernameParameter.Value = username;

            this.Database.ExecuteSqlRaw("EXEC AddAdminRights @Username", usernameParameter);
        }

        public void RemoveAdminRights(string username)
        {
            var usernameParameter = new SqlParameter("@Username", System.Data.SqlDbType.NVarChar);
            usernameParameter.Value = username;

            this.Database.ExecuteSqlRaw("EXEC RemoveAdminRights @Username", usernameParameter);
        }

        public void AddUser(string username, string password)
        {
            var usernameParameter = new SqlParameter("@Username", System.Data.SqlDbType.NVarChar);
            usernameParameter.Value = username;
            var passwordParameter = new SqlParameter("@Password", System.Data.SqlDbType.NVarChar);
            passwordParameter.Value = username;

            this.Database.ExecuteSqlRaw("EXEC AddUser @Username, @Password", usernameParameter, passwordParameter);
        }

        public void ModifyBasicUserData(string name, string surname, string middleName, string address, DateTime birthday, string placeOfBirth, string additionalData, byte[] photo) 
        {
            var nameParameter = new SqlParameter("@Name", System.Data.SqlDbType.NVarChar);
            nameParameter.Value = name;
            var surnameParameter = new SqlParameter("@Surname", System.Data.SqlDbType.NVarChar);
            surnameParameter.Value = surname;
            var middlenameParameter = new SqlParameter("@MiddleName", System.Data.SqlDbType.NVarChar);
            middlenameParameter.Value = middleName;
            var addressParameter = new SqlParameter("@Address", System.Data.SqlDbType.NVarChar);
            addressParameter.Value = address;
            var birthdayParameter = new SqlParameter("@Birthday", System.Data.SqlDbType.DateTime2);
            birthdayParameter.Value = birthday;
            var placeOfBirthParameter = new SqlParameter("@PlaceOfBirth", System.Data.SqlDbType.NVarChar);
            placeOfBirthParameter.Value = placeOfBirth;
            var additionalDataParameter = new SqlParameter("@AdditionalData", System.Data.SqlDbType.NVarChar);
            additionalDataParameter.Value = additionalData;
            var photoDataParameter = new SqlParameter("@Photo", System.Data.SqlDbType.VarBinary);
            photoDataParameter.Value = photo;

            List<object> parameters = 
                new List<object> { 
                    nameParameter,
                    surnameParameter,
                    middlenameParameter,
                    addressParameter, 
                    birthdayParameter,
                    placeOfBirthParameter,
                    additionalDataParameter,
                    photoDataParameter
                };

            this.Database.ExecuteSqlRaw("EXEC ModifyBasicUserData @Name, @Surname, @MiddleName, @Address, @Birthday, @PlaceOfBirth, @AdditionalData, @Photo", parameters);
        }
    }
}
