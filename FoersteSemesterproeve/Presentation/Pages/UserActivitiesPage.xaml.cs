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
    /// Interaction logic for UserActivitiesPage.xaml
    /// </summary>
    public partial class UserActivitiesPage : UserControl
    {
        NavigationRouter router;
        UserService userService;



        public UserActivitiesPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;

            UserText.Text = $"{userService.targetUser?.firstName} {userService.targetUser?.lastName}";

            if (userService.targetUser != null) {
                foreach (Activity activity in userService.targetUser?.activityList)
                {
                    ActivitiesDataGrid.Items.Add(activity);
                }
            }


        }

        private void LeaveActivity_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Waiting for implements of JOIN/LEAVE functionality");
        }

        private void GoActivity_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Waiting for implements of see activity page");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.EditUser);
        }
    }
}
