using FoersteSemesterproeve.Domain.Models;
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
using ActivityModel = FoersteSemesterproeve.Domain.Models.Activity;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        NavigationRouter navigationRouter;
        UserService userService;
        ActivityService activityService;

        public HomePage(NavigationRouter navigationRouter, UserService userService, ActivityService activityService)
        {

            InitializeComponent();
            this.navigationRouter = navigationRouter;
            this.userService = userService;
            this.activityService = activityService;
            LoadActivities();
        }
        private void LoadActivities() 
        {
            ActivitiesList.ItemsSource = activityService.activities;
        }
        
    }
}
