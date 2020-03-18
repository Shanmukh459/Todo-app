using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    class MySQL:IDisposable
    {
        private MySqlConnection Connection;
        private string Database;
        private string Username;
        private string Pw;
        private string server;
        public MySQL()
        {
            Initialize();
            
        }
        private void Initialize()
        {
            server = "localhost";
            Database = "test";
            Username = "root";
            Pw = "root";
            string ConnectionString = "Server=" + server + ";" + "Database=" + Database + ";" + "Uid=" + Username + ";" + "Password=" + Pw + ";";
            Connection = new MySqlConnection(ConnectionString);
        }
        private bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch(MySqlException e)
            {
                switch(e.Number)
                {
                    case 0:
                        MessageBox.Show("Couldn't connect to the server...");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
            
        }
        private bool CloseConnection()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch(MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
        public void Update()
        {

        }
        public void Insert(DateTime date,string Text,TimeSpan Time)
        {
            string Tname = GetTableName(date);
            
            string Command = "insert into "+Tname+" values('"+Text+"','"+Time.ToString(@"hh\:mm\:ss")+"')";
            if (this.OpenConnection() == true)
            {

                if (!IstablePresent(GetTableName(date)))
                {
                    CreateTable(date);                    
                }
                MySqlCommand Cmd = new MySqlCommand(Command, Connection);
                Cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        public void Delete(DateTime date,TimeSpan Time)
        {
            string Tname = GetTableName(date);
            
            if(this.OpenConnection()==true)
            {

                string Command = "delete from "+Tname+" where Scheduledtime='"+Time.ToString(@"hh\:mm\:ss")+"';";
                if(IsRowsPresent(Tname))
                {
                    MySqlCommand Cmd = new MySqlCommand(Command, Connection);
                    Cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
            }
            else
            {
                this.CloseConnection();
            }
        }
        public bool IsRowsPresent(string Tname)
        {
            int NORows = CountRows(Tname);
            if (NORows != 0)
            {
                return true;
            }
            else
                return false;

        }
        /*public void UpdateDb()
        {
            string Command = "update table";
            if(this.OpenConnection()==true)
            {
                MySqlCommand Cmd = new MySqlCommand(Command, Connection);
                Cmd.ExecuteNonQuery();
                this.CloseConnection();

            }

        }*/
        public List<SingleTask> Select(DateTime date)
        {
            string Tname = GetTableName(date);
            List<SingleTask> Columns = new List<SingleTask>();
            if (this.OpenConnection()==true)
            {

                string Command = "select * from "+Tname+";";
                
                if (IstablePresent(Tname))
                {

                    MySqlCommand Cmd = new MySqlCommand(Command, Connection);

                    MySqlDataReader Reader = Cmd.ExecuteReader();
                    while(Reader.Read())
                    {
                        Columns.Add(new SingleTask((string)Reader["task"], (TimeSpan)Reader["ScheduledTime"]));
                    }
                    this.CloseConnection();
                    return Columns;
                }
                
                else
                {
                    this.CloseConnection();
                    return Columns;
                
                }
            }
            return null;
        }
        public string GetTableName(DateTime date)
        {
            
            string Tname = "table"+date.Day.ToString()+date.Month.ToString()+date.Year.ToString() ;
            return Tname;
        }
        public void CreateTable(DateTime date)
        {
            string Tname=GetTableName(date);
            string Command = "Create table " + Tname + " (task varchar(100),ScheduledTime time(0));";
            MySqlCommand Cmd = new MySqlCommand(Command, Connection);
            Cmd.ExecuteNonQuery();

        }
        public bool IstablePresent(string Tname)
        {
            string Command = "select count(*) from "+ Tname + ";";
            
                
                try
                {
                    MySqlCommand Cmd = new MySqlCommand(Command, Connection);
                    Cmd.ExecuteScalar();
                    Debug.WriteLine("till");
                
                }
                catch(MySqlException e)
                {
                    return false;
                }
                return true; 
            
        }
        public int CountRows(string Tname)
        {
            int Count = 0;
            string Command = "Select count(*) from " + Tname;
            if(this.OpenConnection())
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(Command, Connection);
                    Count = cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                catch(Exception e)
                {
                    this.CloseConnection();
                }
                this.CloseConnection();
            }
            return Count;
        }

        public void Dispose()
        {
            this.CloseConnection();
            GC.SuppressFinalize(this);
        }
    }
}
