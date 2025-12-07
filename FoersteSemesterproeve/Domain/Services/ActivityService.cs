using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Domain.Services
{
    public class ActivityService
    {
        public List<Activity> activities;
        public Activity? targetActivity;
        public LocationService locationService;
        public UserService userService;
        

        public ActivityService(LocationService locationService, UserService userService) 
        {
            this.locationService = locationService;
            this.userService = userService;
            this.activities = new List<Activity>();
            GenerateDummyActivities();
        }

        
        //Hvordan en activity bliver oprettet når en admin oprettet det:
        public void AddActivity(string title, User? coach, Location location, int? maxCapacity, DateTime startTime, DateTime endTime) 
        {
            Activity newActivity = new Activity(title, coach, location, maxCapacity, startTime, endTime);
            activities.Add(newActivity);
        }

        public void DeleteActivity(Activity activity)
        {
            activities.Remove(activity);
        }

        public bool JoinActivity(Activity activity, User user) 
        {

            if (activity.participants.Count >= activity.maxCapacity) 
            {
                MessageBox.Show("No spaces left");
                return false;
            }
            if (activity.participants.Contains(user))
            {
                MessageBox.Show("You have already joined");
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
                MessageBox.Show("You can't leave an activity you have not already joined");
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

        private void GenerateDummyActivities()
        {
            AddActivity("Test", userService.users[7], locationService.locations[0], 2, DateTime.Now.AddHours(1).AddMinutes(45), DateTime.Now.AddHours(2).AddMinutes(15));
            AddActivity("Test1", userService.users[7], locationService.locations[0], 7, DateTime.Now.AddHours(9).AddMinutes(20), DateTime.Now.AddHours(2).AddMinutes(45));
            AddActivity("Test2", userService.users[7], locationService.locations[0], 9, DateTime.Now.AddHours(3).AddMinutes(40), DateTime.Now.AddHours(2).AddMinutes(0));
            AddActivity("Test3", userService.users[7], locationService.locations[0], 3, DateTime.Now.AddHours(6).AddMinutes(0), DateTime.Now.AddHours(2).AddMinutes(15));
            AddActivity("Test4", userService.users[7], locationService.locations[0], 5, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
            AddActivity("Test4", userService.users[7], locationService.locations[0], 0, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
        }

    }
}
