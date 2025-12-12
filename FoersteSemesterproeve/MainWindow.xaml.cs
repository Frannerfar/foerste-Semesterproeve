using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Presentation;
using System.Windows;
using System.Windows.Controls;
using static FoersteSemesterproeve.Presentation.NavigationRouter;

namespace FoersteSemesterproeve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class MainWindow : Window
    {
        NavigationRouter router;

        MembershipService membershipService;
        UserService userService;
        ActivityService activityService;
        LocationService locationService;

        // Sæt til false, hvis der skal testes med login
        bool isDeveloping = true;


        /// <summary>
        ///     Constructor for MainWindow
        /// </summary>
        /// <author>Martin</author>
        public MainWindow()
        {
            InitializeComponent();
            // Instantierer en ny liste af buttons List<Button> og sættes i variablen adminButtons
            List<Button> adminButtons = new List<Button>();

            // XAML buttons i MainWindow.xaml der kan tilgås pga. deres x:Name tilføjes til liste af adminButton List<Button>
            //adminButtons.Add(TrainersButton);
            adminButtons.Add(LocationsButton);
            adminButtons.Add(MembershipsButton);
            adminButtons.Add(MembersButton);

            // MembershipService objekt instantieres og gemmes i field i MainWindow.
            membershipService = new MembershipService();
            // LocationService objekt instantieres og gemmes i field i MainWindow.
            locationService = new LocationService();

            // Instantiering hvor membershipService gives som argumenter
            // MembershipService: da vi i UserService er nødt til at kende til MembershipTypes oprettet, 
            //                    for at give folk muligheden for at vælge korrekt MembershipType.
            userService = new UserService(membershipService);
            
            // ActivityService objekt instantieres og sættes i field i MainWindow.
            // har argumenterne locationService og userService som er instantieret få linjer ovenfor
            // Denne ActivityService kan derfor først instantieres efter LocationService og UserService, da ActivityService constructor
            // Kræver at disse gives med som argumenter.
            activityService = new ActivityService(locationService, userService);



            // Instantiering af vores hjemmebygget router (NavigationRouter)
            // En central del af vores applikation, da den håndterer routing mellem forskellige pages (i vores tilfælde UserControls,
            // da UserControls er reuseable components der er lettere at bruge).
            router = new NavigationRouter(MainContent, MenuGrid, UserProfileButton, adminButtons, userService, activityService, locationService, membershipService, HomeButton, ActivitiesButton, TrainersButton, LocationsButton, MembersButton, MembershipsButton);


            // Hvis vi "udvikler/tester" så sættes admin som standard person logget ind
            if (isDeveloping)
            {
                // Looper igennem alle brugere i systemet
                for(int i = 0; i < userService.users.Count; i++)
                {
                    // Hvis brugerens email er det samme som "admin@admin.dk"
                    if (userService.users[i].email == "admin@admin.dk")
                    {
                        // authenticatedUser field i userService sættes til at være denne user.
                        userService.authenticatedUser = userService.users[i];
                    }
                }
                //Der navigeres fra start til Home, hvis vi er i "developing" mode.
                router.Navigate(Route.Home);
                // Vis hovedmenuen, da vi ikke starter på login page
                MenuGrid.Visibility = Visibility.Visible;
            }
            // Hvis vi kører med login skærm fra start.
            else
            {
                // Naviger direkte til LoginPage
                router.Navigate(Route.Login);
            }
        }


        /// <summary>
        ///     Navigerer til routen "Home" ved hjælp af vores router.
        /// Sætter "Home" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            router.Navigate(Route.Home);
        }

        /// <summary>
        ///     Navigerer til routen "Activities" ved hjælp af vores router.
        ///     Sætter "Activities" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            router.Navigate(Route.Activities);
        }

        /// <summary>
        ///     Navigerer til routen "Trainers" ved hjælp af vores router.
        ///     Sætter "Trainers" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            router.Navigate(Route.Trainers);
        }

        /// <summary>
        ///     Navigerer til routen "Locations" ved hjælp af vores router.
        ///     Sætter "Locations" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            router.Navigate(Route.Locations);
        }

        /// <summary>
        ///     Navigerer til routen "Members" ved hjælp af vores router.
        ///     Sætter "Members" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Members_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            router.Navigate(Route.Users);
        }

        /// <summary>
        ///     Navigerer til routen "Memberships" ved hjælp af vores router.
        ///     Sætter "Memberships" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Memberships_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            //SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.MembershipTypes);
        }

        /// <summary>
        ///     Navigerer til routen "Profile" ved hjælp af vores router.
        ///     Sætter "Profile" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Profile);
        }


        /// <summary>
        ///     Fjerner brugeren fra at være vores "authenticatedUser".
        ///     Defaulter valgte aktive menu knap tilbage til "Home".
        ///     Gør menuen midlertidig usynlig, så længe at vi er på "Login" route.
        ///     Navigerer til routen "Login" ved hjælp af vores router.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            // Logget ind user sættes til null
            userService.authenticatedUser = null;
            // Hovedmenuen skal kollapse, så der ikke kan navigeres fra login siden
            MenuGrid.Visibility = Visibility.Collapsed;
            // Naviger til LoginPage
            router.Navigate(Route.Login);
        }
    }
}