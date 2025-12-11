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
    public partial class AddActivityPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;
        

        public AddActivityPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService, LocationService locationService)
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

            DateOnly onlyStartDate = DateOnly.FromDateTime(actualStartDateTime);
            DateOnly onlyEndDate = DateOnly.FromDateTime(actualEndDateTime);

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

            DateTime startDateTime = onlyStartDate.ToDateTime(actualStartTime);
            DateTime endDateTime = onlyEndDate.ToDateTime(actualEndTime);

            if (endDateTime <= startDateTime)
            {
                MessageBox.Show("End time must be after start time.");
                return;
            }



            int? actualMaxCap = null;
            if(!isUnlimited)
            {
                actualMaxCap = tempMaxCap;
            }
            activityService.AddActivity(TitleBox.Text, coach, location, actualMaxCap, startDateTime, endDateTime);
            router.Navigate(NavigationRouter.Route.Activities);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }
    }
}
