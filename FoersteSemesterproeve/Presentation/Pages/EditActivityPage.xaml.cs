using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditActivityPage.xaml
    /// </summary>
    public partial class EditActivityPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;
        public EditActivityPage(NavigationRouter router, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();

            this.router = router;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            this.coaches = new List<User>();


            if (activityService.targetActivity != null)
            {

                // PAGE TITLE
                TitleActivity.Text = $"Edit '{activityService.targetActivity.title}'";



                TitleBox.Text = activityService.targetActivity.title;

                // Trainer
                for (int i = 0; i < userService.users.Count; i++)
                {
                    if (userService.users[i].isCoach == true)
                    {
                        coaches.Add(userService.users[i]);
                        CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                    }
                }
                for (int i = 0; i < coaches.Count; i++)
                {
                    if (activityService.targetActivity.coach == coaches[i])
                    {
                        CoachPicker.SelectedIndex = i;
                    }
                }



                // Location
                for (int i = 0; i < locationService.locations.Count; i++)
                {
                    string maxCapacityText;
                    if (locationService.locations[i].maxCapacity != null)
                    {
                        maxCapacityText = $"max. {locationService.locations[i].maxCapacity} people";
                    }
                    else
                    {
                        maxCapacityText = $"Unlimited people";
                    }
                    //LocationPicker.Items.Add(locationService.locations[i].name);
                    LocationPicker.Items.Add($"{locationService.locations[i].name} ({maxCapacityText})");

                    if (activityService.targetActivity.location == locationService.locations[i])
                    {
                        LocationPicker.SelectedIndex = i;
                    }
                }
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

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            userService.targetUser = user;

            router.Navigate(NavigationRouter.Route.EditUser);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activity);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Location location = locationService.locations[LocationPicker.SelectedIndex];
            User? coach = null;
            
            if(CoachPicker.SelectedItem != null)
            {
                coach = coaches[CoachPicker.SelectedIndex];
            }


            bool isUnlimited = false;
            int tempMaxCap = 0;
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
                if (tempMaxCap > location.maxCapacity)
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

            if (activityService.targetActivity != null)
            {
                activityService.targetActivity.title = TitleBox.Text;
                activityService.targetActivity.maxCapacity = maxCapcacity;
                activityService.targetActivity.coach = coach;
                activityService.targetActivity.location = location;

                activityService.targetActivity.startTime = startDateTime;
                activityService.targetActivity.endTime = endDateTime;

                router.Navigate(NavigationRouter.Route.Activity);
            }
        }

        private void ClearTrainerSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            CoachPicker.SelectedItem = null;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(activityService.targetActivity != null)
            {
                DialogBox dialogBox = new DialogBox($"Are you sure that you want to delete '{activityService.targetActivity.title}' ?");
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true)
                {
                    activityService.DeleteActivity(activityService.targetActivity);
                    router.Navigate(NavigationRouter.Route.Activities);
                }
            }
        }
    }
}
