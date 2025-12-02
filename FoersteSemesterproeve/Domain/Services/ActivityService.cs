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
            activities = GenerateDummyActivities();
        }




        
        //Hvordan en activity bliver oprettet når en admin oprettet det:
        public void AddActivity(Activity activity) 
        {
            activities.Add(activity);
        }
        public void DeleteActivity(Activity activity)
        {
            activities.Remove(activity);
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
        public List<Activity> GetAllActivities() 
        { 
            return activities;
        }

        private List<Activity> GenerateDummyActivities()
        {
            return new List<Activity>()
            {
                new Activity()
                {
                    title = "Yoga for fædre",
                    //coach = new User(1, "Klaus", "Klamhugger","","","","",true,false,new DateOnly(),0, true),
                    location = new Location("Sal 1."),
                    maxCapacity = 10,
                    startTime = DateTime.Today.AddHours(17),
                    endTime = DateTime.Today.AddHours(18),
                },
                new Activity()
                {
                    title = "Crossfit",
                    //coach = new User (2, "Birgitte", "Busketrold", "", "", "", "", true, false, new DateOnly(),0, true),
                    location = new Location("Hos din mor"),
                    maxCapacity = 12,
                    startTime = DateTime.Today.AddHours(12),
                    endTime = DateTime.Today.AddHours(14),

                }
            };
        }

    }
}
