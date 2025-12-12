using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class LoginPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        Grid menuGrid;
        Button userProfileButton;
        List<Button> adminButtons;

        /// <summary>
        ///     Constructor til LoginPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="menuGrid"></param>
        /// <param name="userService"></param>
        /// <param name="userProfileButton"></param>
        /// <param name="adminButtons"></param>
        public LoginPage(NavigationRouter router, Grid menuGrid, UserService userService, Button userProfileButton, List<Button> adminButtons)
        {
            InitializeComponent();

            // parametre fra constructoren sættes i fields i objektet
            this.menuGrid = menuGrid;
            this.router = router;
            this.userService = userService;
            this.userProfileButton = userProfileButton;
            this.adminButtons = adminButtons;
        }

        /// <summary>
        ///     Bruges til at prøve at logge ind, baseret på email og password indtastet
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // For hver bruger i systemet
            for (int i = 0; i < userService.users.Count; i++)
            {
                // Hvis input i Email.Text er det samme som bruger i listens email OG input i PasswordBox er det samme som samme brugers password
                // Kort sagt: hvis emailen og password begger matcher en bruger i systemet
                if(userService.users[i].email == EmailBox.Text && userService.users[i].password == PasswordBox.Password)
                {
                    // authenticatedUser sættes til at være brugeren der matchede både email og password inputs
                    userService.authenticatedUser = userService.users[i];
                    // reference til personen der er logget ind, sættes på knappen til navigering til profil
                    userProfileButton.Tag = userService.users[i];
                    // Hvis personen IKKE er admin, så sæt alle admin buttons til at være collapsed (og dermed ikke fylde noget)
                    if (!userService.users[i].isAdmin)
                    {
                        foreach(Button button in adminButtons)
                        {
                            button.Visibility = Visibility.Collapsed;
                        }
                    }
                    // ellers så sæt admin buttons til at være synlige / visible
                    else
                    {
                        foreach (Button button in adminButtons)
                        {
                            button.Visibility = Visibility.Visible;
                        }
                    }
                    // Naviger til HomePage
                    router.Navigate(NavigationRouter.Route.Home);
                    // Gør hovedmenuen synlig
                    menuGrid.Visibility = Visibility.Visible;
                }
            }
            // Hvis ikke password er korrekt til den indtastede email adresse, så clear indholdet i password input boksen
            PasswordBox.Clear();
        }
    }
}
