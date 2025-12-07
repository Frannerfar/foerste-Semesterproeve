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
    /// Interaction logic for ActivityPage.xaml
    /// </summary>
    public partial class ActivityPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;

        public ActivityPage(NavigationRouter router, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent();

            this.router = router;
            this.activityService = activityService;
            this.userService = userService;
            this.locationService = locationService;

            this.coaches = new List<User>();


            if (activityService.targetActivity != null)
            {

                // PAGE TITLE
                TitleActivity.Text = $"{activityService.targetActivity.title }";



                TitleBox.Text = activityService.targetActivity.title;
    
                // Trainer
                for(int i = 0; i < userService.users.Count;  i++)
                {
                    if (userService.users[i].isCoach == true)
                    {
                        coaches.Add(userService.users[i]);
                        CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                    }
                }
                for(int i = 0; i < coaches.Count; i++)
                {
                    if (activityService.targetActivity.coach == coaches[i])
                    {
                        CoachPicker.SelectedIndex = i;
                    }
                }



                // Location
                for(int i = 0; i < locationService.locations.Count; i++)
                {
                    string maxCapacityText;
                    if (locationService.locations[i].maxCapacity != null)
                    {
                        maxCapacityText = $"max. {locationService.locations[i].maxCapacity} people";
                    }
                    else
                    {
                        maxCapacityText = $"Unlimited people";
                    }
                    //LocationPicker.Items.Add(locationService.locations[i].name);
                    LocationPicker.Items.Add($"{locationService.locations[i].name} ({maxCapacityText})");

                    if (activityService.targetActivity.location == locationService.locations[i])
                    {
                        LocationPicker.SelectedIndex = i;
                    }
                }
                if(LocationPicker.SelectedItem == null)
                {
                    LocationPicker.SelectedIndex = 0;
                }



                // Capacity
                CapacityBox.Text = activityService.targetActivity.maxCapacity.ToString();


                StartDatePicker.SelectedDate = activityService.targetActivity.startTime;
                EndDatePicker.SelectedDate = activityService.targetActivity.endTime;

                StartTimeBox.Text = $"{activityService.targetActivity.startTime.Hour:D2}:{activityService.targetActivity.startTime.Minute:D2}";
                EndTimeBox.Text = $"{activityService.targetActivity.endTime.Hour:D2}:{activityService.targetActivity.endTime.Minute:D2}";

                

                RedrawAttendingButtons();

                RedrawAttenders();

                //for (int i = 0;i < activityService.targetActivity.participants.Count;i++)
                //{
                //    Border userBorder = new Border();
                //    ActivityUsersPanel.Children.Add(userBorder);

                //    StackPanel userPanel = new StackPanel();
                //    userBorder.Child = userPanel;

                //    if (userService.authenticatedUser != null) 
                //    { 

                //        // HVIS BRUGEREN ER ADMIN, SÅ VIS  BRUGERE PÅ AKTIVIETEN SOM BUTTON MED NAVIGATION TIL DERES SIDE
                //        if(userService.authenticatedUser.isAdmin == true)
                //        {
                //            Button userNameButton = new Button();
                //            userNameButton.Tag = activityService.targetActivity.participants[i];
                //            userNameButton.Click += UserButton_Click;
                //            userNameButton.Padding = new Thickness(10, 5, 10, 5);
                //            userNameButton.Width = 200;
                //            userNameButton.Background = new SolidColorBrush(Colors.Yellow);
                //            userNameButton.Content = $"{activityService.targetActivity.participants[i].firstName} {activityService.targetActivity.participants[i].lastName}";
                //            userPanel.Children.Add(userNameButton);
                //        }
                //        // HVIS BRUGEREN IKKE ER ADMIN, SÅ VIS BRUGERE SOM TEKSTBLOK
                //        //else
                //        //{
                //        //    TextBlock userNameButton = new TextBlock();
                //        //    userNameButton.Padding = new Thickness(10, 5, 10, 5);
                //        //    userNameButton.Width = 200;
                //        //    userNameButton.Background = new SolidColorBrush(Colors.Yellow);
                //        //    userNameButton.Text = $"{activityService.targetActivity.participants[i].firstName} {activityService.targetActivity.participants[i].lastName}";
                //        //    userPanel.Children.Add(userNameButton);
                //        //}
                //    }
                //}
            }

        }


        private void RedrawAttenders()
        {

            ActivityUsersPanel.Children.Clear();
            if(activityService.targetActivity != null)
            {
                AmountOfAttendees.Text = activityService.targetActivity.participants.Count.ToString();
                for (int i = 0; i < activityService.targetActivity.participants.Count; i++)
                {
                    Border userBorder = new Border();
                    ActivityUsersPanel.Children.Add(userBorder);

                    StackPanel userPanel = new StackPanel();
                    userBorder.Child = userPanel;

                    if (userService.authenticatedUser != null)
                    {

                        // HVIS BRUGEREN ER ADMIN, SÅ VIS  BRUGERE PÅ AKTIVIETEN SOM BUTTON MED NAVIGATION TIL DERES SIDE
                        if (userService.authenticatedUser.isAdmin == true)
                        {
                            Button userNameButton = new Button();
                            userNameButton.Tag = activityService.targetActivity.participants[i];
                            userNameButton.Click += UserButton_Click;
                            userNameButton.Padding = new Thickness(10, 5, 10, 5);
                            userNameButton.Width = 200;
                            userNameButton.Background = new SolidColorBrush(Colors.Yellow);
                            userNameButton.Content = $"{activityService.targetActivity.participants[i].firstName} {activityService.targetActivity.participants[i].lastName}";
                            userPanel.Children.Add(userNameButton);
                        }
                        // HVIS BRUGEREN IKKE ER ADMIN, SÅ VIS BRUGERE SOM TEKSTBLOK
                        //else
                        //{
                        //    TextBlock userNameButton = new TextBlock();
                        //    userNameButton.Padding = new Thickness(10, 5, 10, 5);
                        //    userNameButton.Width = 200;
                        //    userNameButton.Background = new SolidColorBrush(Colors.Yellow);
                        //    userNameButton.Text = $"{activityService.targetActivity.participants[i].firstName} {activityService.targetActivity.participants[i].lastName}";
                        //    userPanel.Children.Add(userNameButton);
                        //}
                    }
                }
            }
        }


        private void RedrawAttendingButtons()
        {
            AttendingActionPanel.Children.Clear();
            if (userService.authenticatedUser != null)
            {
                bool activityExists = false;
                for (int j = 0; j < userService.authenticatedUser.activityList.Count; j++)
                {
                    if(activityService.targetActivity != null)
                    {
                        if (userService.authenticatedUser.activityList[j] == activityService.targetActivity)
                        {
                            activityExists = true;
                        }
                    }

                }
                if (activityExists)
                {
                    Button leaveButton = new Button();
                    leaveButton.Content = "Leave";
                    leaveButton.Margin = new Thickness(0, 0, 0, 0);
                    leaveButton.Padding = new Thickness(10, 5, 10, 5);
                    leaveButton.Width = 100;
                    leaveButton.Background = new SolidColorBrush(Colors.Red);
                    leaveButton.Click += LeaveButton_Click;
                    leaveButton.Cursor = Cursors.Hand;
                    AttendingActionPanel.Children.Add(leaveButton);
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
                    AttendingActionPanel.Children.Add(joinButton);
                }
            }
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            userService.targetUser = user;

            router.Navigate(NavigationRouter.Route.EditUser);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }


        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            if(activityService.targetActivity != null)
            {
                if (userService.authenticatedUser != null)
                {
                    activityService.JoinActivity(activityService.targetActivity, userService.authenticatedUser);
                    RedrawAttendingButtons();
                    RedrawAttenders();
                }
            }
        }

        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(activityService.targetActivity != null)
            {
                if (userService.authenticatedUser != null)
                {
                    activityService.LeaveActitvity(activityService.targetActivity, userService.authenticatedUser);
                    RedrawAttendingButtons();
                    RedrawAttenders();
                }
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isMaxCapNumber = int.TryParse(CapacityBox.Text, out int maxCap);
            int? maxCapcacity;
            if(isMaxCapNumber)
            {
                maxCapcacity = maxCap;
            }
            else
            {
                maxCapcacity = null;
            }

            bool isStartDate = DateTime.TryParse(StartDatePicker.Text, out DateTime actualStartDateTime);
            bool isEndDate = DateTime.TryParse(EndDatePicker.Text, out DateTime actualEndDateTime);

            
            if(!isStartDate)
            {
                MessageBox.Show("Please input a valid date!");
                return;
            }
            if(!isEndDate)
            {
                MessageBox.Show("Please input a valid date!");
                return;
            }

            DateOnly onlyStartDate = DateOnly.FromDateTime(actualStartDateTime);
            DateOnly onlyEndDate = DateOnly.FromDateTime(actualEndDateTime);
            
            bool isStartTime = TimeOnly.TryParse(StartTimeBox.Text, out TimeOnly actualStartTime);
            bool isEndTime = TimeOnly.TryParse(EndTimeBox.Text, out TimeOnly actualEndTime);

            if (!isStartTime)
            {
                MessageBox.Show("Please input a valid time!");
                return;
            }
            if (!isEndTime)
            {
                MessageBox.Show("Please input a valid time!");
                return;
            }

            DateTime startDateTime = onlyStartDate.ToDateTime(actualStartTime);
            DateTime endDateTime = onlyEndDate.ToDateTime(actualEndTime);

            if (activityService.targetActivity != null)
            {
                activityService.targetActivity.title = TitleBox.Text;
                activityService.targetActivity.maxCapacity = maxCapcacity;
                activityService.targetActivity.coach = coaches[CoachPicker.SelectedIndex];
                activityService.targetActivity.location = locationService.locations[LocationPicker.SelectedIndex];

                activityService.targetActivity.startTime = startDateTime;
                activityService.targetActivity.endTime = endDateTime; 

                router.Navigate(NavigationRouter.Route.Activities);
            }
        }
    }
}
