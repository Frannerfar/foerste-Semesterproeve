using FoersteSemesterproeve.Domain.Services;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for AddLocationPage.xaml
    /// </summary>
    public partial class AddLocationPage : UserControl
    {
        NavigationRouter router;
        LocationService locationService;

        public AddLocationPage(NavigationRouter router, LocationService locationService)
        {
            InitializeComponent();
            this.router = router;
            this.locationService = locationService;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            bool maxCapacityNull = false;
            int? maxCapacity;
            LocationNameFlag.Visibility = Visibility.Collapsed;
            LocationDescriptionFlag.Visibility = Visibility.Collapsed;
            LocationCapacityFlag.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(LocationNameBox.Text))
            {
                flag = true;
                LocationNameFlag.Visibility = Visibility.Visible;
            }
            else
            {
                LocationNameFlag.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(LocationDescriptionBox.Text))
            {
                flag = true;
                LocationDescriptionFlag.Visibility = Visibility.Visible;
            }
            else
            {
                LocationDescriptionFlag.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(LocationCapacityBox.Text))
            {
                maxCapacityNull = true;
            }
            else
            {
                LocationCapacityFlag.Visibility = Visibility.Collapsed;
            }

            bool result = int.TryParse(LocationCapacityBox.Text, out int capacity);
            if(!result && maxCapacityNull == false)
            {
                flag = true;
                LocationCapacityFlag.Visibility = Visibility.Visible;
                MessageBox.Show("Please use valid numbers, if you wish to limit capacity");
            }


            if (flag == true)
            {
                return;
            }

                
            if(maxCapacityNull == true)
            {
                maxCapacity = null;
            }
            else
            {
                maxCapacity = capacity;
            }

            locationService.AddLocation(LocationNameBox.Text, LocationDescriptionBox.Text, maxCapacity);

            router.Navigate(NavigationRouter.Route.Locations);
        }
    
    }
}
