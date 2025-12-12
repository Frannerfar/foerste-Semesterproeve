using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Views;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class UsersPage : UserControl
    {
        UserService userService;
        NavigationRouter router;

        /// <summary>
        ///     Constructor til UsersPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public UsersPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();

            // Parametrene i constructoren sættes som fields
            this.userService = userService;
            this.router = router;

            // funktionen "PopulateDataGrid" kaldes og tilføjer alle users til MemberDataGrid (DataGrid) i XAML
            PopulateDataGrid();
        }

        /// <summary>
        ///     Bruges til at tilføje alle brugere i systemet til regnearks-lignende-controller "DataGrid".
        /// </summary>
        /// <author>Martin</author>
        /// <updated></updated>
        public void PopulateDataGrid()
        {
            // Clear regnearket først
            MemberDataGrid.Items.Clear();
            // For hver bruger der eksisterer i systemet
            foreach (User user in userService.users)
            {
                // Tilføj brugeren til DataGridet "MemberDataGrid", som automatisk binder dataen fra User objekternes properties (ikke fields, men properties)
                MemberDataGrid.Items.Add(user);
            }
        }


        /// <summary>
        ///     Bruges til at navigere til "AddUserPage" siden
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddUser);
        }

        /// <summary>
        ///     Bruges til at sætte targetUser og navigere til "EditUserPage"
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            // Da vi ved at sender er en button, så typecaster vi sender til typen Button.
            Button button = (Button)sender;
            // Og da vi ved at vi har sat en reference til et User objekt på knappens Tag,
            // så kan vi typecast button.Tag til at være en User og gemmes i variablen "user".
            User user = (User)button.Tag;
            // Hvis user ikke er null
            if (user != null)
            {
                // Sæt targetUser i userService til at være brugeren tilknyttet til knappen
                userService.targetUser = user;
                // Naviger til siden "EditUserPage".
                router.Navigate(NavigationRouter.Route.EditUser);
            }
        }

        /// <summary>
        ///     Bruges til at slette en bruger
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            // Da vi ved at sender er en button, så typecaster vi sender til typen Button.
            Button button = (Button)sender;
            // Og da vi ved at vi har sat en reference til et User objekt på knappens Tag,
            // så kan vi typecast button.Tag til at være en User og gemmes i variablen "user".
            User user = (User)button.Tag;
            // Hvis user ikke er null
            if (user != null)
            {
                // og hvis brugeren er en admin
                if(user.isAdmin) 
                {
                    MessageBox.Show("You can't delete an admin");
                    return;
                }
                // hvis brugeren ikke er en admin, så instantieres DialogBox der spørger om du er sikker på, at du vil slette den bruger.
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete '{user.firstName} {user.lastName}'?");
                dialogBox.ShowDialog();
                // Hvis der blev klikket på "JA" i vinduet
                if(dialogBox.DialogResult == true)
                {
                    // så slet brugeren
                    userService.DeleteUserByObject(user);
                    // og genskab DataGridet af brugere.
                    PopulateDataGrid();
                }
            }
        }
    }
}
