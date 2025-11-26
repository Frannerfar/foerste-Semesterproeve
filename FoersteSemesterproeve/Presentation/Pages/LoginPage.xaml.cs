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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        NavigationRouter router;
        Grid menuGrid;
        public LoginPage(NavigationRouter router, Grid menuGrid)
        {
            InitializeComponent();
            this.menuGrid = menuGrid;
            this.router = router;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Home);
            menuGrid.Visibility = Visibility.Visible;
        }
    }
}
