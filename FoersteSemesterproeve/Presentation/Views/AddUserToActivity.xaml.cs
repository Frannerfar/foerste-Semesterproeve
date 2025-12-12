using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Models;
using System;
using System.Windows;


namespace FoersteSemesterproeve.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddUserToActivity.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class AddUserToActivity : Window
    {

        ActivityService activityService;
        UserService userService;
        Activity activity;

        List<User> availableUsers;

        /// <summary>
        ///     Constructor til AddUserToActivity
        /// </summary>
        /// <author>Martin</author>
        /// <param name="activityService"></param>
        /// <param name="userService"></param>
        /// <param name="activity"></param>
        public AddUserToActivity(ActivityService activityService, UserService userService, Activity activity)
        {
            InitializeComponent();

            // parametre sættes i fields
            this.activityService = activityService;
            this.userService = userService;
            this.activity = activity;

            // ny liste af brugere instantieres og sættes i availableUsers i fields
            this.availableUsers = new List<User>();

            // Aktivitetens navn sættes i tekstblok i vinduet
            ActivityName.Text = activity.title;

            // For hver bruger i systemet
            for(int i = 0; i < userService.users.Count; i++)
            {
                // Hvis aktiviteten ikke har brugeren som deltager
                if(!activity.participants.Contains(userService.users[i]))
                {   
                    // Så tilføjes brugeren til den oprettede liste over tilgængelige personer
                    availableUsers.Add(userService.users[i]);
                    // Brugerens fornavn og efternavn tilføjes til ListBoxen i XAML.
                    UserListBox.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                }
            }
        }

        /// <summary>
        ///     Bruges til at tilføje den valgte bruger til parameteren activity der med i vinduets constructor
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Det her kunne have været gjort nemmere, ved at lave ItemTemplate og binding.
            // Dermed kunne vi også bruge UserComboBox.SelectedItem, da det ville have givet os User objektet
            // Men fordi vi ikke bevæger os ud i ItemTemplate, binding og MVVM stil, så gør vi det på denne måde
            // Selvom det ikke er at foretrække, pga. usikkerheden i om indekset matcher vores users.
            
            // variabel sættes
            User user;
            // Hvis ListBoxen har et valgt item
            if (UserListBox.SelectedItem != null)
            {
                // brugeren fra den tilgængelige liste, sættes til variablen "user"
                user = availableUsers[UserListBox.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Please select a valid user");
                return;
            }
            // Her kommer vi kun hen, hvis der blev valgt en bruger
            // brugeren tilføjes til aktiviteten ved hjælp af activityService
            activityService.JoinActivity(activity, user);
            // vinduet lukkes
            this.Close();
        }
    }
}
