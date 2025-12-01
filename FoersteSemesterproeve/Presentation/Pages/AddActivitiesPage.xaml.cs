using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
//using System.Diagnostics;
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
        public AddActivitiesPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService)
        {
            InitializeComponent();
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            CheckAdminPermissions();
        }

        private void CheckAdminPermissions()
        {
            if (userService.authenticatedUser == null || !userService.authenticatedUser.isAdmin)
            {
                // lukker alle felter hvis det ikke er en admin 
                TitleBox.IsEnabled = false;
                DescriptionBox.IsEnabled = false;
                TrainerBox.IsEnabled = false;
                LocationBox.IsEnabled = false;
                ParticipantBox.IsEnabled = false;
                StartTimeCombo.IsEnabled = false;
                StartDatePicker.IsEnabled = false;
                DeadlineTimeCombo.IsEnabled = false;
                DeadlineDatePicker.IsEnabled = false;

               
            }

            
        }
        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            if (userService.authenticatedUser.isAdmin)
            {
                //Dialogbox "Only admins can create activities"
                return;
            }

            if(string.IsNullOrEmpty(TitleBox.Text)) 
            {
                //Dialogbox "Title cannot be empty"
                return;
            }
            // Bro det her sutter åbenbart ved den ikke at jeg prøver at intacisere et nyt objekt af typen aktivitet 
            Activity activity = new Activity()
            {
                //Title = TitleBox.Text,
                //Description = DescriptionBox.Text,
                //Trainer = TrainerBox.Text,
            };

            // Save into service
            activityService.activities.Add(activity);

            MessageBox.Show("Activity created!");

            ClearFields();
        }
        

        private void ClearFields()
        {

        }
    }
}
