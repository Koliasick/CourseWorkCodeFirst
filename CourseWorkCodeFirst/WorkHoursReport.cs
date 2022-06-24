using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkCodeFirst
{
    public partial class WorkHoursReport : Form
    {
        private Business _business;
        public WorkHoursReport(Business business, int employeeId)
        {
            InitializeComponent();
            _business = business;
            var workDays = _business.GetWorkDays(employeeId);
            dataGridView1.ReadOnly = true;
            var data = workDays.Select(wd => new
            {
                Date = new DateOnly(wd.Date.Year,wd.Date.Month,wd.Date.Day),
                Duration = new TimeOnly(wd.Duration.Hour,wd.Duration.Minute),
                HourlyRate = wd.HourlyRate,
                Position = wd.Position.Name
            }).ToList();
            dataGridView1.DataSource = data;
        }

    }
}
