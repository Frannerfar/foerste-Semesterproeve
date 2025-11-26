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

        UserService userService = new UserService();
        ActivityService activityService = new ActivityService();
        LocationService locationService = new LocationService();
        MembershipService membershipService = new MembershipService();

        // Sæt til false, hvis der skal testes med login
        bool isDeveloping = true;

        public MainWindow()
        {
            InitializeComponent();
            router = new NavigationRouter(MainContent, MenuGrid, userService, activityService, locationService, membershipService);

            // Hvis vi "udvikler/tester"
            if(isDeveloping)
            {
                router.Navigate(Route.Home);
                MenuGrid.Visibility = Visibility.Visible;
            }
            // Hvis vi kører med login skærm fra start.
            else
            {
                router.Navigate(Route.Login);
            }
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            DialogBox dialogBox = new DialogBox("Kan du lide hotdog?");
            dialogBox.ShowDialog();
            if(dialogBox.DialogResult == true)
            {
                // HVIS JA
                MessageBox.Show("NICE!");
            }
            else
            {
                // HVIS NEJ
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Home);
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Activities);
        }

        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Trainers);
        }

        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Locations);
        }

        private void Members_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Members);
        }

        private void Memberships_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Memberships);
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(Route.Profile);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Collapsed;
            router.Navigate(Route.Login);
        }
    }
}