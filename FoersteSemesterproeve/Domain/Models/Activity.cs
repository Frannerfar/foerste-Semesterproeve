using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Models
{
    public class Activity
    {
        public string title;
        //public string description;
        public User coach;
        public Location location;
        public int? maxCapacity;
        public DateTime startTime;
        public DateTime endTime;
        public List<User> participants;
        //public int id;
        //public List<Membershipstype> members;

        public Activity ()
        {
            title = "hula bula";
            participants = new List<User> ();
            maxCapacity = 10;
            startTime = DateTime.Now;
            endTime = DateTime.Now;

        }
    }
}
