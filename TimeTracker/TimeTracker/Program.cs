using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TimeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection myConnection = new SqlConnection("Data Source = MIECATTS_TOWER\\MIECATT; Initial Catalog = TimeTracker; Integrated Security = True");
            try
            {
                myConnection.Open();
                Console.WriteLine("Successfully connected to SQL Server.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }
    }
}
