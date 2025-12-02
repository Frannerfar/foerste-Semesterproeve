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

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewTrainerInfoPage.xaml
    /// </summary>
    public partial class ViewTrainerInfoPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        public ViewTrainerInfoPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;

            if(userService.targetUser != null)
            {
                TrainerFullNameBlock.Text = $"{userService.targetUser.firstName} {userService.targetUser.lastName}";
                EmailBlock.Text = userService.targetUser.email;
                TrainerActivitiesList.Items.Add(userService.targetUser.activityList);
            }

        }

        private void BackToTrainersPageButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Trainers);
        }
    }
}
