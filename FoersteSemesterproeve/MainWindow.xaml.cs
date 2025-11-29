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

        SolidColorBrush menuStaticItem;
        SolidColorBrush menuActiveItem;

        Color menuStaticItemColor = Color.FromRgb(119, 136, 153);
        Color menuActiveItemColor = Color.FromRgb(177, 211, 245);

        Button currentActiveMenuButton;

        // Sæt til false, hvis der skal testes med login
        bool isDeveloping = true;

        public MainWindow()
        {
            InitializeComponent();
            List<Button> adminButtons = new List<Button>();
            adminButtons.Add(TrainersButton);
            adminButtons.Add(LocationsButton);
            adminButtons.Add(MembershipsButton);
            adminButtons.Add(MembersButton);

            membershipService = new MembershipService();
            userService = new UserService(membershipService);
            activityService = new ActivityService();
            locationService = new LocationService();
            router = new NavigationRouter(MainContent, MenuGrid, UserProfileButton, adminButtons, userService, activityService, locationService, membershipService);

            menuStaticItem = new SolidColorBrush(menuStaticItemColor);
            menuActiveItem = new SolidColorBrush(menuActiveItemColor);

            currentActiveMenuButton = HomeButton;
            SetMenuButtonActive(currentActiveMenuButton, HomeButton);

            // Hvis vi "udvikler/tester"
            if (isDeveloping)
            {
                router.Navigate(Route.Home);
                MenuGrid.Visibility = Visibility.Visible;
                userService.authenticatedUser = userService.users.FirstOrDefault(u => u.email == "admin@admin.dk");
            }
            // Hvis vi kører med login skærm fra start.
            else
            {
                router.Navigate(Route.Login);
            }
        }

        private void SetMenuButtonActive(Button originalButton, Button newButton)
        {
            if (originalButton != null) 
            {
                originalButton.Background = menuStaticItem;
            }

            newButton.Background = menuActiveItem;
            currentActiveMenuButton = newButton;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Home);
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Activities);
        }

        private void Trainers_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Trainers);
        }

        private void Locations_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Locations);
        }

        private void Members_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
            router.Navigate(Route.Members);
        }

        private void Memberships_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SetMenuButtonActive(currentActiveMenuButton, button);
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