using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Presentation;
using FoersteSemesterproeve.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FoersteSemesterproeve.Presentation.NavigationRouter;

namespace FoersteSemesterproeve
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NavigationRouter router;

        MembershipService membershipService;
        UserService userService;
        ActivityService activityService;
        LocationService locationService;


        // Sæt til false, hvis der skal testes med login
        bool isDeveloping = true;

        public MainWindow()
        {
            InitializeComponent();
            List<Button> adminButtons = new List<Button>();
            //adminButtons.Add(TrainersButton);
            adminButtons.Add(LocationsButton);
            adminButtons.Add(MembershipsButton);
            adminButtons.Add(MembersButton);

            membershipService = new MembershipService();
            locationService = new LocationService();

            // Instantiering hvor membershipService gives som argumenter
            // MembershipService: da vi i UserService er nødt til at kende til MembershipTypes oprettet, 
            //                    for at give folk muligheden for at vælge korrekt MembershipType.
            userService = new UserService(membershipService);
            
            activityService = new ActivityService(locationService, userService);

            // Instantiering af vores hjemmebygget router (NavigationRouter)
            // En central del af vores applikation, da den håndterer routing mellem forskellige pages (i vores tilfælde UserControls,
            // da UserControls er reuseable components der er lettere at bruge).

            router = new NavigationRouter(MainContent, MenuGrid, UserProfileButton, adminButtons, userService, activityService, locationService, membershipService, HomeButton, ActivitiesButton, TrainersButton, LocationsButton, MembersButton, MembershipsButton);


            // Hvis vi "udvikler/tester"
            if (isDeveloping)
            {
                for(int i = 0; i < userService.users.Count; i++)
                {
                    if (userService.users[i].email == "admin@admin.dk")
                    {
                        userService.authenticatedUser = userService.users[i];
                    }
                }
                router.Navigate(Route.Home);
                MenuGrid.Visibility = Visibility.Visible;
            }
            // Hvis vi kører med login skærm fra start.
            else
            {
                router.Navigate(Route.Login);
            }
        }


        /// <summary>
        /// Navigerer til routen "Home" ved hjælp af vores router.
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
        /// Navigerer til routen "Activities" ved hjælp af vores router.
        /// Sætter "Activities" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            //SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Activities);
        }

        /// <summary>
        /// Navigerer til routen "Trainers" ved hjælp af vores router.
        /// Sætter "Trainers" knappen til at være den nye aktive knap.
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
        /// Navigerer til routen "Locations" ved hjælp af vores router.
        /// Sætter "Locations" knappen til at være den nye aktive knap.
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
        /// Navigerer til routen "Members" ved hjælp af vores router.
        /// Sætter "Members" knappen til at være den nye aktive knap.
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
        /// Navigerer til routen "Memberships" ved hjælp af vores router.
        /// Sætter "Memberships" knappen til at være den nye aktive knap.
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
        /// Navigerer til routen "Profile" ved hjælp af vores router.
        /// Sætter "Profile" knappen til at være den nye aktive knap.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Profile);
        }


        /// <summary>
        /// Fjerner brugeren fra at være vores "authenticatedUser".
        /// Defaulter valgte aktive menu knap tilbage til "Home".
        /// Gør menuen midlertidig usynlig, så længe at vi er på "Login" route.
        /// Navigerer til routen "Login" ved hjælp af vores router.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            userService.authenticatedUser = null;
            MenuGrid.Visibility = Visibility.Collapsed;
            router.Navigate(Route.Login);
        }
    }
}