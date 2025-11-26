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

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    /// <Author
    public partial class LoginPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        Grid menuGrid;
        TextBlock userText;
        Button userProfileButton;


        public LoginPage(NavigationRouter router, Grid menuGrid, UserService userService, TextBlock userText, Button userProfileButton)
        {
            InitializeComponent();
            this.menuGrid = menuGrid;
            this.router = router;
            this.userService = userService;
            this.userText = userText;
            this.userProfileButton = userProfileButton;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User? possibleUser = userService.users.FirstOrDefault(u => u.email == UsernameBox.Text);
            if (possibleUser != null) 
            { 
                bool isCorrect = userService.isUserPasswordCorrect(possibleUser, PasswordBox.Password);
                if (isCorrect) { 
                    userService.authenticatedUser = possibleUser;
                    userText.Text = possibleUser.firstName;
                    userProfileButton.Tag = possibleUser;
                    router.Navigate(NavigationRouter.Route.Home);
                    menuGrid.Visibility = Visibility.Visible;
                }
            }
            PasswordBox.Clear();
        }
    }
}
