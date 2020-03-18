using System;

namespace WindowsFormsApp1
{
    class SingleTask
    {
        private TimeSpan timeCreated;
        private string task;
        private TimeSpan scheduledTime;
        public SingleTask(TimeSpan time_created, string task, TimeSpan time)
        {
            this.TimeCreated = time_created;
            this.Task = task;
            this.ScheduledTime = time; 
        }
        public SingleTask(string task,TimeSpan ScheduledTime)
        {
            this.Task = task;
            this.ScheduledTime = ScheduledTime;
        }
        public TimeSpan ScheduledTime { get => scheduledTime; set => scheduledTime = value; }
        public string Task { get => task; set => task = value; }
        public TimeSpan TimeCreated { get => timeCreated; set => timeCreated = value; }
    }
}
