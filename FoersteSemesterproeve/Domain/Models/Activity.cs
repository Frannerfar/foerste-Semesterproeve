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
        //public DateTime startTime;
        //public DateTime endTime;
        public List<User> participants;
        //public Location location;
        public int? maxCapacity;
        //public int id;
        //public List<Membershipstype> members;

        public Activity ()
        {
            participants = new List<User> ();
            maxCapacity = 10;
            title = "hula bula";
        }
    }
}
