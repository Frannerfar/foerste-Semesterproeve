using System.Windows;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Domain.Services
{
    /// <summary>
    ///     Service lag til Activity
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class ActivityService
    {
        public List<Activity> activities;
        public Activity? targetActivity;
        public LocationService locationService;
        public UserService userService;

        /// <summary>
        ///     Constructor til ActivityService
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="locationService"></param>
        /// <param name="userService"></param>
        public ActivityService(LocationService locationService, UserService userService) 
        {
            // Parametrene fra constructoren sættes i objektets fields
            this.locationService = locationService;
            this.userService = userService;
            // Instantierer ny liste af aktiviteter og sættes i objektets field "activities".
            this.activities = new List<Activity>();

            // Her kaldes metoden der laver vores aktiviteter i programmet ved start.
            GenerateDummyActivities();

            // Her kaldes metoden der populater den første aktivitet i listen til maksimale antal deltagere (eller op til 10, hvis ubegrænset)
            // Dette gøres fordi opgaven har som krav, at mindst 1 aktivitet skal være fuld.
            PopulateActivityWithDummyUsers(activities[0]);
            // int starter her som 1 i stedet for 0
            // dette gøres for at populate resten af aktiviteterne med tilfældige brugere og tilfældige antal.
            for (int i = 1; i < activities.Count; i++) 
            {
                // Hver aktivitet populates
                PopulateActivityRandomlyWithUsers(activities[i]);
            }
        }


   

        /// <summary>
        ///     Her oprettes nye aktiviteter
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="title"></param>
        /// <param name="coach"></param>
        /// <param name="location"></param>
        /// <param name="maxCapacity"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public Activity AddActivity(string title, User? coach, Location location, int? maxCapacity, DateTime startTime, DateTime endTime) 
        {
            // Der instantieres et nyt Activity objekt ud fra parameterne modtaget
            Activity newActivity = new Activity(title, coach, location, maxCapacity, startTime, endTime);
            // Den nye aktivitet tilføjes til listen af aktiviteter
            activities.Add(newActivity);
            // returnerer den nye aktivitet
            return newActivity;
        }

        /// <summary>
        ///     Bruges til at fjerne en aktivitet og fjerne alle deltagere på aktiviteten
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="activity"></param>
        public void DeleteActivity(Activity activity)
        {
            // For hver deltager på aktiviteten
            for(int i = 0; i < activity.participants.Count; i++)
            {
                // Fjern denne iterations deltager fra aktiviteten
                LeaveActitvity(activity, activity.participants[i]);
            }
            // Efter alle deltagere er fjernet, fjernes selve aktiviteten fra listen af aktiviteter.
            activities.Remove(activity);
        }

        /// <summary>
        ///     Bruges til at tilføje en bruger til en aktivitet
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="activity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool JoinActivity(Activity activity, User user) 
        {
            // Hvis aktiviteten har ligeså mange deltagere, som aktivitetens maksimale antal
            if(activity.participants.Count >= activity.maxCapacity) 
            {
                MessageBox.Show("No spaces left");
                // returnerer bool for at indikere at brugeren IKKE kunne tilføjes til aktiviteten
                return false;
            }
            // Hvis brugeren allerede er tilmeldt aktiviteten / allerede findes på aktivitetens liste af deltagere
            if(activity.participants.Contains(user))
            {
                MessageBox.Show("You have already joined");
                // returnerer bool for at indikere at brugeren IKKE kunne tilføjes til aktiviteten
                return false;
            }
            // Hvis koden kommer hertil, så kan brugeren godt tilføjes til aktiviteten
            // brugeren tilføjes til aktivitetens liste af deltagere.
            activity.participants.Add(user);
            // aktiviteten tilføjes til brugerens liste af aktiviteter.
            user.activityList.Add(activity);
            // returnerer bool for at indikere at brugeren er tilføjet til aktiviteten
            return true;
        }


        /// <summary>
        ///     Bruges til at fjerne en bruger fra en aktivitet
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="activity"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool LeaveActitvity(Activity activity, User user) 
        {
            // Hvis activity parameterens participants IKKE indeholder user parameteren
            if (!activity.participants.Contains(user)) 
            {
                MessageBox.Show("You can't leave an activity you have not already joined");
                // returnerer bool for at indikere at brugeren IKKE er blevet fjernet fra aktiviteten
                return false;
            }
            // Her kommer vi kun ned, hvis user er tilstede i aktiviteten
            // Derfor fjernes brugeren fra aktivitetens liste af deltagere
            activity.participants.Remove(user);
            // og aktiviteten fjernes fra brugerens liste af aktiviteter.
            user.activityList.Remove(activity);
            // returnerer bool for at indikere at brugeren succesfuldt er blevet fjernet fra aktiviteten
            return true;
        }

        /// <summary>
        ///     Bruges til at lave aktiviteter fra start af programmet.
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
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


        /// <summary>
        ///     Bruges til at tilføje en aktivitet til maksimum antal deltagere, eller hvis ubegrænset, så til de første 10 users.
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="activity"></param>
        private void PopulateActivityWithDummyUsers(Activity activity)
        {
            // Hvis aktiviteten ikke er null (og dermed er begrænset i antal af deltagere)
            if (activity.maxCapacity != null)
            {
                // Vi looper hver 
                for (int i = 0; i < activity.maxCapacity; i++)
                {
                    // Her tilføjes user til aktiviteten
                    // Sikrer os at "Admin Adminsen" ikke bliver tilføjet til listen. Dette gøres kun, for at aktiviteten er fuld
                    // uden at "Admin Adminsen" er med som deltager.
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
        /// <author>Rasmus, Marcus, Martin</author>
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
