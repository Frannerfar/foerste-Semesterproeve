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
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;

        public EditUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;

            if (userService.targetUser != null)
            {
                FirstNameBox.Text = userService.targetUser?.firstName;
                LastNameBox.Text = userService.targetUser?.lastName;
                EmailBox.Text = userService.targetUser?.email;
                CityBox.Text = userService.targetUser?.city;
                AddressBox.Text = userService.targetUser?.address;
                PostalBox.Text = userService.targetUser?.postal.ToString();
            
                if(userService.targetUser.isAdmin) { AdminCheckbox.IsChecked = true; }

                if(userService.targetUser.isCoach) { TrainerCheckbox.IsChecked = true; }

                if(userService.targetUser.hasPaid) { HasPaidCheckbox.IsChecked = true; }
            }
            

            
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (userService.targetUser != null) { 
                userService.targetUser.firstName = FirstNameBox.Text;
                userService.targetUser.lastName = LastNameBox.Text;
                userService.targetUser.email = EmailBox.Text;
                userService.targetUser.city = CityBox.Text;
                userService.targetUser.address = AddressBox.Text;
                userService.targetUser.postal = int.Parse(PostalBox.Text);
            
                if(AdminCheckbox.IsChecked != null)
                {
                    userService.targetUser.isAdmin = (bool)AdminCheckbox.IsChecked;
                }
                if(TrainerCheckbox.IsChecked != null)
                {
                    userService.targetUser.isCoach = (bool)TrainerCheckbox.IsChecked;
                }
                if(HasPaidCheckbox.IsChecked != null)
                {
                    userService.targetUser.hasPaid = (bool)HasPaidCheckbox.IsChecked;
                }

                userService.targetUser.CheckBothMarks();
                router.Navigate(NavigationRouter.Route.Members);
            }
        }
    }
}
