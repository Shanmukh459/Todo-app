using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TaskForm : Form
    {
        private DateTime FormDate;
        public TaskForm()
        {
            InitializeComponent();
            FormDate = DateTime.Now;
        }
        private void TaskForm_Load(object sender, EventArgs e)
        {
            DisplayTaskInTextArea(FormDate);
            
        }
        private void TodaysScheduleButton_Click(object sender, EventArgs e)
        {
            dateTimePicker.Value = DateTime.Now;
        }
       /* private void YesterdaysScheduleButton_Click(object sender, EventArgs e)
        {
                dateTimePicker.Value = DateTime.Now.AddDays(-1);
        }*/
        private void AddTaskButton_Click(object sender, EventArgs e)
        { 
            Form2 AddTaskEventHandler = new Form2(FormDate); // creating new form for taking input
            AddTaskEventHandler.ShowDialog(); // this will display the form
            try
            {
                using (SqlInterface task = new SqlInterface(FormDate))
                {
                    task.AddTask(AddTaskEventHandler.EnteredTask, AddTaskEventHandler.SelectedTime);
                }
                dateTimePicker.Value = FormDate.AddMilliseconds(1);
            }
            catch (ConflictingScheduledTimeException CSTE)
            {
                MessageBox.Show(CSTE.Message);
            }
            catch (WindowClosedException)
            {

            }
        }
        private void DeleteTaskButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlInterface task = new SqlInterface(FormDate))
                {
                    DeleteForm DeleteTaskEvent = new DeleteForm();
                    DeleteTaskEvent.ShowDialog();
                    int index = DeleteTaskEvent.IndexofTask;
                    task.DeleteTask(index);
                    DisplayTaskInTextArea(FormDate);
                }   
            } 

            catch (IncorrectInputException IIE)
            {
                MessageBox.Show("Index can be in range [" + 1 + "-" + IIE.Message + "]");
            }
        }
        private void DisplayTaskInTextArea(DateTime date)
        {
            TaskListArea.Clear();
            DateLabel.Text = date.ToShortDateString();
            DayLabel.Text = date.DayOfWeek.ToString();
            using (SqlInterface task = new SqlInterface(FormDate))
            {
                if (task.Tasks != null && task.Tasks.Count!=0)
                {
                    int i = 0;

                    foreach (SingleTask EachTask in task.Tasks)
                    {
                        TaskListArea.AppendText(
                            ++i 
                            + EachTask.TimeCreated.ToString().Substring(0, 8).PadLeft(9 + 15) 
                            + EachTask.Task.PadLeft(EachTask.Task.Length + 25)
                            + "\n"
                            );
                    }
                }
                else
                {
                    
                        DisplayWhenNoTask();
                }
            }
            
        }
        private void DisplayWhenNoTask()
        {
            DeleteTaskButton.Enabled = false;
            TaskListArea.AppendText("Hurray we don't habe any task!!!!");
        }
        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            FormDate = dateTimePicker.Value;
            if (FormDate.Date < DateTime.Now.Date)
            {
                AddTaskButton.Enabled = false;
                DeleteTaskButton.Enabled = false;
            }
            else
            {
                AddTaskButton.Enabled = true;
                DeleteTaskButton.Enabled = true;
            }
            DisplayTaskInTextArea(FormDate);
        }
    }
}