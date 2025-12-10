using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Models;
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
    /// Interaction logic for ViewTrainerInfoPage.xaml
    /// </summary>
    public partial class TrainerPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;
        public TrainerPage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;

            if(userService.targetUser != null)
            {
                TrainerFullNameBlock.Text = $"{userService.targetUser.firstName} {userService.targetUser.lastName}";
                
                for(int i = 0; i < activityService.activities.Count; i++)
                {

                    if (activityService.activities[i].coach == userService.targetUser)
                    {
                        //TrainerActivitiesScrollViewer.Items.Add
                        StackPanel activityStackPanel = new StackPanel();
                        activityStackPanel.Margin = new Thickness(5, 10, 5, 10);
                        ActivitiesStackPanel.Children.Add(activityStackPanel);

                        Button activityButton = new Button();
                        activityButton.Content = activityService.activities[i].title;
                        activityButton.Padding = new Thickness(15, 10, 15, 10);
                        activityButton.Background = new SolidColorBrush(Colors.Yellow);
                        activityButton.Tag = activityService.activities[i];
                        activityButton.Click += ActivityButton_Click;
                        activityStackPanel.Children.Add(activityButton);
                    }
                }
                
            }

        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if(activity != null )
            {
                activityService.targetActivity = activity;

                router.Navigate(NavigationRouter.Route.Activity);
            }
        }

        private void BackToTrainersPageButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Trainers);
        }
    }
}
