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
using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Views;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    /// <author>Martin</author>
    /// <created>26-11-2025</created>
    /// <updated>27-11-2025</updated>
    public partial class MembersPage : UserControl
    {
        UserService userService;
        NavigationRouter router;

        public MembersPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.userService = userService;
            this.router = router;

            populateDataGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated></updated>
        public void populateDataGrid()
        {
            MemberDataGrid.Items.Clear();
            foreach (User user in userService.users)
            {
                MemberDataGrid.Items.Add(user);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddUser);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;
            if (user != null)
            {
                userService.targetUser = user;
                router.Navigate(NavigationRouter.Route.EditUser);
                //MessageBox.Show($"EDIT USER: {user.firstName} {user.lastName}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;
            if (user != null)
            {
                if(user.isAdmin) 
                {
                    MessageBox.Show("You can't delete an admin");
                    return;
                }
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete '{user.firstName} {user.lastName}'?");
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true)
                {
                    userService.DeleteUserByObject(user);
                    populateDataGrid();
                }
            }
        }
    }
}
