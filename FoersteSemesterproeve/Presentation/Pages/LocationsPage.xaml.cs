using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LocationsPage.xaml
    /// </summary>
    /// <author>Rasmus</author>
    public partial class LocationsPage : UserControl
    {
        NavigationRouter router;
        LocationService locationService;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Rasmus</author>
        /// <param name="router"></param>
        /// <param name="locationService"></param>
        public LocationsPage(NavigationRouter router, LocationService locationService)
        {
            InitializeComponent(); // loader alle xaml elementer

            this.router = router;
            this.locationService = locationService;

            DrawLocations(); // viser alle lokationer når siden indlæses
        }


        /// <summary>
        /// Tegner hele listen af lokationer op i et grid
        /// </summary>
        /// <author>Rasmus</author>
        private void DrawLocations()
        {// rydder tidligere indhold og genopbygger UI
            GridLocations.Children.Clear();
            GridLocations.RowDefinitions.Clear();
            GridLocations.ColumnDefinitions.Clear();

            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int itemsPerRow = 2; // Der skal vises 2 lokationer per række
            for (int i = 0; i < locationService.locations.Count; i++) // looper igennem alle lokationer 
            {
                iRemainder = i % itemsPerRow;
                if (iRemainder == 0) // opretter ny række når dere startes en ny linje i layoutet
                {
                    GridLocations.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    if (rows == 0) // hvis det er første rækker, opretter også kolonner
                    {
                        GridLocations.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        columns++;
                    }
                    rows++;
                }
                if (iRemainder != 0 && columns < itemsPerRow) // tilføjer ekstra kolonne hvis der er plads i rækken 
                {
                    GridLocations.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    columns++;
                }
                // oprettelse af kortet for en lokation 
                Border border = new Border();
                Grid.SetRow(border, rows - 1);
                Grid.SetColumn(border, iRemainder);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(1);
                border.Padding = new Thickness(20);
                border.CornerRadius = new CornerRadius(20);
                border.MaxWidth = 550;
                border.Margin = new Thickness(10);

                GridLocations.Children.Add(border);

                StackPanel stackPanel = new StackPanel();
                border.Child = stackPanel;
                // Lokation navn
                TextBlock nameTextBlock = new TextBlock();
                nameTextBlock.Text = locationService.locations[i].name;
                nameTextBlock.FontSize = 18;
                nameTextBlock.Margin = new Thickness(0, 0, 0, 10);
                nameTextBlock.FontWeight = FontWeights.Bold;
                stackPanel.Children.Add(nameTextBlock);
                // lokation beskrivelse
                TextBlock descriptionTextBlock = new TextBlock();
                descriptionTextBlock.Text = locationService.locations[i].description;
                descriptionTextBlock.Margin = new Thickness(0, 10, 0, 10);
                descriptionTextBlock.FontSize = 14;
                stackPanel.Children.Add(descriptionTextBlock);


                // max kapacitet
                string maxCapacityText;
                if (locationService.locations[i].maxCapacity != null)
                {
                    maxCapacityText = $"Max Capacity: {locationService.locations[i].maxCapacity}";
                }
                else
                {
                    maxCapacityText = "Max Capacity: No max capacity";
                }
                
                TextBlock maxCapacityTextBlock = new TextBlock();
                maxCapacityTextBlock.Text = maxCapacityText;
                descriptionTextBlock.Margin = new Thickness(0, 10, 0, 10);
                maxCapacityTextBlock.FontSize = 14;
                stackPanel.Children.Add(maxCapacityTextBlock);





                StackPanel buttonsPanel = new StackPanel();
                buttonsPanel.Orientation = Orientation.Horizontal;
                buttonsPanel.HorizontalAlignment = HorizontalAlignment.Right;
                buttonsPanel.Margin = new Thickness(0, 20, 0, 0);
                stackPanel.Children.Add(buttonsPanel);
                // redigereingsknap 
                Button buttonEdit = new Button();
                buttonEdit.Content = "Edit";
                buttonEdit.Padding = new Thickness(10, 5, 10, 5);
                buttonEdit.Margin = new Thickness(5);
                buttonEdit.Cursor = Cursors.Hand;
                buttonEdit.Click += EditButton_Click;
                buttonEdit.Tag = locationService.locations[i];
                buttonEdit.Background = new SolidColorBrush(Colors.LawnGreen);
                buttonsPanel.Children.Add(buttonEdit);
                // Slet knap
                Button buttonDelete = new Button();
                buttonDelete.Content = "Delete";
                buttonDelete.Padding = new Thickness(10, 5, 10, 5);
                buttonDelete.Margin = new Thickness(5);
                buttonDelete.Cursor = Cursors.Hand;
                buttonDelete.Click += DeleteButton_Click;
                buttonDelete.Tag = locationService.locations[i];
                buttonDelete.Background = new SolidColorBrush(Colors.Red);
                buttonsPanel.Children.Add(buttonDelete);

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <author> Rasmus </author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //opret ny lokation 
        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddLocation); // navigere til siden hvor man kan tilføje en lokation 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author> Rasmus </author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //Slet knap for lokation 
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Location location = (Location)button.Tag;

            DialogBox dialogBox = new DialogBox($"Are you sure you want to delete '{location.name}'?"); //Dialogbox popup
            dialogBox.ShowDialog();
            if(dialogBox.DialogResult == true) // lokation fjernes 
            {
                locationService.locations.Remove(location);
                DrawLocations(); // genopbygger listen efter sletning
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Rasmus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // Rediger lokation 
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Location location = (Location)button.Tag; // gemmer hvilken lokation der skal redigeres

            locationService.targetLocation = location;

            router.Navigate(NavigationRouter.Route.EditLocation); // navigere til redigeringssiden for lokation 
        }
    }
}
