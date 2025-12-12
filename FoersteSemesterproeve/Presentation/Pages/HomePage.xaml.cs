using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class HomePage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;

        List<Activity> upcomingActivities;

        /// <summary>
        ///     Constructor til HomePage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        /// <param name="activityService"></param>
        public HomePage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();

            // Parametrene fra constructoren sættes i tilsvarende fields
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;
            // Ny list af aktiviteter instantieres og sættes i upcomingActivities field
            this.upcomingActivities = new List<Activity>();

            // Hvilket tidspunkt det er lige nu sættes i en variabel "now".
            DateTime now = DateTime.Now;

            // Her ville det give mening at bruge LINQ OrderBy eller lave sin egen sortering. Men det er uden for scope.
            // Hvis personen er authenticated (altså logget ind - et simpelt null check, for at sikre at den ikke er tom)
            if (userService.authenticatedUser != null)
            {
                // Sæt fornavnet på personen der er logget ind, til "AuthenticatedUserName" tekstblok i XAML
                AuthenticatedUserName.Text = $"Hello, {userService.authenticatedUser.firstName}!";
                // for hver aktivitet den authenticated bruger har på sin liste af aktiviteter
                for (int i = 0;  i < userService.authenticatedUser.activityList.Count; i++)
                {
                    // hvis aktiviteten er højere (dermed nyere) end tidspunktet lige nu (ud fra variablen "now")
                    if (userService.authenticatedUser.activityList[i].startTime > now)
                    {
                        // tilføj aktiviteten til listen af kommende aktiviteter
                        upcomingActivities.Add(userService.authenticatedUser.activityList[i]);
                    }
                }
            }

            // Hjælpe variabler
            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2; // definerer hvor mange elementer der må være på samme række (altså antallet af kolonner per række)

            // Hvis antallet af kommende aktiviteter for brugeren er over 0
            if (upcomingActivities.Count > 0) {
                // Loop igennem alle kommende aktiviteter
                for (int i = 0; i < upcomingActivities.Count; i++)
                {
                    // Her bruges der modulus, som dividerer, men kun returnerer den resterende mængde fra divisionen
                    // variablen i (tælleren / iterationen) divideres med itemsPerRow(som pt. er 2)
                    // Det resterende sættes i variablen "iRemainder".
                    iRemainder = i % itemsPerRow;
                    // Hvis iRemainder == 0, så betyder det enten at i er 0, eller i kan gå lige op i 2.
                    // Dermed er det enten en start, eller hver anden gang siden.
                    if (iRemainder == 0)
                    {
                        // Der tilføjes ny row/række/linje til UpcomingActivitiesGrid i XAML
                        UpcomingActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                        // Hvis der ikke er nogle rows endnu
                        if (rows == 0)
                        {
                            // Så tilføj en kolonne også til UpcomingActivitiesGrid
                            UpcomingActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            // Og tæl antallet af kolonner op.
                            columns++;
                        }
                        // efter at der er tilføjet en række, så kan vi tælle rows op med 1.
                        rows++;

                    }
                    // Hvis iRemainder ikke er 0 og antallet af totale kolonner er mindre end antallet af kolonner per række
                    if (iRemainder != 0 && columns < itemsPerRow)
                    {
                        // Tilføj en ny række
                        UpcomingActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        // Tæl 1 op i antallet af rækker
                        columns++;
                    }

                    // Element til at indeholder aktivitet starter her med en border der instantieres
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = new SolidColorBrush(Colors.Black);
                    // borderen sættes til row - 1
                    Grid.SetRow(border, rows - 1);
                    // og kolonne 0 eller 1, baseret ud fra iRemainder
                    Grid.SetColumn(border, iRemainder);
                    border.MaxWidth = 300;
                    border.Margin = new Thickness(0, 0, 0, 20);
                    border.Padding = new Thickness(10);

                    // StackPanel instantieres og sættes som child til border ovenfor
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(0);
                    border.Child = stackPanel;


                    // HEREFTER LAVES DER TEXTBLOCKS DER KAN INDSÆTTES I STACKPANEL


                    // HOLD NAVN SEKTION
                    TextBlock nameTextBlock = new TextBlock();
                    nameTextBlock.Text = $"{upcomingActivities[i].title}";
                    nameTextBlock.FontSize = 18;
                    nameTextBlock.FontWeight = FontWeights.Bold;
                    nameTextBlock.Margin = new Thickness(0, 0, 0, 10);
                    stackPanel.Children.Add(nameTextBlock);


                    // CAPACITY SEKTION
                    string capacityString;
                    // Hvis aktivitetens maksimale kapcitet ikke er null
                    if (upcomingActivities[i].maxCapacity != null)
                    {
                        // Vis antallet af tilmeldte deltagere ud af mængden af mulige deltagere
                        capacityString = $"Participants: {upcomingActivities[i].participants.Count} / {upcomingActivities[i].maxCapacity}";
                    }
                    // Hvis aktivitetens maksimale kapacitet ER null
                    else
                    {
                        // Så betragter vi det som at aktiviteten ikke er begrænset, og dermed uendelig.
                        capacityString = $"Participants: {upcomingActivities[i].participants.Count} / Unlimited";
                    }
                    TextBlock capacityTextBlock = new TextBlock();
                    capacityTextBlock.Text = capacityString;
                    capacityTextBlock.FontSize = 14;
                    capacityTextBlock.Margin = new Thickness(0, 10, 0, 10);
                    stackPanel.Children.Add(capacityTextBlock);




                    // START SEKTION
                    TextBlock activityStartTimeTextBlock = new TextBlock();
                    activityStartTimeTextBlock.Text = $"Date: {upcomingActivities[i].startTime:dd-MM-yyyy HH:mm}";
                    activityStartTimeTextBlock.FontSize = 14;
                    stackPanel.Children.Add(activityStartTimeTextBlock);


                    // END SEKTION
                    TextBlock activityEndTimeTextBlock = new TextBlock();
                    activityEndTimeTextBlock.Text = $"End: {upcomingActivities[i].endTime:dd-MM-yyyy HH:mm}";
                    activityEndTimeTextBlock.FontSize = 14;
                    stackPanel.Children.Add(activityEndTimeTextBlock);



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

                    UpcomingActivitiesGrid.Children.Add(border);
                }
            }
            // Hvis der ikke er nogle kommende aktiviteter
            else
            {
                // Der instantieres en stackpanel
                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Margin = new Thickness(0);

                // og en textblock sættes som child til stackpanel, hvori der bliver vist teksten "No upcoming activities".
                TextBlock noUpcomingActivitiesBlock = new TextBlock();
                noUpcomingActivitiesBlock.Margin = new Thickness(0, 20, 0, 20);
                noUpcomingActivitiesBlock.FontSize = 16;
                noUpcomingActivitiesBlock.Text = "No upcoming activities";
                noUpcomingActivitiesBlock.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(noUpcomingActivitiesBlock);

                // StackPanel tilføjes til Gridet "UpcomingActivitiesGrid"
                UpcomingActivitiesGrid.Children.Add(stackPanel);
            }
        }


        /// <summary>
        ///     Bruges til at sætte targetActivity til en aktivitet og derefter navigere til "ActivityPage"
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            // Da vi ved at sender er en button, kan vi typecast sender til en Button og lægge den i en variabel "button" af typen Button.
            Button button = (Button)sender;
            // og da vi ved at Button har et Tag reference til en aktivitet, kan vi typecast button.Tag til at være en Activity.
            // og sættes i variablen activity, af typen Activity.
            Activity activity = (Activity)button.Tag;

            // Hvis aktiviteten ikke er null
            if (activity != null)
            {
                // sæt targetActivity i activityService til at være aktiviteten
                activityService.targetActivity = activity;

                // Naviger til "ActivityPage"
                router.Navigate(NavigationRouter.Route.Activity);
            }
        }
    }
}
