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
    public partial class ReportHoursForm : Form
    {
        private Business _business;
        public ReportHoursForm(Business business)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _business = business;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime duration = DateTime.Today;
            double enteredValue = (double)numericUpDown1.Value;
            duration += new TimeSpan((int)enteredValue, (int)((enteredValue - (int)enteredValue) * 60.0), 0);
            if (_business.ReportMyHours(dateTimePicker1.Value, duration))
            {
                MessageBox.Show("Hours reported");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Unable to report rours mauge you trying to report more than 24 hours per day");
            }
        }
    }
}
