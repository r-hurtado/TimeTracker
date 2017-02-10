using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker
{
    class Project
    {
        public struct TimeWorked
        {
            //Time is in hours worked that day
            public double time { get; set; }
            public DateTime day { get; set; }
        }

        public struct ProjectUser
        {
            public User user { get; set; }
            public List<TimeWorked> time { get; set; }

            public double getTime()
            {
                double retVal = 0;

                foreach (TimeWorked i in time)
                    retVal += i.time;

                return retVal;
            }
        }

        public double time
        {
            get
            {
                double retVal = 0;

                foreach (ProjectUser i in users)
                    retVal += i.getTime();

                foreach (ProjectUser i in leaders)
                    retVal += i.getTime();

                return retVal;
            }
            set
            {
                time = value;
            }
        }  

        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }     
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public List<ProjectUser> users { get; set; }    
        public List<ProjectUser> leaders { get; set; }         

        public void Create()
        {

        }

        public void Edit()
        {

        }

        public void Delete()
        {

        }
    }
}
