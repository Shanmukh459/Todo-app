using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp1
{
    class Task : IDisposable
    {
        private DateTime taskDateTime;
        private List<SingleTask> tasks;

        internal List<SingleTask> Tasks { get => tasks; set => tasks = value; }
        public DateTime TaskDateTime { get => taskDateTime; set => taskDateTime = value; }

        public Task(DateTime Date)
        {
            TaskDateTime = Date;
            using (BackEnd FileHandler = new BackEnd(Date))
            {
                try
                {
                    Tasks = FileHandler.GetTasks();
                }
                catch (FileNotFoundException)
                {
                    Tasks = null;
                }
            }
        }
        public void AddTask(string Task,TimeSpan ScheduledTime)
        {
            if(Task == null || Task.Length == 0)
            {
                throw new WindowClosedException();
            }
            if (IsConflictingTime(ScheduledTime))
            {
                throw new ConflictingScheduledTimeException();
            }
            SingleTask CurrentSingleTask = new SingleTask(DateTime.Now.TimeOfDay, Task, ScheduledTime); //creating a new task
            using (BackEnd FileHandler = new BackEnd(taskDateTime))
            {
                FileHandler.AddToTaskFile(CurrentSingleTask); // updating teh corresponding file with the current task
            }
        }
        public void DeleteTask(int Index)
        {
            try
            {
                Tasks.RemoveAt(Index - 1);
                
                using (BackEnd FileHandler = new BackEnd(taskDateTime))
                {
                    FileHandler.UpdateTaskFile(this); //(index - 1) because indexing in list is 0 (zero) based
                }
            }
            catch (Exception)
            {
                throw new IncorrectInputException(Tasks.Count.ToString());
            }
        }
        private bool IsConflictingTime(TimeSpan NewScheduledTime)
        {
            foreach(SingleTask EachTask in Tasks)
            {
                if (EachTask.ScheduledTime == NewScheduledTime)
                    return true;
            }
            return false;
        }
        public static Task GetRequiredTasksObject(DateTime Date)
        {
            using (BackEnd FileHandler = new BackEnd(Date))
            {
                if (FileHandler.IsTaskFileExist())
                {
                    return new Task(Date);
                }
                FileHandler.CreateTaskFile();
                return new Task(Date);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}