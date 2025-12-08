using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
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
using ActivityModel = FoersteSemesterproeve.Domain.Models.Activity;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditActivitiesPage.xaml
    /// </summary>
    public partial class EditActivitiesPage : UserControl
    {
        private NavigationRouter router;
        private ActivityService activityService;
        private UserService userService;
        private LocationService locationService;

        private ActivityModel targetActivity;
        public EditActivitiesPage(NavigationRouter router, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();
            this.router = router;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            targetActivity = activityService.targetActivity;

            if (targetActivity != null)
            {
                TitleBox.Text = targetActivity.title;
                StartDatePicker.SelectedDate = targetActivity.startTime;
                EndDatePicker.SelectedDate = targetActivity.endTime;
                MaxCapacityBox.Text = targetActivity.maxCapacity.ToString();


                var coaches = userService.users.Where(u => u.isCoach).ToList();
                CoachComboBox.ItemsSource = coaches;
                CoachComboBox.DisplayMemberPath = "firstName";
                CoachComboBox.SelectedItem = targetActivity.coach;

                LocationComboBox.ItemsSource = locationService.locations;
                LocationComboBox.DisplayMemberPath = "name";
                LocationComboBox.SelectedItem = targetActivity.location;
            }
        }

        private void SaveEditButton_Click(object sender, EventArgs e)
        {
            if (targetActivity == null)
            {
                MessageBox.Show("Ingen aktivitet valgt til at redigere");
                router.Navigate(NavigationRouter.Route.Activities);
                return; 
            }
               

            targetActivity.title = TitleBox.Text;
            targetActivity.startTime = StartDatePicker.SelectedDate ?? DateTime.Now;
            targetActivity.endTime = EndDatePicker.SelectedDate ?? DateTime.Now;
            targetActivity.maxCapacity = int.TryParse(MaxCapacityBox.Text, out int cap) ? cap : 0;
            targetActivity.coach = (User)CoachComboBox.SelectedItem;
            targetActivity.location = (Location)LocationComboBox.SelectedItem;

            MessageBox.Show($"Changes are saved for '{targetActivity.title}'");
            router.Navigate(NavigationRouter.Route.Activities);
        }
        
        private void CancelEditButton_Click(Object sender, EventArgs e) 
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }

    }
}
