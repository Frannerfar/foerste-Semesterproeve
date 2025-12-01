using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LocationsPage.xaml
    /// </summary>
    public partial class LocationsPage : UserControl
    {
        public LocationsPage()
        {
            InitializeComponent();
        }
        private void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            //if (userService.authenticatedUser.isAdmin)
            {
                //Dialogbox "Only admins can create activities"
                return;
            }

           // if (string.IsNullOrEmpty())
            {
                //Dialogbox "Title cannot be empty"
                return;
            }
        }
    }
}
