using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for UserActivitiesPage.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class UserActivitiesPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;

        /// <summary>
        ///     Constructor til UserActivitiesPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        /// <param name="activityService"></param>
        public UserActivitiesPage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();

            // parametrene fra constructoren sættes i fields
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;

            // Tekst felt på siden sættes til at være targetUsers fornavn og efternavn.
            // targetUser er den person, som der blev trykket på, inden der blev navigeret til denne side
            // Det er den måde vi ved, hvilken person vi skal hive data fra.
            UserText.Text = $"{userService.targetUser?.firstName} {userService.targetUser?.lastName}";
            
            // Hjælpe variabler
            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2; // definerer hvor mange elementer der må være på samme række (altså antallet af kolonner per række)

            if (userService.targetUser != null)
            {
                // Hvis antallet af antallet af aktiviteter targetUser har på sin aktivitetsliste er over 0
                if (userService.targetUser.activityList.Count > 0)
                {
                    // Loop igennem alle targetUsers aktiviteter i aktivitetsliste
                    for (int i = 0; i < userService.targetUser.activityList.Count; i++)
                    {
                        // Her bruges der modulus, som dividerer, men kun returnerer den resterende mængde fra divisionen
                        // variablen i (tælleren / iterationen) divideres med itemsPerRow(som pt. er 2)
                        // Det resterende sættes i variablen "iRemainder".
                        iRemainder = i % itemsPerRow;
                        // Hvis iRemainder == 0, så betyder det enten at i er 0, eller i kan gå lige op i 2.
                        // Dermed er det enten en start, eller hver anden gang siden.
                        if (iRemainder == 0)
                        {
                            // Der tilføjes ny row/række/linje til UserActivitiesGrid i XAML
                            UserActivitiesGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            // Hvis der ikke er nogle rows endnu
                            if (rows == 0)
                            {
                                // Så tilføj en kolonne også til UserActivitiesGrid
                                UserActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
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
                            UserActivitiesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            // Tæl 1 op i antallet af rækker
                            columns++;
                        }

                        // Element til at indeholde aktivitet starter her med en border der instantieres
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
                        nameTextBlock.Text = $"{userService.targetUser.activityList[i].title}";
                        nameTextBlock.FontSize = 18;
                        nameTextBlock.FontWeight = FontWeights.Bold;
                        nameTextBlock.Margin = new Thickness(0, 0, 0, 10);
                        stackPanel.Children.Add(nameTextBlock);



                        // CAPACITY SEKTION
                        string capacityString;
                        // Hvis aktivitetens maksimale kapcitet ikke er null
                        if (userService.targetUser.activityList[i].maxCapacity != null)
                        {
                            // Vis antallet af tilmeldte deltagere ud af mængden af mulige deltagere
                            capacityString = $"Participants: {userService.targetUser.activityList[i].participants.Count} / {userService.targetUser.activityList[i].maxCapacity}";
                        }
                        else
                        {
                            // Så betragter vi det som at aktiviteten ikke er begrænset, og dermed uendelig.
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
                // Hvis der ikke er nogle aktiviteter på targetUsers aktivitetsliste
                else
                {
                    // Der instantieres en stackpanel
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Margin = new Thickness(0);

                    // og en textblock sættes som child til stackpanel, hvori der bliver vist teksten "No activities".
                    TextBlock noUpcomingActivitiesBlock = new TextBlock();
                    noUpcomingActivitiesBlock.Margin = new Thickness(0, 20, 0, 20);
                    noUpcomingActivitiesBlock.FontSize = 16;
                    noUpcomingActivitiesBlock.Text = "No activities";
                    noUpcomingActivitiesBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(noUpcomingActivitiesBlock);

                    // StackPanel tilføjes til Gridet "UserActivitiesGrid"
                    UserActivitiesGrid.Children.Add(stackPanel);
                }
        
            }
        }


        /// <summary>
        ///     Bruges til at navigere til "ActivityPage" som er data på en specific aktivitet
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            // Da vi ved at sender er en Button, kan vi typecast sender til at være en Button
            Button button = (Button)sender;
            // og da vi ved at Button har en aktivitet på Tag referencen,
            // så kan vi typecast button.Tag til at være en Activity.
            Activity activity = (Activity)button.Tag;

            // Hvis aktiviteten IKKE er null
            if (activity != null)
            {
                // så sættes targetActivity til at være aktiviteten
                activityService.targetActivity = activity;
                // Der navigeres til "ActivityPage"
                router.Navigate(NavigationRouter.Route.Activity);
            }
        }
    }
}
