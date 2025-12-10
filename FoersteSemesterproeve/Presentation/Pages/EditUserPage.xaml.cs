using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
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
    /// <author>Martin</author>
    /// <created>26-11-2025</created>
    /// <updated></updated>
    public partial class EditUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public EditUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;


            if (userService.targetUser != null)
            {
                EditUserText.Text = $"'{userService.targetUser.firstName} {userService.targetUser.lastName}'";
                
                FirstNameBox.Text = userService.targetUser?.firstName;
                LastNameBox.Text = userService.targetUser?.lastName;
                EmailBox.Text = userService.targetUser?.email;
                CityBox.Text = userService.targetUser?.city;
                AddressBox.Text = userService.targetUser?.address;
                PostalBox.Text = userService.targetUser?.postal.ToString();

                DatePicker.SelectedDate = new DateTime(userService.targetUser.dateofBirth, new TimeOnly(0, 0));
                

                if(userService.targetUser.isAdmin) { AdminCheckbox.IsChecked = true; }

                if(userService.targetUser.isCoach) { TrainerCheckbox.IsChecked = true; }

                //if(userService.targetUser.hasPaid) { HasPaidCheckbox.IsChecked = true; }

                for(int i = 0; i < userService.membershipService.membershipTypes.Count; i++)
                {
                    MembershipComboBox.Items.Add(userService.membershipService.membershipTypes[i].name);
                    if(userService.membershipService.membershipTypes[i] == userService.targetUser?.membershipType)
                    {
                        MembershipComboBox.SelectedIndex = i;
                    }
                }

                //User.Gender[] genders = Enum.GetValues<User.Gender>();

                //for (int i = 0; i < genders.Length; i++)
                //{
                //    GenderComboBox.Items.Add(genders[i].ToString());
                //    if (userService?.targetUser?.gender != null && genders[i] == userService?.targetUser?.gender)
                //    {
                //        GenderComboBox.SelectedIndex = i;
                //    }
                //}
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                userService.targetUser.membershipType = userService.membershipService.membershipTypes[MembershipComboBox.SelectedIndex];

                userService.targetUser.CheckBothMarks();
                router.Navigate(NavigationRouter.Route.Members);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Members);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (userService.targetUser != null) 
            {
                if (userService.targetUser.isAdmin)
                {
                    MessageBox.Show("You can't delete an admin");
                    return;
                }
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete {userService.targetUser.firstName} {userService.targetUser.lastName}?");
                dialogBox.ShowDialog();
                if (dialogBox.DialogResult == true)
                {
                    userService.users.Remove(userService.targetUser);
                    router.Navigate(NavigationRouter.Route.Members);
                }
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.UserActivities);
        }
    }
}
