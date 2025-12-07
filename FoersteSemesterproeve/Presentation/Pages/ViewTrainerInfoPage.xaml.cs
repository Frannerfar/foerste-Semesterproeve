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
        ActivityService activityService;
        public ViewTrainerInfoPage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;

            if(userService.targetUser != null)
            {
                TrainerFullNameBlock.Text = $"{userService.targetUser.firstName} {userService.targetUser.lastName}";
                
                for(int i = 0; i < activityService.activities.Count; i++)
                {
                    if (activityService.activities[i].coach == userService.targetUser)
                    {
                        TrainerActivitiesList.Items.Add(activityService.activities[i].title);
                    }
                }
                
            }

        }

        private void BackToTrainersPageButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Trainers);
        }
    }
}
