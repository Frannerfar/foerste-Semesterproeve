using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;

        public AddUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;



        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (userService.targetUser != null)
            {
                DateOnly date;
                if(DatePicker.SelectedDate != null)
                {
                    date = DateOnly.FromDateTime(DatePicker.DisplayDate);
                }
                else
                {
                    date = new DateOnly(2000, 1, 1);
                }

                    userService.AddUser(
                    FirstNameBox.Text,
                    LastNameBox.Text,
                    EmailBox.Text,
                    CityBox.Text,
                    AddressBox.Text,
                    date,
                    int.Parse(PostalBox.Text),
                    (bool)AdminCheckbox.IsChecked,
                    (bool)TrainerCheckbox.IsChecked,
                    (bool)HasPaidCheckbox.IsChecked);

                router.Navigate(NavigationRouter.Route.Members);
            }
        }
    }
}
