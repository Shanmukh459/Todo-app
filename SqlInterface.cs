using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class SqlInterface:IDisposable
    {
        public DateTime Date;
        public List<SingleTask> Tasks;
        public SqlInterface(DateTime Date)
        {
            this.Date = Date;
            using (MySQL DatabaseHandler = new MySQL())
            {
                Tasks = DatabaseHandler.Select(Date);
            }
        }
        public void AddTask(string Task,TimeSpan ScheduledTime)
        {
            using (MySQL DatabaseHandler = new MySQL())
            {
                DatabaseHandler.Insert(Date, Task, ScheduledTime);
            }
        }
        public void DeleteTask(int Index)
        {
            using (MySQL DatabaseHandler = new MySQL())
            {
                DatabaseHandler.Delete(Date, Tasks[Index - 1].ScheduledTime);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
