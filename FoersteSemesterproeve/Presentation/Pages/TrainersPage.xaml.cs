using FoersteSemesterproeve.Domain.Models;
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
    /// Interaction logic for TrainersPage.xaml
    /// </summary>
    public partial class TrainersPage : UserControl
    {
        NavigationRouter router;
        UserService userService;
        public TrainersPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();

            this.router = router;
            this.userService = userService;

            for(int i = 0; i < userService.users.Count; i++)
            {
                if (userService.users[i].isCoach == true)
                {
                    Button coachName = new Button();
                    coachName.Content = $"{userService.users[i].firstName} {userService.users[i].lastName}";
                    coachName.FontSize = 15;
                    coachName.Focusable = false;
                    coachName.Width = 150;
                    coachName.Margin = new Thickness(0, 10, 0, 10);
                    coachName.Tag = userService.users[i];
                    coachName.Click += ViewCoachInfoButton_Click;

                    TrainersPageStackPanel.Children.Add(coachName);
                }
            }
        }

        private void ViewCoachInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            User user = (User)button.Tag;

            userService.targetUser = user;

            router.Navigate(NavigationRouter.Route.Trainer);
        }
    }
}
