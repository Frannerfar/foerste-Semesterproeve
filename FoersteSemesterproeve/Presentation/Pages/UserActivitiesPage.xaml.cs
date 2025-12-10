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
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for UserActivitiesPage.xaml
    /// </summary>
    public partial class UserActivitiesPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;


        public UserActivitiesPage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;

            UserText.Text = $"{userService.targetUser?.firstName} {userService.targetUser?.lastName}";

            //if (userService.targetUser != null) {
            //    foreach (Activity activity in userService.targetUser?.activityList)
            //    {
            //        ActivitiesDataGrid.Items.Add(activity);
            //    }
            //}


            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2;

            if(userService.targetUser != null)
            {
                if (userService.targetUser.activityList.Count > 0)
                {
                    for (int i = 0; i < userService.targetUser.activityList.Count; i++)
                    {
                        iRemainder = i % itemsPerRow;
                        iRemainder = i % itemsPerRow;
                        if (iRemainder == 0)
                        {
                            UserActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            if (rows == 0)
                            {
                                UserActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                columns++;
                            }
                            rows++;

                        }
                        if (iRemainder != 0 && columns < itemsPerRow)
                        {
                            UserActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            columns++;
                        }


                        Border border = new Border();
                        border.BorderThickness = new Thickness(1);
                        border.BorderBrush = new SolidColorBrush(Colors.Black);
                        Grid.SetRow(border, rows - 1);
                        Grid.SetColumn(border, iRemainder);
                        border.MaxWidth = 300;
                        border.Margin = new Thickness(0, 0, 0, 20);
                        border.Padding = new Thickness(10);

                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Margin = new Thickness(0);
                        border.Child = stackPanel;



                        // HOLD NAVN SEKTION
                        TextBlock nameTextBlock = new TextBlock();
                        nameTextBlock.Text = $"{userService.targetUser.activityList[i].title}";
                        nameTextBlock.FontSize = 18;
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
                        if (userService.targetUser.activityList[i].maxCapacity != null)
                        {
                            capacityString = $"Participants: {userService.targetUser.activityList[i].participants.Count} / {userService.targetUser.activityList[i].maxCapacity}";
                        }
                        else
                        {
                            capacityString = $"Participants: {userService.targetUser.activityList[i].participants.Count} / Unlimited";
                        }
                        TextBlock capacityTextBlock = new TextBlock();
                        capacityTextBlock.Text = capacityString;
                        capacityTextBlock.FontSize = 14;
                        capacityTextBlock.Margin = new Thickness(0, 10, 0, 10);
                        stackPanel.Children.Add(capacityTextBlock);




                        // START SEKTION
                        TextBlock activityStartTimeTextBlock = new TextBlock();
                        activityStartTimeTextBlock.Text = $"Date: {userService.targetUser.activityList[i].startTime:dd-MM-yyyy HH:mm}";
                        activityStartTimeTextBlock.FontSize = 14;
                        stackPanel.Children.Add(activityStartTimeTextBlock);


                        // END SEKTION
                        TextBlock activityEndTimeTextBlock = new TextBlock();
                        activityEndTimeTextBlock.Text = $"End: {userService.targetUser.activityList[i].endTime:dd-MM-yyyy HH:mm}";
                        activityEndTimeTextBlock.FontSize = 14;
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
                        seeMoreButton.Tag = userService.targetUser.activityList[i];
                        seeMoreButton.Click += ActivityButton_Click;
                        seeMoreButton.Cursor = Cursors.Hand;

                        stackPanel.Children.Add(seeMoreButton);

                        UserActivitiesGrid.Children.Add(border);
                    }
                }
                else
                {

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Margin = new Thickness(0);

                    TextBlock noUpcomingActivitiesBlock = new TextBlock();
                    noUpcomingActivitiesBlock.Margin = new Thickness(0, 20, 0, 20);
                    noUpcomingActivitiesBlock.FontSize = 16;
                    noUpcomingActivitiesBlock.Text = "No activities";
                    noUpcomingActivitiesBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(noUpcomingActivitiesBlock);

                    UserActivitiesGrid.Children.Add(stackPanel);
                }
        
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

        

        private void LeaveActivity_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Waiting for implements of JOIN/LEAVE functionality");
        }


        //private void BackButton_Click(object sender, RoutedEventArgs e)
        //{
        //    router.Navigate(NavigationRouter.Route.EditUser);
        //}
    }
}
