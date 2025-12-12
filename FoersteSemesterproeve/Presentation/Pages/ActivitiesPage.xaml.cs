using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FoersteSemesterproeve.Views;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ActivitiesView.xaml
    /// </summary>
    /// <author> Rasmus </author>
    public partial class ActivitiesPage : UserControl
    {
        NavigationRouter router; // styrer hvilket sider der bliver vist
        ActivityService activityService; // Indeholder liste over aktiviteter og logik til tilmeld og afmeld aktivitet
        UserService userService; // Håndtere data om bruger der er logget ind
        /// <summary>
        /// 
        /// </summary>
        /// <author> Rasmus </author>
        /// <param name="navigationRouter"></param>
        /// <param name="activityService"></param>
        /// <param name="userService"></param>
        public ActivitiesPage(NavigationRouter navigationRouter, ActivityService activityService, UserService userService)
        {

            InitializeComponent(); // initialisere de forskellige xaml elementer
            this.router = navigationRouter;
            this.activityService = activityService;
            this.userService = userService;
            // laver et validering tjek på om brugeren er en admin, for at få vist addactivity knap
            if(userService.authenticatedUser != null && userService.authenticatedUser.isAdmin)
            {
                AddActivityButton.Visibility = Visibility.Visible;
            }

            LoadActivities(); // indlæser alle aktiviteter, så de bliver vist
            
        }

        /// <summary>
        /// Sørger for at vise alle aktiviteter 
        /// </summary>
        /// <author> Rasmus </author>
        private void LoadActivities()
        {
            Grid ActivitiesGrid = new Grid(); // Opretter grid til aktiviteter 
            ActivitiesScrollViewer.Content = ActivitiesGrid; // opretter scroll funktionalitet, så man kan scrolle i aktiviteter

            int rows = 0; 
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2;
            for (int i = 0; i < activityService.activities.Count; i++) //Tæller alle aktiviteterne op 
            {
                iRemainder = i % itemsPerRow;
                if (iRemainder == 0) // Hvis resten er 0 starter vi på en ny række
                {
                    ActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    if (rows == 0) // Hvis det er første række, skal bliver der også tilføjet en kolonne
                    {
                        ActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        columns++;
                    }
                    rows++;

                }
                if (iRemainder != 0 && columns < itemsPerRow) // hvis der er plads nok tilføjes der en kolonne mere 
                {
                    ActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    columns++;
                }
                //Der bliver oprettet en ramme omkring aktivitet 
                Border activityBorder = new Border();
                activityBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                activityBorder.BorderThickness = new Thickness(1);
                activityBorder.CornerRadius = new CornerRadius(10);
                activityBorder.MaxWidth = 400;
                activityBorder.Margin = new Thickness(10, 10, 10, 10);
                Grid.SetRow(activityBorder, rows - 1); // indsætter den i den korrekt række 
                Grid.SetColumn(activityBorder, iRemainder); // indsætter i den rigtig kolonne
                ActivitiesGrid.Children.Add(activityBorder); // bliver placeret rigtigt i griddet

                // indeholder alt information omkring aktiviteten tekst og knapper
                StackPanel activityStackPanel = new StackPanel(); 
                activityBorder.Child = activityStackPanel;
                //Hvis brugeren er en admin bliver der vist en delete knap i hver aktivitet
                if (userService.authenticatedUser != null && userService.authenticatedUser.isAdmin)
                {
                    StackPanel buttonsPanel = new StackPanel();
                    buttonsPanel.HorizontalAlignment = HorizontalAlignment.Right;
                    buttonsPanel.Orientation = Orientation.Horizontal;
                    buttonsPanel.Margin = new Thickness(0, 10, 10, 10);
                    activityStackPanel.Children.Add(buttonsPanel);

                    Button deleteButton = new Button();
                    deleteButton.Content = "Delete";
                    deleteButton.Margin = new Thickness(0, 0, 10, 0);
                    deleteButton.Padding = new Thickness(10, 5, 10, 5);
                    deleteButton.Background = new SolidColorBrush(Colors.Red);
                    deleteButton.Click += DeleteButton_Click;
                    deleteButton.Cursor = Cursors.Hand;
                    deleteButton.Tag = activityService.activities[i]; //aktiviteten bliver gemt som tag, så den ved der hører til delete knappen 
                    buttonsPanel.Children.Add(deleteButton);

                }

                
                TextBlock activityTitle = new TextBlock();
                activityTitle.Text = $"{activityService.activities[i].title}";
                activityTitle.FontSize = 20;
                activityTitle.FontWeight = FontWeights.Bold;
                activityTitle.Margin = new Thickness(20, 10, 20, 10);
                activityStackPanel.Children.Add(activityTitle);

                
                string maxCap;
                if(activityService.activities[i].maxCapacity != null)
                {
                    maxCap = $"{activityService.activities[i].maxCapacity}"; // viser antal deltagere og kapaciteten eller ingen begrænsning
                }
                else
                {
                    maxCap = $"Unlimited";
                }
                TextBlock participantsText = new TextBlock();
                participantsText.Text = $"Participants: {activityService.activities[i].participants.Count} / {maxCap}";
                participantsText.FontSize = 16;
                participantsText.Margin = new Thickness(20, 10, 20, 0);
                activityStackPanel.Children.Add(participantsText);

                // Opretter panel til visning til start og slut tid
                StackPanel timesPanel = new StackPanel();
                timesPanel.Margin = new Thickness(20, 10, 20, 10);
                activityStackPanel.Children.Add(timesPanel);
                // start tidspunkt
                TextBlock dateStartText = new TextBlock();
                dateStartText.Text = $"Start: {activityService.activities[i].startTime:dd-MM-yyyy HH:mm}";
                dateStartText.FontSize = 16;
                timesPanel.Children.Add(dateStartText);
                // slut tidspunkt
                TextBlock dateEndText = new TextBlock();
                dateEndText.Text = $"End: {activityService.activities[i].endTime:dd-MM-yyyy HH:mm}";
                dateEndText.FontSize = 16;
                timesPanel.Children.Add(dateEndText);



                // Aktion panel til "See More" knap og "Join"/"Leave" knap.
                StackPanel activityActionPanel = new StackPanel();
                activityActionPanel.Margin = new Thickness(20, 10, 20, 10);
                activityActionPanel.Orientation = Orientation.Horizontal;
                //Sættes som child til "activityStackPanel"
                activityStackPanel.Children.Add(activityActionPanel);


                //når "See More" knap bliver trykket, åbnes den detaljeret udgave af aktiviteten 
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

                if (userService.authenticatedUser != null) // viser enten join eller leave knappen for at tilmelde sig en aktivitet
                {
                    bool activityExists = false;
                    for(int j = 0; j < userService.authenticatedUser.activityList.Count; j++) // itererer igennem for at tjekke om brugeren allerede er på aktiviteten
                    {
                        if (userService.authenticatedUser.activityList[j] == activityService.activities[i])
                        {
                            activityExists = true;
                        }

                    }
                    if(activityExists) // Viser "Leave knap", når brugeren er tilmeldt aktiviteten 
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
                    else // viser "join knappen", hvis brugeren ikke eksisterer på aktiviteten 
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




        // Navigere hen til siden hvor den detaljeret version af aktiviteten vises
        private void SeeActivityButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            activityService.targetActivity = activity;

            router.Navigate(NavigationRouter.Route.Activity);
        }
        // Navigeres til redigeringssiden for den bestemte aktivitet man har valgt
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            activityService.targetActivity = activity;

            router.Navigate(NavigationRouter.Route.EditActivity);
        }

        // Sletter en aktivitet, samt viser en dialogbox om man er sikker 
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if(activity != null )
            {

                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete '{activity.title}'"); // Dialogbox pop-up
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true) // hvis "OK" slettes aktiviteten
                {
                    activityService.activities.Remove(activity);
                    LoadActivities(); // opdaterer UI efter sletning
                }
            }
        }

        // brugeren tilmelder sig en aktivitet
        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if (userService.authenticatedUser != null)
            {
                activityService.JoinActivity(activity, userService.authenticatedUser);
                LoadActivities(); // skifter rundt på knapperne, så der står "Leave"
            }
        }
        // brugeren afmelder sig en aktivitet
        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Activity activity = (Activity)button.Tag;

            if (userService.authenticatedUser != null)
            {
                activityService.LeaveActitvity(activity, userService.authenticatedUser);
                LoadActivities(); // skifter rundt på knapperne, så der står "join"
            }
        }
        // Navigere til siden hvor man kan tilføje en ny aktivitet
        private void AddNewActivity_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddActivity);
        }
    }
}
