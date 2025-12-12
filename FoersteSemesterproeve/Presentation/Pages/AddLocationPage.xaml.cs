using FoersteSemesterproeve.Domain.Services;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for AddLocationPage.xaml
    /// </summary>
    /// <author> Rasmus </author>
    public partial class AddLocationPage : UserControl
    {
        NavigationRouter router;
        LocationService locationService;
        /// <summary>
        ///
        /// </summary>
        /// <author> Rasmus </author>
        public AddLocationPage(NavigationRouter router, LocationService locationService)
        {
            InitializeComponent(); // Loader xaml elementer 
            this.router = router;
            this.locationService = locationService;
        }
        // når bruger er ved at tilføje en ny lokation og trykker save
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false; // bruges til at se om der er fejl i input
            bool maxCapacityNull = false; // bruges til at vise om kapacitet skal være ubegrænset
            int? maxCapacity; // faktiske værdi for kapacitet
            LocationNameFlag.Visibility = Visibility.Collapsed;
            LocationDescriptionFlag.Visibility = Visibility.Collapsed;
            LocationCapacityFlag.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(LocationNameBox.Text)) // tjekker om der er indtastet et navn 
            {
                flag = true; // Der er fejl
                LocationNameFlag.Visibility = Visibility.Visible; // viser advarsel
            }
            else
            {
                LocationNameFlag.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(LocationDescriptionBox.Text)) // tjekker om beskrivelsen er indtastet
            {
                flag = true;
                LocationDescriptionFlag.Visibility = Visibility.Visible;
            }
            else
            {
                LocationDescriptionFlag.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(LocationCapacityBox.Text)) // håndtere kapacitet
            {
                maxCapacityNull = true; // hvis feltet er tomt = ingen begrænsninger
            }
            else
            { // der er skrevet noget i feltet som skal valideres som et tal 
                LocationCapacityFlag.Visibility = Visibility.Collapsed;
            }

            bool result = int.TryParse(LocationCapacityBox.Text, out int capacity);
            if(!result && maxCapacityNull == false) // validere om inputtet er et tal, hvis ikke udskrives besked 
            {
                flag = true;
                LocationCapacityFlag.Visibility = Visibility.Visible;
                MessageBox.Show("Please use valid numbers, if you wish to limit capacity");
            }

            // hvis der er fejl stoppes der og venter på rettelse
            if (flag == true)
            {
                return;
            }

            // sætter den endelige værdi for kapacitet     
            if(maxCapacityNull == true)
            {
                maxCapacity = null; // ingen kapacitets grænse
            }
            else
            {
                maxCapacity = capacity; // brugerens indtastet kapacitet
            }

            locationService.AddLocation(LocationNameBox.Text, LocationDescriptionBox.Text, maxCapacity); // tilføjer lokation til systemet via LocationService

            router.Navigate(NavigationRouter.Route.Locations); // Navigere tilbage til oversigten over lokationer
        }
    
    }
}
