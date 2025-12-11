using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
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

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    /// <author>Martin</author>
    /// <created>26-11-2025</created>
    /// <updated>27-11-2025</updated>
    public partial class LoginPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        Grid menuGrid;
        Button userProfileButton;
        List<Button> adminButtons;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>28-11-2025</updated>
        /// <param name="router"></param>
        /// <param name="menuGrid"></param>
        /// <param name="userService"></param>
        /// <param name="userProfileButton"></param>
        /// <param name="adminButtons"></param>
        public LoginPage(NavigationRouter router, Grid menuGrid, UserService userService, Button userProfileButton, List<Button> adminButtons)
        {
            InitializeComponent();
            this.menuGrid = menuGrid;
            this.router = router;
            this.userService = userService;
            this.userProfileButton = userProfileButton;
            this.adminButtons = adminButtons;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? possibleUser = null;

            for (int i = 0; i < userService.users.Count; i++)
            {
                if(userService.users[i].email == EmailBox.Text && userService.users[i].password == PasswordBox.Password)
                {
                    possibleUser = userService.users[i];
                }
            }

            if (possibleUser != null) 
            { 
                bool isCorrect = userService.isUserPasswordCorrect(possibleUser, PasswordBox.Password);
                if (isCorrect) { 
                    userService.authenticatedUser = possibleUser;
                    userProfileButton.Tag = possibleUser;
                    if(!possibleUser.isAdmin)
                    {
                        foreach(Button button in adminButtons)
                        {
                            button.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        foreach (Button button in adminButtons)
                        {
                            button.Visibility = Visibility.Visible;
                        }
                    }

                        router.Navigate(NavigationRouter.Route.Home);
                    menuGrid.Visibility = Visibility.Visible;

                }
            }
            PasswordBox.Clear();
        }
    }
}
