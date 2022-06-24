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
    public partial class LoginForm : Form
    {
        private Func<string, string, Task<(bool result, string message)>> _loggingIn;
        private Task _currentlyLogging;

        public LoginForm(Func<string, string, Task<(bool result, string message)>> loginAttemptHandler)
        {
            InitializeComponent();
            _loggingIn += loginAttemptHandler;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentlyLogging != null && !_currentlyLogging.IsCompleted)
            {
                return;
            }

            if (textBox1.Text.Length == 0)
            {
                errorMessage.Text = "Please enter Username";
                return;
            }
            if (textBox2.Text.Length == 0)
            {
                errorMessage.Text = "Please enter Password";
                return;
            }

            var color = errorMessage.ForeColor;
            errorMessage.ForeColor = Color.Black;
            errorMessage.Text = "Please wait trying to establish connection with database";

            _currentlyLogging = _loggingIn.Invoke(textBox1.Text, textBox2.Text)
                .ContinueWith((task) =>
                {
                    var result = task.Result;
                    if (result.result)
                    {
                        this.Invoke(() => { this.Close(); });
                    }
                    else
                    {
                        errorMessage.Invoke(() => {
                            errorMessage.ForeColor = color;
                            errorMessage.Text = result.message; 
                        });
                    }
                });
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            errorMessage.Text = string.Empty;
        }
    }
}
