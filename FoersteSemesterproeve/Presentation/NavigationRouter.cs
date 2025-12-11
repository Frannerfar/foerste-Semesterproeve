using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Presentation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static FoersteSemesterproeve.Presentation.NavigationRouter;

namespace FoersteSemesterproeve.Presentation
{
    /// <summary>
    ///     Håndterer navigation mellem forskellige pages / usercontrolsi samme Window
    /// </summary>
    /// <author>Martin</author>
    /// <created>2025-11-25</created>
    /// <updated></updated>
    /// <remarks>Bruges af MainWindow til UI routing</remarks>
    public class NavigationRouter
    {
        Route currentRoute;
        ContentControl MainContent;
        Grid menuGrid;
        Dictionary<Route, Func<UserControl>> routes;

        private UserService userService;
        private ActivityService activityService;
        private LocationService locationService;
        private MembershipService membershipService;
        private Button userProfileButton;
        private List<Button> adminButtons;
        private Button HomeButton;
        private Button ActivitiesButton;
        private Button TrainersButton;
        private Button LocationsButton;
        private Button MembersButton;
        private Button MembershipsButton;

        SolidColorBrush menuStaticItem;
        SolidColorBrush menuActiveItem;

        Color menuStaticItemColor = Color.FromRgb(119, 136, 153);
        Color menuActiveItemColor = Color.FromRgb(177, 211, 245);

        Button currentActiveMenuButton;



        /// <summary>
        ///     Constructor der modtager referencer til 
        ///     ContentControl (Element vi sætter UserControls under) og Grid (Menu).
        /// </summary>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        /// 
        public NavigationRouter(ContentControl contentControl, Grid menuGrid, Button userProfileButton, List<Button> adminButtons, UserService userService, ActivityService activityService, LocationService locationService, MembershipService membershipService, Button HomeButton, Button ActivitiesButton, Button TrainersButton, Button LocationsButton, Button MembersButton, Button MembershipsButton)
        {
            this.MainContent = contentControl;
            this.menuGrid = menuGrid;

            this.userService = userService;
            this.activityService = activityService;
            this.locationService = locationService;
            this.membershipService = membershipService;
            this.userProfileButton = userProfileButton;
            this.adminButtons = adminButtons;

            this.HomeButton = HomeButton;
            this.ActivitiesButton = ActivitiesButton;
            this.TrainersButton = TrainersButton;
            this.LocationsButton = LocationsButton;
            this.MembersButton = MembersButton;
            this.MembershipsButton = MembershipsButton;

            menuStaticItem = new SolidColorBrush(menuStaticItemColor);
            menuActiveItem = new SolidColorBrush(menuActiveItemColor);

            currentActiveMenuButton = HomeButton;
            SetMenuButtonActive(currentActiveMenuButton, HomeButton);
        }


        public void Navigate(Route route)
        {
            switch (route)
            {
                case Route.Login:
                    MainContent.Content = new LoginPage(this, menuGrid, userService, userProfileButton, adminButtons);
                    break;
                case Route.Home:
                    MainContent.Content = new HomePage(this, userService, activityService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.HomeButton);
                    break;
                case Route.Activities:
                    MainContent.Content = new ActivitiesPage(this, activityService, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.ActivitiesButton);
                    break;
                case Route.Users:
                    MainContent.Content = new UsersPage(this, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembersButton);
                    break;
                case Route.Trainers:
                    MainContent.Content = new TrainersPage(this, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.TrainersButton);
                    break;
                case Route.Locations:
                    MainContent.Content = new LocationsPage(this, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.LocationsButton);
                    break;
                case Route.AddLocation:
                    MainContent.Content = new AddLocationPage(this, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.LocationsButton);
                    break;
                case Route.EditLocation:
                    MainContent.Content = new EditLocationPage(this, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.LocationsButton);
                    break;
                case Route.MembershipTypes:
                    MainContent.Content = new MembershipTypesPage(this, membershipService, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembershipsButton);
                    break;
                case Route.AddMembershipType:
                    MainContent.Content = new AddMembershipTypePage(this, membershipService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembershipsButton);
                    break;
                case Route.EditMembershipType:
                    MainContent.Content = new EditMembershipTypePage(this, membershipService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembershipsButton);
                    break;
                case Route.Profile:
                    MainContent.Content = new ProfilePage(this, userService, membershipService);
                    ResetButtonActive(this.currentActiveMenuButton);
                    break;
                case Route.AddUser:
                    MainContent.Content = new AddUserPage(this, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembersButton);
                    break;
                case Route.EditUser:
                    MainContent.Content = new EditUserPage(this, userService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembersButton);
                    break;
                case Route.UserActivities:
                    MainContent.Content = new UserActivitiesPage(this, userService, activityService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.MembersButton);
                    break;
                case Route.AddActivity:
                    MainContent.Content = new AddActivityPage(this, activityService, userService, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.ActivitiesButton);
                    break;
                case Route.EditActivity:
                    MainContent.Content = new EditActivityPage(this, activityService, userService, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.ActivitiesButton);
                    break;
                case Route.Activity:
                    MainContent.Content = new ActivityPage(this, activityService, userService, locationService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.ActivitiesButton);
                    break;
                case Route.Trainer:
                    MainContent.Content = new TrainerPage(this, userService, activityService);
                    SetMenuButtonActive(this.currentActiveMenuButton, this.TrainersButton);
                    break;
            }
        }

        /// <summary>
        /// Sørger for at visuelt sætte indikation på, hvilken route man befinder sig på.
        /// Dette gøres ved at sætte den nuværende aktive button tilbage til vores standard menu farve. 
        /// Derefter sættes den nye Button der er blevet klikket på, til få den nye aktive farve, samt til at være den nye aktive Button.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="originalButton"></param>
        /// <param name="newButton"></param>
        private void SetMenuButtonActive(Button originalButton, Button newButton)
        {
            if (originalButton != null)
            {
                originalButton.Background = menuStaticItem;
            }

            newButton.Background = menuActiveItem;
            currentActiveMenuButton = newButton;
        }

        private void ResetButtonActive(Button originalButton)
        {
            originalButton.Background = menuStaticItem;
        }





        /// <summary>
        ///     Enum til navngivning af unikke værdier.
        ///     Bruges til routing.
        /// </summary>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        public enum Route
        {
            Login,
            Home,
            Activities,
            Users,
            Trainers,
            Locations,
            AddLocation,
            EditLocation,
            MembershipTypes,
            AddMembershipType,
            EditMembershipType,
            Profile,
            AddUser,
            EditUser,
            UserActivities,
            AddActivity,
            EditActivity,
            Activity,
            Trainer
        }

    }
}
