using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Models;
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
using System.Windows.Shapes;

namespace FoersteSemesterproeve.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AddUserToActivity.xaml
    /// </summary>
    public partial class AddUserToActivity : Window
    {

        ActivityService activityService;
        UserService userService;
        Activity activity;

        List<User> availableUsers;

        public AddUserToActivity(ActivityService activityService, UserService userService, Activity activity)
        {
            InitializeComponent();

            this.activityService = activityService;
            this.userService = userService;
            this.activity = activity;
            this.availableUsers = new List<User>();

            
            ActivityName.Text = activity.title;


            for(int i = 0; i < userService.users.Count; i++)
            {
                if(!activity.participants.Contains(userService.users[i]))
                {
                    // Der tilføjes til en oprettet liste, hvis personen er available til at blive tilføjet.
                    availableUsers.Add(userService.users[i]);
                    UserListBox.Items.Add($"{userService.users[i].firstName} {userService.users[i].lastName}");
                }
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Det her kunne have været gjort nemmere, ved at lave ItemTemplate og binding.
            // Dermed kunne vi også bruge UserComboBox.SelectedItem, da det ville have givet os User objektet
            // Men fordi vi ikke bevæger os ud i ItemTemplate, binding og MVVM stil, så gør vi det på denne måde
            // Selvom det ikke er at foretrække, pga. usikkerheden i om indekset matcher vores users.
            User user;
            if (UserListBox.SelectedItem != null)
            {
                user = availableUsers[UserListBox.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Please select a valid user");
                return;
            }
            activityService.JoinActivity(activity, user);
            this.Close();
        }
    }
}
