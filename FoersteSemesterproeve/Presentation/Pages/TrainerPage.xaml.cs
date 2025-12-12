using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ViewTrainerInfoPage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class TrainerPage : UserControl
    {
        //fields
        NavigationRouter router;
        UserService userService;
        ActivityService activityService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        /// <param name="activityService"></param>
        public TrainerPage(NavigationRouter router, UserService userService, ActivityService activityService)
        {
            InitializeComponent();
            // her håndteres constructorens input
            this.router = router;
            this.userService = userService;
            this.activityService = activityService;

            // if statement kører hvis targetUser ikke er null
            if(userService.targetUser != null)
            {
                // indsætter targetUsers fornavn og efternavn i en textblock
                TrainerFullNameBlock.Text = $"{userService.targetUser.firstName} {userService.targetUser.lastName}";
                
                // looper listen activities ud
                for(int i = 0; i < activityService.activities.Count; i++)
                {
                    // if statement der sammenligner iterationens værdi coach med targetuser, hvis det er det samme så køres statementet
                    if (activityService.activities[i].coach == userService.targetUser)
                    {
                        //instantierer nyt stackpanel
                        StackPanel activityStackPanel = new StackPanel();
                        activityStackPanel.Margin = new Thickness(5, 10, 5, 10);
                        // tilføjer det nye stackpanel et allerede eksisterende stackpanel
                        ActivitiesStackPanel.Children.Add(activityStackPanel);

                        // instantierer ny button
                        Button activityButton = new Button();
                        // tilføjer knappens tekst, størrelse, farve og tag
                        activityButton.Content = activityService.activities[i].title;
                        activityButton.Padding = new Thickness(15, 10, 15, 10);
                        activityButton.Background = new SolidColorBrush(Colors.Yellow);
                        activityButton.Tag = activityService.activities[i];
                        // sætter hvilken funktion der køres når der bliver klikket
                        activityButton.Click += ActivityButton_Click;
                        // tilføjer knappen til det stackpanel der blev instanieret før
                        activityStackPanel.Children.Add(activityButton);
                    }
                }
                
            }

        }

        /// <summary>
        /// Funktion tilhørende activity knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityButton_Click(object sender, RoutedEventArgs e)
        {
            // gemmer sender i button
            // sender er selve klikket med musen så vi ved at det er en knap, derfor typecaster vi til typen Button
            Button button = (Button)sender;
            // gemmer button.Tag i activity
            // button.Tag har activity listen på sig, derfor typecaster vi til typen Activity
            Activity activity = (Activity)button.Tag;

            // if statement kører hvis activity ikke er null
            if(activity != null )
            {
                // sætter activity over i targetActivity
                activityService.targetActivity = activity;
                // router hen til siden Activity
                router.Navigate(NavigationRouter.Route.Activity);
            }
        }

        /// <summary>
        /// Funktion tilhørende back knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToTrainersPageButton_Click(object sender, RoutedEventArgs e)
        {
            // funktion der router hen til næste side
            router.Navigate(NavigationRouter.Route.Trainers);
        }
    }
}
