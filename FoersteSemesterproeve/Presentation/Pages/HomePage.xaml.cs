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
        NavigationRouter navigationRouter;
        UserService userService;
        ActivityService activityService;
         
        public HomePage(NavigationRouter navigationRouter, UserService userService, ActivityService activityService)
        {
            
            InitializeComponent();
            this.navigationRouter = navigationRouter;
            this.userService = userService;
            this.activityService = activityService;

            if(userService.authenticatedUser != null)
            {
                AuthenticatedUserName.Text = $"Hello, {userService.authenticatedUser.firstName}!";
            }

            DateTime now = DateTime.Now;



            for (int i = 0; i < 5; i++)
            {
                DateTime day = now.AddDays(i);
                DateOnly date = DateOnly.FromDateTime(day);
                DayOfWeek whichDay = day.DayOfWeek;
                int dayNumber = day.Day;
                int monthNumber = day.Month;


                StackPanel dayPanel = new StackPanel();
                Grid.SetRow(dayPanel, 0);
                Grid.SetColumn(dayPanel, i);
                GridDays.Children.Add(dayPanel);


                    TextBlock dayTextBlock = new TextBlock();
                    dayTextBlock.Text = $"{whichDay} ({dayNumber}/{monthNumber})";
                    dayTextBlock.FontSize = 20;
                    dayTextBlock.FontWeight = FontWeights.Bold;
                    dayTextBlock.HorizontalAlignment  = HorizontalAlignment.Center;
                    dayPanel.Children.Add(dayTextBlock);

                //Grid dayActivitiesGrid = new Grid();
                //dayActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                //dayActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //GridDays.Children.Add(dayActivitiesGrid);

                Border dayActivitiesBorder = new Border();
                dayActivitiesBorder.BorderThickness = new Thickness(1);
                dayActivitiesBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                dayActivitiesBorder.Background = new SolidColorBrush(Colors.LightGray);
                Grid.SetColumn(dayActivitiesBorder, i);
                Grid.SetRow(dayActivitiesBorder, 1);
                GridDays.Children.Add(dayActivitiesBorder);

                ScrollViewer scrollViewer = new ScrollViewer();
                dayActivitiesBorder.Child = scrollViewer;

                StackPanel activitiesPanel = new StackPanel();
                scrollViewer.Content = activitiesPanel;

                List<Activity> dayActivitiesList = new List<Activity>();

                for(int j = 0; j < activityService.activities.Count; j++)
                {
                    DateOnly activityDate = DateOnly.FromDateTime(activityService.activities[j].startTime);
                    if (activityDate == date)
                    {
                        dayActivitiesList.Add(activityService.activities[j]);

                        Border activityItemBorder = new Border();
                        activityItemBorder.Margin = new Thickness(10);
                        activityItemBorder.Padding = new Thickness(10);
                        activityItemBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                        activityItemBorder.BorderThickness = new Thickness(1);
                        activityItemBorder.Background = new SolidColorBrush(Colors.White);
                        activitiesPanel.Children.Add(activityItemBorder);

                        StackPanel activityItemPanel = new StackPanel();
                        activityItemBorder.Child = activityItemPanel;

                        TextBlock activityNameTextBlock = new TextBlock();
                        activityNameTextBlock.Text = activityService.activities[j].title;
                        activityNameTextBlock.FontSize = 14;
                        activityNameTextBlock.FontWeight = FontWeights.DemiBold;
                        activityItemPanel.Children.Add(activityNameTextBlock);

                        TextBlock activityStartTimeTextBlock = new TextBlock();
                        activityStartTimeTextBlock.Text = $"Start: {activityService.activities[j].startTime}";
                        activityItemPanel.Children.Add(activityStartTimeTextBlock);

                        TextBlock activityEndTimeTextBlock = new TextBlock();
                        activityEndTimeTextBlock.Text = $"End: {activityService.activities[j].endTime}";
                        activityItemPanel.Children.Add(activityEndTimeTextBlock);

                        TimeSpan difference = activityService.activities[j].endTime - activityService.activities[j].startTime;
                        int hours = difference.Hours;
                        int minutes = difference.Minutes;
                        TextBlock activityDurationTextBlock = new TextBlock();
                        activityDurationTextBlock.Text = $"Duration: {hours:D1}:{minutes:D2} hours";
                        activityItemPanel.Children.Add(activityDurationTextBlock);

                        TextBlock activityCapacityTextBlock = new TextBlock();
                        activityCapacityTextBlock.Text = $"Capacity: {activityService.activities[j].participants.Count} / {activityService.activities[j].maxCapacity}";
                        activityItemPanel.Children.Add(activityCapacityTextBlock);

                        Button enterActivity = new Button();
                        enterActivity.Content = "See";
                        enterActivity.Margin = new Thickness(10);
                        enterActivity.Padding = new Thickness(10);
                        enterActivity.BorderBrush = new SolidColorBrush(Colors.LightSteelBlue);
                        enterActivity.BorderThickness = new Thickness(1);
                        enterActivity.Cursor = Cursors.Hand;
                        enterActivity.Background = new SolidColorBrush(Colors.Yellow);
                        enterActivity.Tag = activityService.activities[j];
                        activityItemPanel.Children.Add(enterActivity);


                    }
                }
            }









            List <Domain.Models.Activity> upcomingActivities = new List<Domain.Models.Activity>();

            for(int i = 0;  i < activityService.activities.Count; i++)
            {
                if(activityService.activities[i].startTime > now)
                {
                    upcomingActivities.Add(activityService.activities[i]);
                    //Debug.WriteLine($"New Upcoming Activity: {activityService.activities[i].title}");
                }
            }


            if (upcomingActivities.Count > 0) {
                for (int i = 0; i < upcomingActivities.Count; i++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    border.Padding = new Thickness(20);

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(10);
                    border.Child = stackPanel;

                    TextBlock nameTextBlock = new TextBlock();
                    nameTextBlock.Text = upcomingActivities[i].title;
                    stackPanel.Children.Add(nameTextBlock);

                    string coachString;
                    if (upcomingActivities[i].coach != null)
                    {
                        coachString = $"{upcomingActivities[i].coach.firstName} {upcomingActivities[i].coach.lastName}";
                    }
                    else
                    {
                        coachString = "None";
                    }

                    TextBlock trainerTextBlock = new TextBlock();
                    trainerTextBlock.Text = coachString;
                    stackPanel.Children.Add(trainerTextBlock);


                    string capacityString;
                    if (upcomingActivities[i].maxCapacity != null)
                    {
                        capacityString = $"Max Capacity: {upcomingActivities[i].maxCapacity}";
                    }
                    else
                    {
                        capacityString = "Unlimited";
                    }


                    TextBlock capacityTextBlock = new TextBlock();
                    capacityTextBlock.Text = capacityString;
                    stackPanel.Children.Add(capacityTextBlock);


                    TextBlock participantCountTextBlock = new TextBlock();
                    participantCountTextBlock.Text = $"Participants: {upcomingActivities[i].participants.Count}";
                    stackPanel.Children.Add(participantCountTextBlock);


                    Button seeMoreButton = new Button();
                    seeMoreButton.Margin = new Thickness(20, 5, 20, 5);
                    seeMoreButton.Background = new SolidColorBrush(Colors.Yellow);
                    seeMoreButton.Content = "See More";
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
    }
}
