using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Domain.Services
{
    public class ActivityService
    {
        public List<Activity> activities;

        public ActivityService() 
        {
            activities = populateActivities();
        }




        private List<Activity> populateActivities()
        {
            List<Activity> activities = new List<Activity>();

            activities.Add(new Activity());
            activities.Add(new Activity());
            activities.Add(new Activity());
            activities.Add(new Activity());
            activities.Add(new Activity());


            return activities;
        }
    }
}
