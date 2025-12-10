using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// <author>Marcus</author>
    public partial class ProfilePage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        MembershipService membershipService;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        /// <param name="membershipService"></param>
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

                for(int i = 0; i < membershipService.membershipTypes.Count; i++)
                {
                    MembershipComboBox.Items.Add(membershipService.membershipTypes[i].name);
                    if (membershipService.membershipTypes[i] == userService.authenticatedUser.membershipType)
                    {
                        MembershipComboBox.SelectedIndex = i;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            userService.targetUser = userService.authenticatedUser;
            router.Navigate(NavigationRouter.Route.UserActivities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            EmailBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            PostalBox.IsEnabled = true;
            DatePicker.IsEnabled = false;
            MembershipComboBox.IsEnabled = true;

            EditInfoButton.Visibility = Visibility.Collapsed;
            SaveEditInfoButton.Visibility = Visibility.Visible;
            CancelEditModeButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(userService.authenticatedUser != null)
            {
                if(FirstNameBox == null ||
                    LastNameBox == null ||
                    EmailBox == null ||
                    CityBox == null ||
                    AddressBox == null ||
                    PostalBox == null ||
                    DatePicker == null ||
                    MembershipComboBox == null)
                {
                    MessageBox.Show("Can't leave a box empty");
                    return;
                }   

                if(FirstNameBox != null &&
                    LastNameBox != null &&
                    EmailBox != null &&
                    CityBox != null &&
                    AddressBox != null &&
                    PostalBox != null &&
                    DatePicker != null &&
                    MembershipComboBox != null)
                {
                    bool isPostal = int.TryParse(PostalBox.Text, out int postalBoxInput);
                    if(isPostal == false)
                    {
                        MessageBox.Show("Postal Box only takes numbers");
                    }

                    DateTime pickedDateTime;
                    if (DatePicker.SelectedDate.HasValue)
                    {
                        pickedDateTime = DatePicker.SelectedDate.Value;
                        Debug.WriteLine("Datepicker set to Correct time");
                    }
                    else
                    {
                        MessageBox.Show("You have to choose a birthdate");
                        return;
                    }

                    DateOnly pickedDateOnly = DateOnly.FromDateTime(pickedDateTime);

                    userService.authenticatedUser.firstName = FirstNameBox.Text;
                    userService.authenticatedUser.lastName = LastNameBox.Text;
                    userService.authenticatedUser.email = EmailBox.Text;
                    userService.authenticatedUser.city = CityBox.Text;
                    userService.authenticatedUser.address = AddressBox.Text;
                    userService.authenticatedUser.postal = postalBoxInput;
                    userService.authenticatedUser.dateofBirth = pickedDateOnly;
                    userService.authenticatedUser.membershipType = membershipService.membershipTypes[MembershipComboBox.SelectedIndex];
                    
                    FirstNameBox.IsEnabled = false;
                    LastNameBox.IsEnabled = false;
                    EmailBox.IsEnabled = false;
                    CityBox.IsEnabled = false;
                    AddressBox.IsEnabled = false;
                    PostalBox.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    MembershipComboBox.IsEnabled = false;

                    EditInfoButton.Visibility = Visibility.Visible;
                    SaveEditInfoButton.Visibility = Visibility.Hidden;
                    CancelEditModeButton.Visibility = Visibility.Hidden;    
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEditModeButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            EmailBox.IsEnabled = false;
            CityBox.IsEnabled = false;
            AddressBox.IsEnabled = false;
            PostalBox.IsEnabled = false;
            DatePicker.IsEnabled = false;
            MembershipComboBox.IsEnabled = false;

            EditInfoButton.Visibility = Visibility.Visible;
            SaveEditInfoButton.Visibility = Visibility.Hidden;
            CancelEditModeButton.Visibility = Visibility.Hidden;

            FirstNameBox.Text = userService.authenticatedUser.firstName;
            LastNameBox.Text = userService.authenticatedUser.lastName;
            EmailBox.Text = userService.authenticatedUser.email;
            CityBox.Text = userService.authenticatedUser.city;
            AddressBox.Text = userService.authenticatedUser.address;
            PostalBox.Text = userService.authenticatedUser.postal.ToString();
            DatePicker.Text = userService.authenticatedUser.dateofBirth.ToString();

            for (int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                MembershipComboBox.Items.Add(membershipService.membershipTypes[i].name);
                if (membershipService.membershipTypes[i] == userService.authenticatedUser.membershipType)
                {
                    MembershipComboBox.SelectedIndex = i;
                }
            }
        }

    }
}
