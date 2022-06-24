using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CourseWorkCodeFirst
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            IConfigurationRoot DALconfiguration;

            try
            {
                DALconfiguration = new ConfigurationBuilder()
                      .SetBasePath(ProjectSourcePath.Value)
                      .AddJsonFile("DALsettings.json")
                      .Build();
            }
            catch 
            {
                MessageBox.Show("Unable to read configuration file for data access layer");
                return;
            }

            ApplicationDataContext? applicationDataContext = null;
            string login = string.Empty;

            var loginForm = new LoginForm(
                async (string username, string password) => {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();

                    login = username;

                    string connectionString;
                    try
                    {
                        string connectionStringFormat = DALconfiguration.GetConnectionString("Database");
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendFormat(connectionStringFormat, username, password);
                        connectionString = stringBuilder.ToString();
                    }
                    catch 
                    {
                        return (false, "Error occured during reading or interpreting connection string template");
                    }

                    optionsBuilder.UseSqlServer(connectionString);

                    var context = new ApplicationDataContext(optionsBuilder.Options);

                    if (await context.Database.CanConnectAsync()) 
                    {
                        applicationDataContext = context;
                        return (true,string.Empty); 
                    }
                    return (false, "Unable to connect to database");
                });

            Application.Run(loginForm);

            if (applicationDataContext == null) 
            {
                return;
            }

            var business = new Business(applicationDataContext);

            Application.Run(new AccountInfo(business,AccountInfo.Mode.AdminMode,login,login));
        }
    }
}