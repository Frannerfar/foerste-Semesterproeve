using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <author> Rasmus </author>
    public partial class AddActivityPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;

        /// <summary>
        /// 
        /// </summary>
        /// <author> Rasmus </author>
        public AddActivityPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent(); // Loader xaml elementer
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;
            // opretter en tom liste over trænere
            this.coaches = new List<User>();
            // udfylder dropdown med alle brugere der er trænere
            for(int i = 0; i < userService.users.Count; i++)
            {
                if (userService.users[i].isCoach == true)
                {
                    coaches.Add(userService.users[i]);
                    CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                }
            }
            // udfylder dropdown for lokationer
            for (int i = 0; i < locationService.locations.Count; i++)
            {
                string maxCapacityText;
                if(locationService.locations[i].maxCapacity != null) // hvis lokation har kapacitet vises teksten for det
                {
                    maxCapacityText = $"max. {locationService.locations[i].maxCapacity} people";
                }
                else
                {
                    maxCapacityText = $"Unlimited people";
                }
                LocationPicker.Items.Add($"{locationService.locations[i].name} ({maxCapacityText})"); // tilføjer navn og kapacitet til dropdown
            }
            LocationPicker.SelectedIndex = 0; // sætter standard valget for en lokation til den første i listen 

            // sætter standard værdier for dato og tid
            StartDatePicker.SelectedDate = DateTime.Now;
            EndDatePicker.SelectedDate = DateTime.Now.AddDays(1);

            StartTimeBox.Text = "12:00";
            EndTimeBox.Text = "13:00";
        }

        // klik der opretter en aktivitet
        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text)) // tjekker om titlen er udfyldt
            {
                MessageBox.Show("Title is required.");
                return;
            }
            // henter valgt træner, kan godt være null
            User? coach = null;
            if(CoachPicker.SelectedItem != null)
            {
                coach = coaches[CoachPicker.SelectedIndex];
            }

            // henter den valgte lokation     
            Location location = locationService.locations[LocationPicker.SelectedIndex];

            bool isUnlimited = false; // bruges hvis der ikke er nogen begræsning på 
            int tempMaxCap = 0;
            // Hvis feltet er tomt, tjekker om lokation tillader ubegrænset, hvis ikke Messagebox bliver vist 
            if(string.IsNullOrEmpty(CapacityBox.Text))
            {
                if(location.maxCapacity != null)
                {
                    MessageBox.Show("This location doesn't support unlimited people attending");
                    return;
                }
                isUnlimited = true; // aktiviteten får ingen kapacitet 
            }
            // hvis der er indtastet en kapacitet, tjekker om det er et tal og hvis der er plads på aktiviteten 
            if(!isUnlimited)
            {
                if (!int.TryParse(CapacityBox.Text, out tempMaxCap))
                {
                    MessageBox.Show("Max capacity must be a number.");
                    return;
                }
                if(tempMaxCap > location.maxCapacity)
                {
                    MessageBox.Show("Max capacity is higher than room  capacity");
                    return;
                }
            }

            // validerer om brugeren har valgt dato og tid, hvis ikke bliver der vist messagebox
            bool isStartDate = DateTime.TryParse(StartDatePicker.Text, out DateTime actualStartDateTime);
            bool isEndDate = DateTime.TryParse(EndDatePicker.Text, out DateTime actualEndDateTime);

            if (!isStartDate)
            {
                MessageBox.Show("Please input a valid start date!");
                return;
            }

            if (!isEndDate)
            {
                MessageBox.Show("Please input a valid end date!");
                return;
            }
            //konverterer datoer om til DateOnly for at kunne kombinere med TimeOfBirth
            DateOnly onlyStartDate = DateOnly.FromDateTime(actualStartDateTime);
            DateOnly onlyEndDate = DateOnly.FromDateTime(actualEndDateTime);
            // valider tiden 
            bool isStartTime = TimeOnly.TryParse(StartTimeBox.Text, out TimeOnly actualStartTime);
            bool isEndTime = TimeOnly.TryParse(EndTimeBox.Text, out TimeOnly actualEndTime);

            if (!isStartTime)
            {
                MessageBox.Show("Please input a valid start time!");
                return;
            }

            if (!isEndTime)
            {
                MessageBox.Show("Please input a valid end time!");
                return;
            }
            // kombiner dato og tid til DateTime objekt
            DateTime startDateTime = onlyStartDate.ToDateTime(actualStartTime);
            DateTime endDateTime = onlyEndDate.ToDateTime(actualEndTime);

            if (endDateTime <= startDateTime) // tjekker om slut tiden er efter start tidspunkt
            {
                MessageBox.Show("End time must be after start time.");
                return;
            }


            // bliver oprettet ny aktivitet gennem ActivityService
            int? actualMaxCap = null;
            if(!isUnlimited)
            {
                actualMaxCap = tempMaxCap;
            }// tilføjer aktiviteten med de indtastede værdier
            activityService.AddActivity(TitleBox.Text, coach, location, actualMaxCap, startDateTime, endDateTime);
            router.Navigate(NavigationRouter.Route.Activities); // Navigere til oversigten af aktiviteter

        }
        // navigere til aktivitetsoversigten hvis cancel knappen bliver trykket
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }
    }
}
