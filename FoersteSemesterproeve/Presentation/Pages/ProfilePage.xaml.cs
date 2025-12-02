using FoersteSemesterproeve.Domain.Models;
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
using static FoersteSemesterproeve.Domain.Models.User;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        MembershipService membershipService;
        public ProfilePage(NavigationRouter router, UserService userService, MembershipService membershipService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;
            this.membershipService = membershipService;

            if(userService.authenticatedUser != null)
            {
                FirstNameBox.Text = userService.authenticatedUser.firstName;
                LastNameBox.Text = userService.authenticatedUser.lastName;
                EmailBox.Text = userService.authenticatedUser.email;
                CityBox.Text = userService.authenticatedUser.city;
                AddressBox.Text = userService.authenticatedUser.address;
                PostalBox.Text = userService.authenticatedUser.postal.ToString();
                DatePicker.Text = userService.authenticatedUser.dateofBirth.ToString();

                User.Gender[] genders = Enum.GetValues<User.Gender>();

                for (int i = 0; i < genders.Length; i++)
                {
                    GenderComboBox.Items.Add(genders[i].ToString());
                    if (genders[i] == userService.authenticatedUser.gender)
                    {
                        GenderComboBox.SelectedIndex = i;
                    }
                }

                for(int i = 0; i < membershipService.membershipTypes.Count; i++)
                {
                    MembershipComboBox.Items.Add(membershipService.membershipTypes[i].name.ToString());
                    if (membershipService.membershipTypes[i] == userService.authenticatedUser.membershipType)
                    {
                        MembershipComboBox.SelectedIndex = i;
                    }
                }

            }
        }
        private void ActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Activities);
        }



    }
}
