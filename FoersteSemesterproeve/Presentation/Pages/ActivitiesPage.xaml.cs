using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for ActivitiesView.xaml
    /// </summary>
    public partial class ActivitiesPage : UserControl
    {
        private NavigationRouter router;
        private ActivityService activityService;
        private UserService userService;

        public Visibility DeleteButtonVisibility =>
            userService.authenticatedUser?.isAdmin == true ? Visibility.Visible : Visibility.Collapsed;

        public ActivitiesPage(NavigationRouter router, ActivityService activityService, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.activityService = activityService;
            this.userService = userService;

            LoadActivities();
        }

        private void LoadActivities()
        {
            var user = userService.authenticatedUser;

            if (user == null)
            {
                JoinedActivitiesList.ItemsSource = null;
                AllActivitiesList.ItemsSource = activityService.GetAllActivities();
                return;
            }

            var joined = activityService.activities
                .Where(a => user.activityList.Contains(a))
                .ToList();

            var others = activityService.activities
                .Where(a => !user.activityList.Contains(a))
                .ToList();

            JoinedActivitiesList.ItemsSource = joined;
            AllActivitiesList.ItemsSource = others;

            foreach (var activity in activityService.activities)
            {
                activity.participantCount = userService.users.Count(u => u.activityList.Contains(activity));
            }
        }

        private void AddActivityClick(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddActivities);
        }

        private void DeleteActivityClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ActivityModel activity = (ActivityModel)button.Tag;

            var dialog = new DialogBox($"Er du sikker på at du vil slette '{activity.title}'?");
            dialog.ShowDialog();

            if (dialog.DialogResult == true)
            {
                activityService.DeleteActivity(activity);
                LoadActivities();
            }
        }

        private void JoinActivityClick(object sender, RoutedEventArgs e)
        {
            var user = userService.authenticatedUser;
            Button button = (Button)sender;
            ActivityModel activity = (ActivityModel)button.Tag;

            if (activityService.JoinActivity(activity, user))


                MessageBox.Show($"Du er nu tilmeldt aktiviteten: {activity.title}");
            else
                MessageBox.Show($"Du kan ikke tilmelde dig aktiviteten");
                
            LoadActivities();
            
        }

        private void LeaveActivityClick(object sender, RoutedEventArgs e)
        {
            var user = userService.authenticatedUser;
            Button button = (Button)sender;
            ActivityModel activity = (ActivityModel)button.Tag;

            if (activityService.LeaveActitvity(activity, user))


                MessageBox.Show($"Du er nu afmeldt aktiviteten: {activity.title}");
            else
                MessageBox.Show($"Du er ikke tilmeldt denne aktivitet.");
                
            LoadActivities();
            
        }

        private void EditActivityClick(object sender, RoutedEventArgs e) 
        {
            MessageBox.Show("Edit knappen blev klikket");

            Button button = (Button)sender;
            ActivityModel activity = ( ActivityModel)button.Tag;

            activityService.targetActivity = activity;
            router.Navigate(NavigationRouter.Route.EditActivities);
        }
    }
}
