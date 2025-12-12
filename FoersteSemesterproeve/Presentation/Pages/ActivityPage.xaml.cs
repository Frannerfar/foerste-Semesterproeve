using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Presentation.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Rasmus</author>
    public partial class ActivityPage : UserControl
    {
        NavigationRouter router;
        ActivityService activityService;
        UserService userService;
        LocationService locationService;

        List<User> coaches;

        public ActivityPage(NavigationRouter router, ActivityService activityService, UserService userService, LocationService locationService)
        {
            InitializeComponent(); // initalisere de forskellige UI elementer

            this.router = router; 
            this.activityService = activityService; 
            this.userService = userService; 
            this.locationService = locationService; 
            // hvis brugeren er en admin vises adminstrationspanelet
            if (userService.authenticatedUser != null && userService.authenticatedUser.isAdmin) 
            {
                AttendingAdminActionPanel.Visibility = Visibility.Visible;
            }
            this.coaches = new List<User>(); // initalisere listen over trænere

            //Hvis der er valgt en aktivitet, vises dataen for den 
            if (activityService.targetActivity != null)
            {




                // sætter aktivitens titel
                TitleActivity.Text = $"{activityService.targetActivity.title }";



                TitleBox.Text = activityService.targetActivity.title;
    
                // Alle brugerer der har rollen "coach" bliver tilføjet til dropdown
                for(int i = 0; i < userService.users.Count;  i++)
                {
                    if (userService.users[i].isCoach == true)
                    {
                        coaches.Add(userService.users[i]);
                        CoachPicker.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                    }
                }
                // viser den træner, som er tilknyttet aktiviten
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
                    string maxCapacityText; //viser max kapacitet, vis der er en 
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
                    // hvis lokation passer til aktiviten, vælges den 
                    if (activityService.targetActivity.location == locationService.locations[i])
                    {
                        LocationPicker.SelectedIndex = i;
                    }
                }
                if(LocationPicker.SelectedItem == null)// hvis der ikke er valgt nogen lokation, bliver en første i listen valgt
                {
                    LocationPicker.SelectedIndex = 0; 
                }



                // Kapacitet vises
                CapacityBox.Text = activityService.targetActivity.maxCapacity.ToString();

                // Datoer vises
                StartDatePicker.SelectedDate = activityService.targetActivity.startTime;
                EndDatePicker.SelectedDate = activityService.targetActivity.endTime;
                // Tider vises som klokkeslæt
                StartTimeBox.Text = $"{activityService.targetActivity.startTime.Hour:D2}:{activityService.targetActivity.startTime.Minute:D2}";
                EndTimeBox.Text = $"{activityService.targetActivity.endTime.Hour:D2}:{activityService.targetActivity.endTime.Minute:D2}";
                // Opdater knapper og deltagerliste visuelt 
                RedrawAttendingButtons();

                RedrawAttenders();
            }

        }

        /// <summary>
        /// Tegner listen over deltagere
        /// </summary>
        /// <author> Rasmus </author>
        private void RedrawAttenders()
        {
            ActivityUsersPanel.Children.Clear(); // Rydder panelet inden den bliver bygget op igen 
            if(activityService.targetActivity != null)
            {
                string maxCapacityText;// viser antal deltagere / max kapacitet
                if (activityService.targetActivity.maxCapacity != null)
                {
                    maxCapacityText = $"{activityService.targetActivity.maxCapacity}";
                }
                else
                {
                    maxCapacityText = $"Unlimited";
                }

                AmountOfAttendees.Text = $"{activityService.targetActivity.participants.Count} / {maxCapacityText}";
                for (int i = 0; i < activityService.targetActivity.participants.Count; i++) // går igennem alle deltagere og viser dem på siden 
                {
                    Border userBorder = new Border();
                    ActivityUsersPanel.Children.Add(userBorder);

                    StackPanel userPanel = new StackPanel();
                    userPanel.Orientation = Orientation.Horizontal;
                    userPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    userBorder.Child = userPanel;

                    if (userService.authenticatedUser != null)
                    {
                        // hvis brugeren er en admin, kan de klikke sig ind på brugeren og fjerne dem
                        if (userService.authenticatedUser.isAdmin == true)
                        { // knap med brugerens navn, der fører til redigering af brugeren
                            Button userNameButton = new Button(); 
                            userNameButton.Tag = activityService.targetActivity.participants[i];
                            userNameButton.Click += UserButton_Click;
                            userNameButton.Padding = new Thickness(10, 5, 10, 5);
                            userNameButton.Width = 200;
                            userNameButton.Background = new SolidColorBrush(Colors.LightYellow);
                            userNameButton.Cursor = Cursors.Hand;
                            userNameButton.Content = $"{activityService.targetActivity.participants[i].firstName} {activityService.targetActivity.participants[i].lastName}";
                            userPanel.Children.Add(userNameButton);
                            // "x" knap til at fjerne bruger fra aktivitet
                            Button userRemoveButton = new Button();
                            userRemoveButton.Tag = activityService.targetActivity.participants[i];
                            userRemoveButton.Click += RemoveUserFromActivityButton_Click;
                            userRemoveButton.Padding = new Thickness(10, 5, 10, 5);
                            userRemoveButton.Margin = new Thickness(20, 0, 0, 0);
                            //userRemoveButton.Width = 200;
                            userRemoveButton.Background = new SolidColorBrush(Colors.Red);
                            userRemoveButton.Content = "X";
                            userRemoveButton.Cursor = Cursors.Hand;
                            userPanel.Children.Add(userRemoveButton);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// opdater knapper for Join/Leave
        /// </summary>
        /// <author> Rasmus </author>
        private void RedrawAttendingButtons()
        {
            AttendingActionPanel.Children.Clear();
            if (userService.authenticatedUser != null)
            {
                bool activityExists = false;
                for (int j = 0; j < userService.authenticatedUser.activityList.Count; j++) // tjekker om brugeren allerede deltager i aktiviteten 
                {
                    if(activityService.targetActivity != null)
                    {
                        if (userService.authenticatedUser.activityList[j] == activityService.targetActivity)
                        {
                            activityExists = true;
                        }
                    }

                }
                if (activityExists) // viser "Leave" knappen hvis brugeren deltager, ellers vises "Join" knappen 
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
        //Navigere til redigeringssiden for den bruger der er valgt
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            userService.targetUser = user;

            router.Navigate(NavigationRouter.Route.EditUser);
        }
        // Navigere tilbage til aktivitetslisten 
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }

        // brugeren tilmelder sig aktiviteten 
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
        // brugeren framelder sig aktiviten 
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

        // Admin fjerner en bruger fra aktiviteten 
        private void RemoveUserFromActivityButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            if(activityService.targetActivity != null)
            {
                activityService.LeaveActitvity(activityService.targetActivity, user);
                RedrawAttendingButtons();
                RedrawAttenders();
            }
        }

        // Lukker redigeringen og navigere tilbage til aktivitetoversigt
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }
        // gemmer ændringer der er foretaget
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isMaxCapNumber = int.TryParse(CapacityBox.Text, out int maxCap); // validering for om kapacitet er et tal
            int? maxCapcacity;
            if(isMaxCapNumber)
            {
                maxCapcacity = maxCap;
            }
            else
            {
                maxCapcacity = null;
            }
            // validerer datoerne
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
            // konvertering af datoer og tider til DateTime objekter
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
            // gemmer ændringer, hvis aktiviteten findes
            if (activityService.targetActivity != null)
            {
                activityService.targetActivity.title = TitleBox.Text;
                activityService.targetActivity.maxCapacity = maxCapcacity;
                activityService.targetActivity.coach = coaches[CoachPicker.SelectedIndex];
                activityService.targetActivity.location = locationService.locations[LocationPicker.SelectedIndex];

                activityService.targetActivity.startTime = startDateTime;
                activityService.targetActivity.endTime = endDateTime; 

                router.Navigate(NavigationRouter.Route.Activities); // navigere tilbage til oversigten 
            }
        }

        private void AddUsersToActivityButton_Click(object sender, RoutedEventArgs e)
        {
            if (activityService.targetActivity != null) 
            {  // åbner vindue med valg af brugere der kan tilføjes til aktiviteten 
                AddUserToActivity addNewUserWindow = new AddUserToActivity(activityService, userService, activityService.targetActivity);
                addNewUserWindow.ShowDialog();
                RedrawAttendingButtons(); // opdater efter vinduet lukkes
                RedrawAttenders();
            }
        }
        // Navigere til redigeringssiden for den aktivitet der er valgt (anden visning)
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.EditActivity);
        }
    }
}
