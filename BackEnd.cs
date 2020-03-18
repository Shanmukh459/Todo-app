using System;
using System.Collections.Generic;
using System.IO;

namespace WindowsFormsApp1
{
    class BackEnd : IDisposable
    {
        private string TaskFilePath = @"C:\Users\Sk185626\Downloads\Todo-app-coderefactored\Todo-app-coderefactored\Tasks\";
        private string TaskFileExt = "-task.txt";
        private DateTime Date;
        public BackEnd(DateTime Date)
        {
            this.Date = Date;
        }
        private string GetTaskFilePath()
        {
            return TaskFilePath + Date.Month + Date.Day + Date.Year + TaskFileExt;
        }
        public bool IsTaskFileExist()
        {
            string Path = GetTaskFilePath();
            return File.Exists(Path);
        }
        private string GetPathAndThrowExeptionIfRequired()
        {
            string Path = GetTaskFilePath();
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException();            
            }
            return Path;
        }
        public void CreateTaskFile()
        {
            string Path = GetTaskFilePath();
            using (StreamWriter SW = new StreamWriter(Path))
            {
                File.SetAttributes(Path, File.GetAttributes(Path) & FileAttributes.ReadOnly);
                SW.WriteLine(Date.ToShortDateString() + "  " + Date.DayOfWeek);
                SW.WriteLine("TIME" + "TASK".PadLeft(20));
            }
        }
        public void AddToTaskFile(SingleTask CurrentSingleTask)
        {
            string Path = GetPathAndThrowExeptionIfRequired();
            using (StreamWriter SW = new StreamWriter(Path,true))
            {
                SW.WriteLine(CurrentSingleTask.TimeCreated +" "+ CurrentSingleTask.Task+" AT "+CurrentSingleTask.ScheduledTime);
            }
        }
        public List<SingleTask> GetTasks()
        {
            string Path = GetPathAndThrowExeptionIfRequired();
            List<SingleTask> AllTask = new List<SingleTask>();
            string CurrTask;
            using (StreamReader SR = new StreamReader(Path))
            {
                CurrTask = SR.ReadLine();
                CurrTask = SR.ReadLine();
                while ((CurrTask = SR.ReadLine()) != null)
                {
                    char[] seperator = { ' ' };
                    string[] tokenize = CurrTask.Split(seperator,2,StringSplitOptions.RemoveEmptyEntries);
                    if(tokenize.Length == 2)
                        try
                        {
                            AllTask.Add(new SingleTask(
                                TimeSpan.Parse(tokenize[0]),
                                tokenize[1],
                                TimeSpan.Parse(tokenize[1].Substring(tokenize[1].Length-8))
                                ));
                        }
                        catch(Exception)
                        {

                        }
                }
            }
            return AllTask;
        }
        public void UpdateTaskFile(Task CurrenTask) 
        {
            DateTime Date = CurrenTask.TaskDateTime;
            string Path = GetPathAndThrowExeptionIfRequired();
            if (CurrenTask.Tasks.Count!=0)
            {
                CreateTaskFile();
                using (StreamWriter SW = new StreamWriter(Path, true))
                {
                    foreach (SingleTask EachTask in CurrenTask.Tasks)
                    {
                        SW.WriteLine(
                            EachTask.TimeCreated.ToString()
                            + " "
                            + EachTask.Task.PadLeft(15)
                            );
                    }
                }

            }
            else
            {
                File.Delete(Path);
            }
                
            
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
    }
    
}