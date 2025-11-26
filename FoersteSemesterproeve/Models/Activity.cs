using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Models
{
    class Activity
    {
        public string title;
        public DateTime startTime;
        public DateTime endTime;
        public List <User> users;
        public Location location;
        public int? maxCapacity;
        public int id;
        public List<Membershipstype> members;

        public Activity ()
        {
        
        }
    }
}
