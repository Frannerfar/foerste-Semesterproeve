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
        public User? coach { get; set; }
        public Location location { get; set; }
        public int? maxCapacity {  get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<User> participants { get; set; }
        

        public Activity (string title, User? coach, Location location, int? maxCapacity, DateTime startTime, DateTime endTime)
        {
            this.title = title;
            this.coach = coach;
            this.maxCapacity = maxCapacity;
            this.startTime = startTime;
            this.endTime = endTime;
            this.location = location;
            this.participants = new List<User> ();
        }
    }
}
