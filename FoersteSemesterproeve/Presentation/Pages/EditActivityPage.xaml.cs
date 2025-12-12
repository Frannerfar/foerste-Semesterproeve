using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <author> Rasmus </author>
    public partial class EditActivityPage : UserControl
    {// services og navigation
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;
        // Liste over trænere som kan vælges i dropdown
        List<User> coaches;
        public EditActivityPage(NavigationRouter router, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();
            // gemmer reference til services og navigation
            this.router = router;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            this.coaches = new List<User>(); // tom liste der skal fyldes med trænere

            // Fortsæt kun hvis der er valgt en aktivitet at redigere
            if (activityService.targetActivity != null)
            {

                // title viser hvilken aktivitet der redigeres 
                TitleActivity.Text = $"Edit '{activityService.targetActivity.title}'";


                // bliver udfyldt med titlens nuværende titel
                TitleBox.Text = activityService.targetActivity.title;

                // Bliver oprettet dropdown for trænere
                for (int i = 0; i < userService.users.Count; i++)
                {
                    if (userService.users[i].isCoach == true)
                    {
                        coaches.Add(userService.users[i]);
                        CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                    }
                }// sætter træneren, som allerede er tilknyttet aktiviten 
                for (int i = 0; i < coaches.Count; i++) 
                {
                    if (activityService.targetActivity.coach == coaches[i])
                    {
                        CoachPicker.SelectedIndex = i;
                    }
                }



                // Opretter dropdown for lokation 
                for (int i = 0; i < locationService.locations.Count; i++)
                {
                    string maxCapacityText;
                    if (locationService.locations[i].maxCapacity != null)
                    {
                        maxCapacityText = $"max. {locationService.locations[i].maxCapacity} people"; // viser max kapacitet, hvis den pågældende lokation har en 
                    }
                    else
                    {
                        maxCapacityText = $"Unlimited people";
                    }
                    // lokation bliver tilføjet til dropdown 
                    LocationPicker.Items.Add($"{locationService.locations[i].name} ({maxCapacityText})");
                    
                    if (activityService.targetActivity.location == locationService.locations[i])
                    {
                        LocationPicker.SelectedIndex = i;
                    }
                } // hvis ingen lokation er valgt, sættes den første i listen 
                if (LocationPicker.SelectedItem == null)
                {
                    LocationPicker.SelectedIndex = 0;
                }



                // Capacity
                CapacityBox.Text = $"{activityService.targetActivity.maxCapacity}";


                StartDatePicker.SelectedDate = activityService.targetActivity.startTime;
                EndDatePicker.SelectedDate = activityService.targetActivity.endTime;

                StartTimeBox.Text = $"{activityService.targetActivity.startTime.Hour:D2}:{activityService.targetActivity.startTime.Minute:D2}";
                EndTimeBox.Text = $"{activityService.targetActivity.endTime.Hour:D2}:{activityService.targetActivity.endTime.Minute:D2}";

            }

        }
        // navigere til redigering af bruger, når der trykkes på en deltager
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            userService.targetUser = user;

            router.Navigate(NavigationRouter.Route.EditUser);
        }
        // Navigere tilbage til aktivitetsoversigten
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }


        // Lukker uden at gemme ændringer og navigere tilbage til aktivitetsdetalje
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activity);
        }
        // Gemmer foretaget ændringer 
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Location location = locationService.locations[LocationPicker.SelectedIndex];
            User? coach = null; 
            // henter den valgte træner
            
            if(CoachPicker.SelectedItem != null)
            {
                coach = coaches[CoachPicker.SelectedIndex];
            }

            // kapacitet
            bool isUnlimited = false;
            int tempMaxCap = 0;
            //Tjekker om lokationen har en begrænsning på antal 
            if (string.IsNullOrEmpty(CapacityBox.Text))
            {
                if (location.maxCapacity != null)
                {
                    MessageBox.Show("This location doesn't support unlimited people attending");
                    return;
                }
                isUnlimited = true;
            }

            bool isMaxCapNumber = int.TryParse(CapacityBox.Text, out tempMaxCap);
            int? maxCapcacity = null;

            if(!isUnlimited)
            {
                if (isMaxCapNumber)
                {
                    maxCapcacity = tempMaxCap; 
                    //Tjekker om kapaciteten lavere end den kapacitet der er blevet sat og tjekker om det er et tal
                    if(activityService.targetActivity != null && tempMaxCap < activityService.targetActivity.participants.Count)
                    {
                        MessageBox.Show("You can't limit below the current participant count");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Max capacity must be a number.");
                    return;
                    //maxCapcacity = null;
                }
                if (tempMaxCap > location.maxCapacity) // tjekker om kapaciteten overskrider lokation kapacitet
                {
                    MessageBox.Show("Max capacity is higher than room  capacity");
                    return;
                }

            }

            bool isStartDate = DateTime.TryParse(StartDatePicker.Text, out DateTime actualStartDateTime);
            bool isEndDate = DateTime.TryParse(EndDatePicker.Text, out DateTime actualEndDateTime);


            if (!isStartDate)
            {
                MessageBox.Show("Please input a valid date!");
                return;
            }
            if (!isEndDate)
            {
                MessageBox.Show("Please input a valid date!");
                return;
            }

            DateOnly onlyStartDate = DateOnly.FromDateTime(actualStartDateTime);
            DateOnly onlyEndDate = DateOnly.FromDateTime(actualEndDateTime);

            bool isStartTime = TimeOnly.TryParse(StartTimeBox.Text, out TimeOnly actualStartTime);
            bool isEndTime = TimeOnly.TryParse(EndTimeBox.Text, out TimeOnly actualEndTime);

            if (!isStartTime)
            {
                MessageBox.Show("Please input a valid time!");
                return;
            }
            if (!isEndTime)
            {
                MessageBox.Show("Please input a valid time!");
                return;
            }

            DateTime startDateTime = onlyStartDate.ToDateTime(actualStartTime);
            DateTime endDateTime = onlyEndDate.ToDateTime(actualEndTime);
            // gemmer ændringer i aktivitetservice
            if (activityService.targetActivity != null)
            {
                activityService.targetActivity.title = TitleBox.Text;
                activityService.targetActivity.maxCapacity = maxCapcacity;
                activityService.targetActivity.coach = coach;
                activityService.targetActivity.location = location;

                activityService.targetActivity.startTime = startDateTime;
                activityService.targetActivity.endTime = endDateTime;

                router.Navigate(NavigationRouter.Route.Activity); // når alt er gemt, navigeres der til siden med aktivitetens detaljer 
            }
        }
        // ryd valget af træner
        private void ClearTrainerSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            CoachPicker.SelectedItem = null; // nulstiller dropdown med træner, så der ikke er valgt nogen 
        }
        // Slet aktivitet
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(activityService.targetActivity != null)
            {// Dialogbox vises inden sletning 
                DialogBox dialogBox = new DialogBox($"Are you sure that you want to delete '{activityService.targetActivity.title}' ?");
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true) // bekræftigelse af slet aktivitet
                {
                    activityService.DeleteActivity(activityService.targetActivity);
                    router.Navigate(NavigationRouter.Route.Activities);
                }
            }
        }
    }
}
