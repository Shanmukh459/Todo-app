using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ConflictingScheduledTimeException : Exception
    {
        public override string Message
        {
            get
            {
                return "You already have a Task at this time";
            }
        }
    }
    class WindowClosedException : Exception
    {
        public override string Message
        {
            get
            {
                return "oops form got closed!!";
            }
        }
    }
    public class IncorrectInputException : Exception
    {
        string ErrMessage;
        public IncorrectInputException(String Message) 
        {
            ErrMessage = Message;
        }
        public override string Message
        {
            get
            {
                return ErrMessage;
            }
        }
    }
}
