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

        public MainWindow()
        {
            InitializeComponent();
            router = new NavigationRouter(MainContent);
            router.Navigate(Route.Home);
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            DialogBox dialogBox = new DialogBox("test test!!:D");
            dialogBox.ShowDialog();
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
    }
}