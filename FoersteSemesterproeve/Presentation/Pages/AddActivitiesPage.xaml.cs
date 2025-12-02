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
        public AddActivitiesPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            CoachPicker.ItemsSource = userService.users.Where(u => u.isCoach).ToList();
            LocationPicker.ItemsSource = locationService.locations;

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

                if (CoachPicker.SelectedItem is not User coach)
                {
                    MessageBox.Show("You must select a coach.");
                    return;
                }

                if (LocationPicker.SelectedItem is not Location location)
                {
                    MessageBox.Show("You must select a location.");
                    return;
                }

                if (!int.TryParse(CapacityBox.Text, out int maxCap))
                {
                    MessageBox.Show("Max capacity must be a number.");
                    return;
                }

                // ----- DATE & TIME -----
                var start = ParseDateTime(StartDatePicker, StartTimeBox.Text);
                var end = ParseDateTime(EndDatePicker, EndTimeBox.Text);

                if (end <= start)
                {
                    MessageBox.Show("End time must be after start time.");
                    return;
                }

                // ----- CREATE ACTIVITY -----
                Activity activity = new Activity
                {
                    title = TitleBox.Text,
                    //coach = coach,
                    location = location,
                    maxCapacity = maxCap,
                    startTime = start,
                    endTime = end
                };

                activityService.AddActivity(activity);

                MessageBox.Show("Activity created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private DateTime ParseDateTime(DatePicker picker, string timeText)
        {
            var date = picker.SelectedDate ?? DateTime.Today;

            var time = TimeSpan.Parse(timeText); // "10:30" → TimeSpan

            return date.Date + time;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Cancelled.");
        }
    }
}
