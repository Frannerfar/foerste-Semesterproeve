using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Models
{
    public class Activity
    {
        public string title { get; set; }
        public User coach { get; set; }
        public Location location { get; set; }
        public int? maxCapacity {  get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<User> participants { get; set; }
        public int participantCount { get; set; }
        

        public Activity ()
        {
            title = "hula bula";
            participants = new List<User> ();
            maxCapacity = 10;
            startTime = DateTime.Now;
            endTime = DateTime.Now.AddHours(1);

        }
    }
}
