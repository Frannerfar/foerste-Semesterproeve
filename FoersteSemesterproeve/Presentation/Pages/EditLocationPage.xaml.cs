using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditLocationPage.xaml
    /// </summary>
    public partial class EditLocationPage : UserControl
    {
        NavigationRouter router;
        LocationService locationService;

        public EditLocationPage(NavigationRouter router, LocationService locationService)
        {
            InitializeComponent();

            this.router = router;
            this.locationService = locationService;
            if (locationService.targetLocation != null)
            {
                TargetLocationName.Text = locationService.targetLocation.name;

                LocationNameBox.Text = locationService.targetLocation.name;
                LocationDescriptionBox.Text = locationService.targetLocation.description;
                LocationCapacityBox.Text = locationService.targetLocation.maxCapacity.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (locationService.targetLocation != null)
            {
                bool flag = false;
                bool maxCapacityNull = false;
                if (string.IsNullOrEmpty(LocationNameBox.Text))
                {
                    flag = true;
                    LocationNameBox.Visibility = Visibility.Visible;
                }
                if (string.IsNullOrEmpty(LocationDescriptionBox.Text))
                {
                    flag = true;
                    LocationDescriptionBox.Visibility = Visibility.Visible;
                }
                if (string.IsNullOrEmpty(LocationCapacityBox.Text))
                {
                    flag = true;
                    maxCapacityNull = true;
                    LocationCapacityFlag.Visibility = Visibility.Visible;
                }

                bool result = int.TryParse(LocationCapacityBox.Text, out int capacity);
                if (!result && maxCapacityNull == false)
                {
                    flag = true;
                    LocationCapacityFlag.Visibility = Visibility.Visible;
                    MessageBox.Show("Please use valid numbers, if you wish to limit capacity");
                }

                if (flag == true)
                {
                    return;
                }

                locationService.targetLocation.name = LocationNameBox.Text;
                locationService.targetLocation.description = LocationDescriptionBox.Text;
                if (maxCapacityNull == true)
                {
                    locationService.targetLocation.maxCapacity = null;
                }
                else
                {
                    locationService.targetLocation.maxCapacity = capacity;
                }
                router.Navigate(NavigationRouter.Route.Locations);
            }
        }

    }
}
