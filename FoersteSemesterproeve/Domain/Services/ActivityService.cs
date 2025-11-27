using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        //Hvordan en activity bliver oprettet når en admin oprettet det:
        public void CreateActivity(Activity activity) 
        {
            activities.Add(activity);
        }
        public bool JoinActivity(Activity activity, User user) 
        {

            if (activity.participants.Count >= activity.maxCapacity || activity.participants.Contains(user)) 
            {
                return false;
            }
            activity.participants.Add(user);
            user.activityList.Add(activity);
            
             return true;
       
            
        }
        public bool LeaveActitvity(Activity activity, User user) 
        {
            if (!activity.participants.Contains(user)) 
            { 
                return false; 
            }
            activity.participants.Remove(user);
            user.activityList.Remove(activity);
            return true;
        }
    }
}
