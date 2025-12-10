using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using Activity = FoersteSemesterproeve.Domain.Models.Activity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddActivitiesPage.xaml
    /// </summary>
    public partial class AddActivitiesPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;
        

        public AddActivitiesPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            this.coaches = new List<User>();
            
            for(int i = 0; i < userService.users.Count; i++)
            {
                if (userService.users[i].isCoach == true)
                {
                    coaches.Add(userService.users[i]);
                    CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                }
            }

            for (int i = 0; i < locationService.locations.Count; i++)
            {
                string maxCapacityText;
                if(locationService.locations[i].maxCapacity != null)
                {
                    maxCapacityText = $"max. {locationService.locations[i].maxCapacity} people";
                }
                else
                {
                    maxCapacityText = $"Unlimited people";
                }
                LocationPicker.Items.Add($"{locationService.locations[i].name} ({maxCapacityText})");
            }
            LocationPicker.SelectedIndex = 0;












            StartDatePicker.SelectedDate = DateTime.Now;
            EndDatePicker.SelectedDate = DateTime.Now.AddDays(1);

            StartTimeBox.Text = "12:00";
            EndTimeBox.Text = "13:00";
        }

        
        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // ----- VALIDERING -----
                if (string.IsNullOrWhiteSpace(TitleBox.Text))
                {
                    MessageBox.Show("Title is required.");
                    return;
                }

                User? coach = null;
                if(CoachPicker.SelectedItem != null)
                {
                    coach = coaches[CoachPicker.SelectedIndex];
                }

                
                Location location = locationService.locations[LocationPicker.SelectedIndex];

                bool isUnlimited = false;
                int tempMaxCap = 0;
                if(string.IsNullOrEmpty(CapacityBox.Text))
                {
                    if(location.maxCapacity != null)
                    {
                        MessageBox.Show("This location doesn't support unlimited people attending");
                        return;
                    }
                    isUnlimited = true;
                }

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

                // ----- DATE & TIME -----
                var start = ParseDateTime(StartDatePicker, StartTimeBox.Text);
                var end = ParseDateTime(EndDatePicker, EndTimeBox.Text);

                if (end <= start)
                {
                    MessageBox.Show("End time must be after start time.");
                    return;
                }

                int? actualMaxCap = null;
                if(!isUnlimited)
                {
                    actualMaxCap = tempMaxCap;
                }
                activityService.AddActivity(TitleBox.Text, coach, location, actualMaxCap, start, end);

                router.Navigate(NavigationRouter.Route.Activities);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private DateTime ParseDateTime(DatePicker picker, string timeText)
        {
            DateTime date = picker.SelectedDate ?? DateTime.Today;

            TimeSpan time = TimeSpan.Parse(timeText); // "10:30" → TimeSpan

            return date.Date + time;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }
    }
}
