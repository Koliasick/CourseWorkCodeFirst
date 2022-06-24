using DAL;
using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public partial class AccountInfo : Form
    {
        private Business _business;
        private Mode _mode;
        private string _username;

        public enum Mode
        {
            AdminMode,
            UserMode
        }

        public AccountInfo(Business business, Mode mode, string username, string loggedUsername)
        {
            _mode = mode;
            _username = username;
            _business = business;
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.StartPosition = FormStartPosition.CenterScreen;
            if (mode == Mode.UserMode)
            {
                textBox11.Enabled = false;
                textBox8.Enabled = false;
                comboBox1.Enabled = false;
                panel1.Visible = false;
                panel2.Visible = false;
            }
            else if (username == loggedUsername)
            {
                panel1.Visible = false;
                button3.Visible = false;
                button5.Visible = true;
                button6.Visible = false;
                radioButton1.Checked = false;
            }
            else
            {
                button7.Visible = false;
            }
            PopulateFormsWithData();
        }

        private void PopulateFormsWithData()
        {
            Employee employee = _business.GetEmployeeInfo(_username);

            var positions = _business.GetPositionsList();

            var employees = _business.GetWorkingEployeesList();

            var myWorkHoursOnPositions = _business.GetWorkDays(employee.Id);

            var moneyRecieved = _business.GetPaymentsToEmployee(employee.Id);

            var moneyEarned = _business.CalculateAmountOfMoneyEarnedDuringWorkDays(myWorkHoursOnPositions);

            unapidSalaryLabel.Text = String.Format("{0:C2}", (moneyEarned - moneyRecieved));

            byte[] image = employee.Photo;

            if (image != null && image.Length != 0)
            {
                MemoryStream ms = new MemoryStream(image);
                pictureBox1.Image = Image.FromStream(ms);// now image owns the stream and i should not close it
            }

            textBox1.Text = employee.Name;

            textBox2.Text = employee.Surname;

            textBox3.Text = employee.Middlename;

            textBox4.Text = employee.Address;

            dateTimePicker1.Value = employee.BirthdayDate;

            textBox6.Text = employee.PlaceOfBirth;

            textBox7.Text = employee.AdditionalPassportData;

            textBox8.Text = employee.RegistrationNumber.ToString();

            var position = employee.Positions
                .Where((p) => p.ValidTill == null)
                .First()
                .Position;

            if (position.Department != null)
            {
                textBox5.Text = position.Department.Name;
                textBox9.Text = position.Department.Head.Name;
            }

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(positions.ToArray());

            comboBox1.SelectedItem = positions
                .Find((p) => p.Id == position.Id);

            textBox11.Text = position.HourlyRates
                .Where((hr) => hr.ValidTill == null)
                .Select((hr) => hr.HourlyPayment)
                .First()
                .ToString();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            byte[] image;

            using (MemoryStream ms = new MemoryStream())
            {
                if (pictureBox1.Image == null)
                {
                    image = new byte[] { };
                }
                else
                {
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    image = ms.ToArray();
                }
            }

            if (_mode == Mode.UserMode)
            {
                if (_business.ChangeMyBasicData(
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    dateTimePicker1.Value,
                    textBox6.Text,
                    textBox7.Text,
                    image))
                {
                    MessageBox.Show("Successfully saved changes!");
                }
                else
                {
                    MessageBox.Show("Unable to save changes");
                }
            }
            else
            {
                if (_business.ChangeUserData(
                    _username,
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    dateTimePicker1.Value,
                    textBox6.Text,
                    textBox7.Text,
                    int.Parse(textBox8.Text),
                    (comboBox1.SelectedItem as ComboboxItem).Id,
                    image,
                    (radioButton1.Checked ? Roles.User : Roles.Admin)))
                {
                    MessageBox.Show("Successfully saved changes!");
                }
                else
                {
                    MessageBox.Show("Unable to save changes");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image file|*.png;*.jpeg;*.jpg";
            openFileDialog.Title = "Select new photo";

            DialogResult result = openFileDialog.ShowDialog();
            openFileDialog.RestoreDirectory = true;

            if (result == DialogResult.OK && openFileDialog.FileName != "")
            {
                try
                {
                    if (openFileDialog.CheckPathExists)
                    {
                        pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Given Path does not exist");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            var input = sender as TextBox;
            if (!int.TryParse(input.Text, out _))
            {
                e.Cancel = true;
                errorProvider1.SetError(input, "This field can contain only int values");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportHoursForm(_business)) 
            {
                reportForm.ShowDialog(this);
            }
            PopulateFormsWithData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            new WorkHoursReport(_business, _business.GetEmployeeInfo(_username).Id)
                .ShowDialog();
        }
    }
}
