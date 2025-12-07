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
using static System.Net.Mime.MediaTypeNames;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;

        List<Domain.Models.Activity> upcomingActivities;

        public HomePage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            
            InitializeComponent();
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;
            this.upcomingActivities = new List<Domain.Models.Activity>();

            if (userService.authenticatedUser != null)
            {
                AuthenticatedUserName.Text = $"Hello, {userService.authenticatedUser.firstName}!";
            }

            DateTime now = DateTime.Now;


            if(userService.authenticatedUser != null)
            {
                for(int i = 0;  i < userService.authenticatedUser.activityList.Count; i++)
                {
                    if (userService.authenticatedUser.activityList[i].startTime > now)
                    {
                        upcomingActivities.Add(userService.authenticatedUser.activityList[i]);
                        //MessageBox.Show($"Added new activity: {userService.authenticatedUser.activityList[i].title}");
                    }
                }
            }


            if (upcomingActivities.Count > 0) {
                for (int i = 0; i < upcomingActivities.Count; i++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.MaxWidth = 300;
                    border.Padding = new Thickness(10);

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(10);
                    border.Child = stackPanel;



                    // HOLD NAVN SEKTION
                    TextBlock nameTextBlock = new TextBlock();
                    nameTextBlock.Text = $"{upcomingActivities[i].title}";
                    nameTextBlock.FontSize = 16;
                    nameTextBlock.FontWeight = FontWeights.Bold;
                    nameTextBlock.Margin = new Thickness(0, 0, 0, 10);
                    stackPanel.Children.Add(nameTextBlock);



                    // TRAINER SEKTION
                    //string coachString;
                    //if (upcomingActivities[i].coach != null)
                    //{
                    //    coachString = $"{upcomingActivities[i].coach.firstName} {upcomingActivities[i].coach.lastName}";
                    //}
                    //else
                    //{
                    //    coachString = "None";
                    //}
                    //TextBlock trainerTextBlock = new TextBlock();
                    //trainerTextBlock.Text = $"Trainer: {coachString}";
                    //stackPanel.Children.Add(trainerTextBlock);


                    // CAPACITY SEKTION
                    string capacityString;
                    if (upcomingActivities[i].maxCapacity != null)
                    {
                        capacityString = $"Participants: {upcomingActivities[i].participants.Count} / {upcomingActivities[i].maxCapacity}";
                    }
                    else
                    {
                        capacityString = $"Participants: {upcomingActivities[i].participants.Count} / Unlimited";
                    }
                    TextBlock capacityTextBlock = new TextBlock();
                    capacityTextBlock.Text = capacityString;
                    capacityTextBlock.Margin = new Thickness(0, 10, 0, 10);
                    stackPanel.Children.Add(capacityTextBlock);




                    // START SEKTION
                    TextBlock activityStartTimeTextBlock = new TextBlock();
                    activityStartTimeTextBlock.Text = $"Date: {upcomingActivities[i].startTime}";
                    stackPanel.Children.Add(activityStartTimeTextBlock);


                    // END SEKTION
                    TextBlock activityEndTimeTextBlock = new TextBlock();
                    activityEndTimeTextBlock.Text = $"End: {upcomingActivities[i].endTime}";
                    stackPanel.Children.Add(activityEndTimeTextBlock);



                    //// VARIGHED SEKTION
                    //TimeSpan difference = upcomingActivities[i].endTime - upcomingActivities[i].startTime;
                    //int hours = difference.Hours;
                    //int minutes = difference.Minutes;
                    //TextBlock activityDurationTextBlock = new TextBlock();
                    //activityDurationTextBlock.Margin = new Thickness(0, 10, 0, 10);
                    //activityDurationTextBlock.Text = $"Duration: {hours:D1}:{minutes:D2} hours";
                    //stackPanel.Children.Add(activityDurationTextBlock);




                    // SEE MORE BUTTON
                    Button seeMoreButton = new Button();
                    seeMoreButton.Margin = new Thickness(0, 20, 0, 20);
                    seeMoreButton.Padding = new Thickness(0, 5, 0, 5);
                    seeMoreButton.Background = new SolidColorBrush(Colors.Yellow);
                    seeMoreButton.Content = "See More";
                    seeMoreButton.Tag = upcomingActivities[i];
                    seeMoreButton.Click += ActivityButton_Click;
                    seeMoreButton.Cursor = Cursors.Hand;

                    stackPanel.Children.Add(seeMoreButton);

                    UpcomingActivitiesStack.Children.Add(border);
                }
            }
            else
            {

                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(10);

                TextBlock noUpcomingActivitiesBlock = new TextBlock();
                noUpcomingActivitiesBlock.Text = "----- No upcoming activities -----";
                noUpcomingActivitiesBlock.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(noUpcomingActivitiesBlock);

                UpcomingActivitiesStack.Children.Add(stackPanel);
            }
        }

        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if (activity != null)
            {
                activityService.targetActivity = activity;

                router.Navigate(NavigationRouter.Route.Activity);
            }
        }
    }
}
