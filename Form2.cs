using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public string EnteredTask;
        public TimeSpan SelectedTime;
        public Form2(DateTime date)
        {
            InitializeComponent();;
            FillHoursComboBox();
            FillMinutesComboBox();
            if(date.Date == DateTime.Now.Date)
            {
                DisableSomeHourOptions();
            }
        }
        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            EnteredTask = TaskInputBox.Text;
            SelectedTime = TimeSpan.Parse(HoursComboBox.Text + ":" + MinutesComboBox.Text, new CultureInfo("en-US"));
            if (EnteredTask.Length == 0)
            {
                label1.Text = "Please Enter some Task!!";
            }
            else
            {
                this.Dispose();
            }
        }

        private void HoursComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}