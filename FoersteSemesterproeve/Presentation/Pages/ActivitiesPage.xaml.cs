using FoersteSemesterproeve.Domain.Services;
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
    /// Interaction logic for ActivitiesView.xaml
    /// </summary>
    public partial class ActivitiesPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;

        public ActivitiesPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService)
        {

            InitializeComponent();
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            LoadActivities();
            
        }


        private void LoadActivities()
        {
            //ActivitiesList.ItemsSource = null;
            //ActivitiesList.ItemsSource = activityService.activities;
            ActivitiesList.ItemsSource = activityService.GetAllActivities(); 
        }
        private void JoinButtonClick(object sender, RoutedEventArgs e)
        {
            if (userService.authenticatedUser != null)
            {
                activityService.JoinActivity(activityService.activities[0], userService.authenticatedUser);
                //Debug.WriteLine($"{userService.authenticatedUser.firstName} has joined {activityService.activities[0].title}");
            }

        }

        private void AddActivityClick(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddActivities);
        }
    }
}
