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
using FoersteSemesterproeve.Views;

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
            //ActivitiesGrid.Children.Clear();
            //ActivitiesGrid.RowDefinitions.Clear();
            //ActivitiesGrid.ColumnDefinitions.Clear();

            Grid ActivitiesGrid = new Grid();
            ActivitiesScrollViewer.Content = ActivitiesGrid;
            //ActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2;
            for (int i = 0; i < activityService.activities.Count; i++)
            {
                iRemainder = i % itemsPerRow;
                if (iRemainder == 0)
                {
                    ActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    if (rows == 0)
                    {
                        ActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        columns++;
                    }
                    rows++;

                }
                if (iRemainder != 0 && columns < itemsPerRow)
                {
                    ActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    columns++;
                }

                Border activityBorder = new Border();
                activityBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                //activityBorder.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
                activityBorder.BorderThickness = new Thickness(1);
                activityBorder.CornerRadius = new CornerRadius(10);
                activityBorder.MaxWidth = 400;
                activityBorder.Margin = new Thickness(10, 10, 10, 10);
                Grid.SetRow(activityBorder, rows - 1);
                Grid.SetColumn(activityBorder, iRemainder);
                //Grid.SetColumn(activityBorder, 0);
                //Grid.SetRow(activityBorder, i);
                ActivitiesGrid.Children.Add(activityBorder);


                StackPanel activityStackPanel = new StackPanel();
                activityBorder.Child = activityStackPanel;

                if(userService.authenticatedUser != null && userService.authenticatedUser.isAdmin)
                {
                    // BUTTONS
                    StackPanel buttonsPanel = new StackPanel();
                    buttonsPanel.HorizontalAlignment = HorizontalAlignment.Right;
                    buttonsPanel.Orientation = Orientation.Horizontal;
                    buttonsPanel.Margin = new Thickness(0, 10, 10, 10);
                    activityStackPanel.Children.Add(buttonsPanel);

                    Button editButton = new Button();
                    editButton.Content = "Edit";
                    editButton.Margin = new Thickness(0, 0, 10, 0);
                    editButton.Padding = new Thickness(10, 5, 10, 5);
                    //editButton.Width = 200;
                    editButton.Background = new SolidColorBrush(Colors.LimeGreen);
                    editButton.Click += EditButton_Click;
                    editButton.Cursor = Cursors.Hand;
                    editButton.Tag = activityService.activities[i];
                    buttonsPanel.Children.Add(editButton);

                    Button deleteButton = new Button();
                    deleteButton.Content = "Delete";
                    deleteButton.Margin = new Thickness(0, 0, 10, 0);
                    deleteButton.Padding = new Thickness(10, 5, 10, 5);
                    //deleteButton.Width = 200;
                    deleteButton.Background = new SolidColorBrush(Colors.Red);
                    deleteButton.Click += DeleteButton_Click;
                    deleteButton.Cursor = Cursors.Hand;
                    deleteButton.Tag = activityService.activities[i];
                    buttonsPanel.Children.Add(deleteButton);

                }





                // ACTIVITY TITLE
                TextBlock activityTitle = new TextBlock();
                activityTitle.Text = $"{activityService.activities[i].title}";
                activityTitle.FontSize = 20;
                activityTitle.FontWeight = FontWeights.Bold;
                activityTitle.Margin = new Thickness(20, 10, 20, 10);
                activityStackPanel.Children.Add(activityTitle);




                // PARTICIPANTS
                string maxCap;
                if(activityService.activities[i].maxCapacity != null)
                {
                    maxCap = $"{activityService.activities[i].maxCapacity}";
                }
                else
                {
                    maxCap = $"Unlimited";
                }
                TextBlock participantsText = new TextBlock();
                participantsText.Text = $"Participants: {activityService.activities[i].participants.Count} / {maxCap}";
                participantsText.FontSize = 16;
                participantsText.Margin = new Thickness(20, 10, 20, 0);
                //participantsText.FontWeight = FontWeights.Bold;
                activityStackPanel.Children.Add(participantsText);



                // DURATION
                StackPanel durationPanel = new StackPanel();
                durationPanel.Margin = new Thickness(20, 10, 20, 10);

                activityStackPanel.Children.Add(durationPanel);

                TextBlock durationText = new TextBlock();
                durationText.Text = $"Duration: {activityService.activities[i].endTime - activityService.activities[i].startTime}";
                durationText.FontSize = 16;
                durationPanel.Children.Add(durationText);





                // TIMES
                StackPanel timesPanel = new StackPanel();
                timesPanel.Margin = new Thickness(20, 10, 20, 10);
                activityStackPanel.Children.Add(timesPanel);

                TextBlock dateStartText = new TextBlock();
                dateStartText.Text = $"Start: {activityService.activities[i].startTime}";
                dateStartText.FontSize = 16;
                timesPanel.Children.Add(dateStartText);

                TextBlock dateEndText = new TextBlock();
                dateEndText.Text = $"End: {activityService.activities[i].endTime}";
                dateEndText.FontSize = 16;
                timesPanel.Children.Add(dateEndText);




                StackPanel activityActionPanel = new StackPanel();
                activityActionPanel.Margin = new Thickness(20, 10, 20, 10);
                activityActionPanel.Orientation = Orientation.Horizontal;
                activityStackPanel.Children.Add(activityActionPanel);

                Button activitySeeMore = new Button();
                activitySeeMore.Content = "See More";
                activitySeeMore.Background = new SolidColorBrush(Colors.Yellow);
                activitySeeMore.Margin = new Thickness(0, 0, 10, 0);
                activitySeeMore.Padding = new Thickness(10, 5, 10, 5);
                activitySeeMore.Width = 100;
                activitySeeMore.Cursor = Cursors.Hand;
                activitySeeMore.Tag = activityService.activities[i];
                activitySeeMore.Click += SeeActivityButton_Click;
                activityActionPanel.Children.Add(activitySeeMore);

                if (userService.authenticatedUser != null)
                {
                    bool activityExists = false;
                    for(int j = 0; j < userService.authenticatedUser.activityList.Count; j++)
                    {
                        if (userService.authenticatedUser.activityList[j] == activityService.activities[i])
                        {
                            activityExists = true;
                        }

                    }
                    if(activityExists)
                    {
                        Button leaveButton = new Button();
                        leaveButton.Content = "Leave";
                        leaveButton.Margin = new Thickness(0, 0, 0, 0);
                        leaveButton.Padding = new Thickness(10, 5, 10, 5);
                        leaveButton.Width = 100;
                        leaveButton.Background = new SolidColorBrush(Colors.Red);
                        leaveButton.Click += LeaveButton_Click;
                        leaveButton.Cursor = Cursors.Hand;
                        leaveButton.Tag = activityService.activities[i];
                        activityActionPanel.Children.Add(leaveButton);
                    }
                    else
                    {
                        Button joinButton = new Button();
                        joinButton.Content = "Join";
                        joinButton.Padding = new Thickness(10, 5, 10, 5);
                        joinButton.Margin = new Thickness(0, 0, 0, 0);
                        joinButton.Width = 100;
                        joinButton.Background = new SolidColorBrush(Colors.LimeGreen);
                        joinButton.Click += JoinButton_Click;
                        joinButton.Cursor = Cursors.Hand;
                        joinButton.Tag = activityService.activities[i];
                        activityActionPanel.Children.Add(joinButton);
                    }
                }
            }
        }





        private void SeeActivityButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            activityService.targetActivity = activity;

            router.Navigate(NavigationRouter.Route.Activity);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            activityService.targetActivity = activity;

            router.Navigate(NavigationRouter.Route.EditActivity);
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if(activity != null )
            {

                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete '{activity.title}'");
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true)
                {
                    activityService.activities.Remove(activity);
                    LoadActivities();
                }
            }
        }


        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if (userService.authenticatedUser != null)
            {
                activityService.JoinActivity(activity, userService.authenticatedUser);
                LoadActivities();
            }
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if (userService.authenticatedUser != null)
            {
                activityService.LeaveActitvity(activity, userService.authenticatedUser);
                LoadActivities();
            }
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddActivities);
        }
    }
}
