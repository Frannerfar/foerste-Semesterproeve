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
    /// Håndterer navigation mellem forskellige pages / usercontrolsi samme Window
    /// </summary>
    /// <author>Martin</author>
    /// <created>2025-11-25</created>
    /// <remarks>Bruges af MainWindow til UI routing</remarks>
    public class NavigationRouter
    {
        Route currentRoute;
        ContentControl control;
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
        /// <updated
        public NavigationRouter(ContentControl contentControl, Grid menuGrid, Button userProfileButton, List<Button> adminButtons, UserService userService, ActivityService activityService, LocationService locationService, MembershipService membershipService)
        {
            this.control = contentControl;
            this.menuGrid = menuGrid;

            this.userService = userService;
            this.activityService = activityService;
            this.locationService = locationService;
            this.membershipService = membershipService;
            this.userProfileButton = userProfileButton;
            this.adminButtons = adminButtons;

            routes = new Dictionary<Route, Func<UserControl>>
            {
                { Route.Login, () => new LoginPage(this, menuGrid, userService, userProfileButton, adminButtons) },
                { Route.Home, () =>new HomePage(this, userService, activityService) },
                { Route.Activities, () => new ActivitiesPage(this, activityService, userService) },
                { Route.Members, () =>new MembersPage(this, userService) },
                { Route.Trainers, () =>new TrainersPage() },
                { Route.Locations, () => new LocationsPage() },
                { Route.Memberships, () => new MembershipsPage(this, membershipService) },
                { Route.Profile, () =>new ProfilePage() },
                { Route.AddUser, () => new AddUserPage(this, userService) },
                { Route.EditUser, () => new EditUserPage(this, userService) },
                { Route.AddActivities, () => new AddActivitiesPage(this, activityService) },
                { Route.EditMembershipType, () => new EditMembershipTypePage(this, membershipService) },
                { Route.AddMembershipType, () => new AddMembershipTypePage(this, membershipService) }
                
            };
        }

        /// <summary>
        ///     Constructor der modtager referencer til 
        ///     ContentControl (Element vi sætter UserControls under) og Grid (Menu).
        /// </summary>
        /// <param name="route">Using the enum Route to decide routing direction</param>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        /// <updated
        public void Navigate(Route route)
        {
            if (!routes.TryGetValue(route, out Func<UserControl>? view))
            {
                MessageBox.Show($"No view registered for route {route}.", nameof(route));
                this.Navigate(Route.Home);
                //throw new ArgumentException($"No view registered for route {route}.", nameof(route));
            }
            else
            {
                control.Content = view();
                currentRoute = route;
            }
        }

        public enum Route
        {
            Login,
            Home,
            Activities,
            Members,
            Trainers,
            Locations,
            Memberships,
            Profile,
            AddUser,
            EditUser,
            AddActivities,
            EditMembershipType,
            AddMembershipType
        }

    }
}
