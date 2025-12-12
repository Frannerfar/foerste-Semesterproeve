using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for TrainersPage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class TrainersPage : UserControl
    {
        //fields
        NavigationRouter router;
        UserService userService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public TrainersPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();

            // input i constructoren håndteres her
            this.router = router;
            this.userService = userService;

            //looper users listen ud
            for(int i = 0; i < userService.users.Count; i++)
            {
                //hvis iterationens bool isCoach er det samme som true, så køres if statementet
                if (userService.users[i].isCoach == true)
                {
                    // for hvert iteration der når herind bliver der instantieret en ny knap
                    Button coachName = new Button();
                    // her tilføjes tekst, skriftstørrelse, størrelse og så videre til knappen
                    coachName.Content = $"{userService.users[i].firstName} {userService.users[i].lastName}";
                    coachName.FontSize = 15;
                    coachName.Focusable = false;
                    coachName.Width = 150;
                    coachName.Margin = new Thickness(0, 10, 0, 10);
                    coachName.Tag = userService.users[i];
                    coachName.Click += ViewCoachInfoButton_Click;
                    // tilføjer hver knap til et allerede eksisterende stackpanel
                    TrainersPageStackPanel.Children.Add(coachName);
                }
            }
        }

        /// <summary>
        /// funktion tilhørende en knap der viser nye side med træners info
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCoachInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // sender er klikket med musen som vi sætter i button
            // vi ved det kun kan være en knap derfor typecaster vi sender til typen Button
            Button button = (Button)sender;
            // buttong.Tag bliver sat i user
            // vi ved at button.Tag har en user med sig derfor typecaster vi button.Tag til typen Button
            User user = (User)button.Tag;

            // sætter user i targetUser
            userService.targetUser = user;

            // router til den nye side
            router.Navigate(NavigationRouter.Route.Trainer);
        }
    }
}
