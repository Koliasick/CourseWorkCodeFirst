using CourseWorkCodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDataContextFactory : IDesignTimeDbContextFactory<ApplicationDataContext>
    {
        public ApplicationDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(ProjectSourcePath.Value)
                .AddJsonFile("DALsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DeploymentAccess"));

            return new ApplicationDataContext(optionsBuilder.Options);
        }
    }
}
