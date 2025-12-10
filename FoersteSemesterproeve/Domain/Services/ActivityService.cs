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

            PopulateActivityWithDummyUsers(activities[0]);
            for (int i = 1; i < activities.Count; i++) 
            {
                PopulateActivityRandomlyWithUsers(activities[i]);
            }
        }

        
        //Hvordan en activity bliver oprettet når en admin oprettet det:
        public Activity AddActivity(string title, User? coach, Location location, int? maxCapacity, DateTime startTime, DateTime endTime) 
        {
            Activity newActivity = new Activity(title, coach, location, maxCapacity, startTime, endTime);
            activities.Add(newActivity);
            return newActivity;
        }

        public void DeleteActivity(Activity activity)
        {
            activities.Remove(activity);
        }

        public bool JoinActivity(Activity activity, User user) 
        {

            if(activity.participants.Count >= activity.maxCapacity) 
            {
                MessageBox.Show("No spaces left");
                return false;
            }
            if(activity.participants.Contains(user))
            {
                MessageBox.Show("You have already joined");
                return false;
            }
            //if(user.membershipType)
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
                //AddActivity("Test", userService.users[0], locationService.locations[0], 2, DateTime.Now.AddHours(1).AddMinutes(45), DateTime.Now.AddHours(2).AddMinutes(15));
                AddActivity("Spinning med Viggo", null, locationService.locations[0], 6, DateTime.Now.AddHours(6).AddMinutes(0), DateTime.Now.AddHours(2).AddMinutes(15));
                AddActivity("Mordor Maratonløb", null, locationService.locations[0], 7, DateTime.Now.AddHours(9).AddMinutes(20), DateTime.Now.AddHours(2).AddMinutes(45));
                AddActivity("Second Breakfast Yoga", null, locationService.locations[0], 9, DateTime.Now.AddHours(3).AddMinutes(40), DateTime.Now.AddHours(2).AddMinutes(0));
                AddActivity("Kampsport med Hugo", null, locationService.locations[0], 5, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
                AddActivity("'Winter is Coming' Powerlift", null, locationService.locations[0], null, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
                AddActivity("Isengaard CrossFit", null, locationService.locations[0], null, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
                AddActivity("Øksekast med John", null, locationService.locations[0], null, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
                AddActivity("Fellowship gruppetræning", null, locationService.locations[0], null, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
                // Bueskydning hvor vi tilføjer Orlando Bloom som træner (userService.users[8]. Derudover dynamisk tid.
                AddActivity("Bueskydning for meget øvede", userService.users[8], locationService.locations[0], null, DateTime.Now.AddHours(20).AddMinutes(3), DateTime.Now.AddHours(1).AddMinutes(30));
        }


        private void PopulateActivityWithDummyUsers(Activity activity)
        {
            // Hvis aktiviteten ikke er null (og dermed er begrænset i antal af deltagere)
            if (activity.maxCapacity != null)
            {
                // Vi looper hver 
                for (int i = 0; i < activity.maxCapacity; i++)
                {
                    // Her tilføjes user til aktiviteten
                    activity.participants.Add(userService.users[i+1]);
                    // Her tilføjes aktiviteten til user
                    userService.users[i+1].activityList.Add(activity);
                }
            }
            else
            {
                // Populate med de 10 første users
                for (int i = 0; i < 10; i++)
                {
                    // Her tilføjes user til aktiviteten
                    activity.participants.Add(userService.users[i]);
                    // Her tilføjes aktiviteten til user
                    userService.users[i].activityList.Add(activity);
                }
            }
        }

        /// <summary>
        ///     Bruges til at tilføje tilfældige mængder af tilfældige brugere til en aktivitet
        /// </summary>
        /// <author>Martin</author>
        /// <param name="activity"></param>
        private void PopulateActivityRandomlyWithUsers(Activity activity)
        {
            // Instantiering af Random, så vi kan generere tilfældige tal
            Random random = new Random();
            // Variabel vi bruger til at indeholde antallet af users vi tilmelder
            int randomAmountOfPeopleToJoin = 0;

            // HashSet er en unik liste. Vi bruger den til at undgå duplicates af Users, i stedet for en List
            HashSet<User> hashedUsers = new HashSet<User>();

            // Hvis ikke aktiviteten er null (hvis null, så håndterer vi det som ubegrænset mængde)
            if (activity.maxCapacity != null)
            {
                // Vi sætter variablen til at være mellem 1 og aktivitetens maksimum deltagere
                randomAmountOfPeopleToJoin = random.Next(1, (int)activity.maxCapacity);

                // Så længe at listen af unikke brugere er mindre end variablen er sat til, af personer vi gerne vil have tilføjet
                while (hashedUsers.Count < randomAmountOfPeopleToJoin)
                {
                    // Tilfældig user tilføjes til den unikke liste
                    // Her bruges userService.users (som indeholder alle users i programmet) og random objektet til at generere
                    // et tal mellem 0 og antallet af users i programmet
                    hashedUsers.Add(userService.users[random.Next(0, userService.users.Count)]);
                    // Hvis User allerede eksisterer i den unikke liste, så bliver den naturligvis ikke tilføjet - og dermed
                    // fortsætter loopet indtil det har det rigtige antal af unikke User i sig.
                }

                // For hver bruger/User i den unikke liste hashedUsers, til føjer vi aktiviteten til brugeren og brugeren til aktiviteten
                foreach (User user in hashedUsers)
                {
                    // Her tilføjes user til aktiviteten
                    activity.participants.Add(user);
                    // Her tilføjes aktiviteten til user
                    user.activityList.Add(activity);
                }
            }
            // Ubegrænset antal deltagere, hvis aktivitetens maxCapacity ER null
            else
            {
                // Vi sætter variablen til at være mellem 1 og 10, for at have et fornuftigt antal at populate aktiviteten med
                randomAmountOfPeopleToJoin = random.Next(1, 10);

                // Så længe at listen af unikke brugere er mindre end antallet af personer vi gerne vil have tilføjet
                while(hashedUsers.Count < randomAmountOfPeopleToJoin)
                {
                    // Tilfældig user tilføjes til den unikke liste
                    // Her bruges userService.users (som indeholder alle users i programmet) og random objektet til at generere
                    // et tal mellem 0 og antallet af users i programmet
                    hashedUsers.Add(userService.users[random.Next(0, userService.users.Count)]);
                    // Hvis User allerede eksisterer i den unikke liste, så bliver den naturligvis ikke tilføjet - og dermed
                    // fortsætter loopet indtil det har det rigtige antal af unikke User i sig.
                }

                // For hver bruger/User i den unikke liste hashedUsers, til føjer vi aktiviteten til brugeren og brugeren til aktiviteten
                foreach(User user in hashedUsers)
                {
                    // Her tilføjes user til aktiviteten
                    activity.participants.Add(user);
                    // Her tilføjes aktiviteten til user
                    user.activityList.Add(activity);
                }
            }
        }
    }
}
