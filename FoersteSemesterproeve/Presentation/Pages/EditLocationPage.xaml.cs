using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditLocationPage.xaml
    /// </summary>
    /// <author> Rasmus </author>
    public partial class EditLocationPage : UserControl
    {
        NavigationRouter router;
        LocationService locationService;

        public EditLocationPage(NavigationRouter router, LocationService locationService)
        {
            InitializeComponent(); // Loader xaml elementer

            this.router = router;
            this.locationService = locationService;
            if (locationService.targetLocation != null) // hvis der er valgt en lokation, vises dens nuværende oplysninger
            {
                TargetLocationName.Text = locationService.targetLocation.name;

                LocationNameBox.Text = locationService.targetLocation.name;
                LocationDescriptionBox.Text = locationService.targetLocation.description;
                LocationCapacityBox.Text = locationService.targetLocation.maxCapacity.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {// fortsætter kun hvis der er valgt en lokation 
            if (locationService.targetLocation != null)
            {
                bool flag = false;
                bool maxCapacityNull = false;
                if (string.IsNullOrEmpty(LocationNameBox.Text)) // validering på navn input
                {
                    flag = true;
                    LocationNameBox.Visibility = Visibility.Visible;
                }
                if (string.IsNullOrEmpty(LocationDescriptionBox.Text)) // validering på beskrivelse input
                {
                    flag = true;
                    LocationDescriptionBox.Visibility = Visibility.Visible;
                }
                if (string.IsNullOrEmpty(LocationCapacityBox.Text)) // håndtere og validere kapacitet input
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

                if (flag == true) // hvis der sker fejl stopper funktionen 
                {
                    return;
                }
                // opdater oplysninger på lokation
                locationService.targetLocation.name = LocationNameBox.Text;
                locationService.targetLocation.description = LocationDescriptionBox.Text;
                if (maxCapacityNull == true)// sætter kapacitet, eller null for ubegrænset
                {
                    locationService.targetLocation.maxCapacity = null;
                }
                else
                {
                    locationService.targetLocation.maxCapacity = capacity;
                }
                router.Navigate(NavigationRouter.Route.Locations); // navigeres tilbage til lokationsoversigten
            }
        }

    }
}
