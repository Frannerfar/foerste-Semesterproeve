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

        /// <summary>
        ///     Constructor der modtager referencer til 
        ///     ContentControl (Element vi sætter UserControls under) og Grid (Menu).
        /// </summary>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        public NavigationRouter(ContentControl contentControl, Grid menuGrid, Button userProfileButton, List<Button> adminButtons, UserService userService, ActivityService activityService, LocationService locationService, MembershipService membershipService)
        {
            this.MainContent = contentControl;
            this.menuGrid = menuGrid;

            this.userService = userService;
            this.activityService = activityService;
            this.locationService = locationService;
            this.membershipService = membershipService;
            this.userProfileButton = userProfileButton;
            this.adminButtons = adminButtons;

            //routes = new Dictionary<Route, Func<UserControl>>
            //{
            //    { Route.Login, () => new LoginPage(this, menuGrid, userService, userProfileButton, adminButtons) },
            //    { Route.Home, () => new HomePage(this, userService, activityService) },
            //    { Route.Activities, () => new ActivitiesPage(this, activityService, userService) },
            //    { Route.Members, () => new MembersPage(this, userService) },
            //    { Route.Trainers, () => new TrainersPage(this, userService) },
            //    { Route.Locations, () => new LocationsPage(this, locationService) },
            //    { Route.AddLocation, () => new AddLocationPage(this, locationService) },
            //    { Route.EditLocation, () => new EditLocationPage(this, locationService) },
            //    { Route.Memberships, () => new MembershipsPage(this, membershipService, userService) },
            //    { Route.AddMembershipType, () => new AddMembershipTypePage(this, membershipService) },
            //    { Route.EditMembershipType, () => new EditMembershipTypePage(this, membershipService) },
            //    { Route.Profile, () => new ProfilePage(this, userService, membershipService) },
            //    { Route.AddUser, () => new AddUserPage(this, userService) },
            //    { Route.EditUser, () => new EditUserPage(this, userService) },
            //    { Route.UserActivities, () => new UserActivitiesPage(this, userService) }, 
            //    { Route.AddActivities, () => new AddActivitiesPage(this, activityService, userService, locationService) },
            //    { Route.ViewTrainerInfo, () => new ViewTrainerInfoPage(this, userService) }
            //};
        }

        /// <summary>
        ///     Constructor der modtager referencer til 
        ///     ContentControl (Element vi sætter UserControls under) og Grid (Menu).
        /// </summary>
        /// <param name="route">Using the enum Route to decide routing direction</param>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        //public void NavigateOld(Route route)
        //{
        //    if (!routes.TryGetValue(route, out Func<UserControl>? view))
        //    {
        //        MessageBox.Show($"No view registered for route {route}.", nameof(route));
        //        this.Navigate(Route.Home);
        //        //throw new ArgumentException($"No view registered for route {route}.", nameof(route));
        //    }
        //    else
        //    {
        //        MainContent.Content = view();
        //        currentRoute = route;
        //    }
        //}



        public void Navigate(Route route)
        {
            switch (route)
            {
                case Route.Login:
                    MainContent.Content = new LoginPage(this, menuGrid, userService, userProfileButton, adminButtons);
                    break;
                case Route.Home:
                    MainContent.Content = new HomePage(this, userService, activityService);
                    break;
                case Route.Activities:
                    MainContent.Content = new ActivitiesPage(this, activityService, userService);
                    break;
                case Route.Members:
                    MainContent.Content = new MembersPage(this, userService);
                    break;
                case Route.Trainers:
                    MainContent.Content = new TrainersPage(this, userService);
                    break;
                case Route.Locations:
                    MainContent.Content = new LocationsPage(this, locationService);
                    break;
                case Route.AddLocation:
                    MainContent.Content = new AddLocationPage(this, locationService);
                    break;
                case Route.EditLocation:
                    MainContent.Content = new EditLocationPage(this, locationService);
                    break;
                case Route.Memberships:
                    MainContent.Content = new MembershipsPage(this, membershipService, userService);
                    break;
                case Route.AddMembershipType:
                    MainContent.Content = new AddMembershipTypePage(this, membershipService);
                    break;
                case Route.EditMembershipType:
                    MainContent.Content = new EditMembershipTypePage(this, membershipService);
                    break;
                case Route.Profile:
                    MainContent.Content = new ProfilePage(this, userService, membershipService);
                    break;
                case Route.AddUser:
                    MainContent.Content = new AddUserPage(this, userService);
                    break;
                case Route.EditUser:
                    MainContent.Content = new EditUserPage(this, userService);
                    break;
                case Route.UserActivities:
                    MainContent.Content = new UserActivitiesPage(this, userService);
                    break;
                case Route.AddActivities:
                    MainContent.Content = new AddActivitiesPage(this, activityService, userService, locationService);
                    break;
                case Route.ViewTrainerInfo:
                    MainContent.Content = new ViewTrainerInfoPage(this, userService, activityService);
                    break;
            }
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
            Members,
            Trainers,
            Locations,
            AddLocation,
            EditLocation,
            Memberships,
            AddMembershipType,
            EditMembershipType,
            Profile,
            AddUser,
            EditUser,
            UserActivities,
            AddActivities,
            ViewTrainerInfo
        }

    }
}
