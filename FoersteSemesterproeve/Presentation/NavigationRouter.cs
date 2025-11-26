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
        Dictionary<Route, UserControl> routes;

        private UserService userService;
        private ActivityService activityService;
        private LocationService locationService;
        private MembershipService membershipService;

        /// <summary>
        ///     Constructor der modtager referencer til 
        ///     ContentControl (Element vi sætter UserControls under) og Grid (Menu).
        /// </summary>
        /// <author>Martin</author>
        /// <created>2025-11-25</created>
        /// <updated
        public NavigationRouter(ContentControl contentControl, Grid menuGrid, UserService userService, ActivityService activityService, LocationService locationService, MembershipService membershipService)
        {
            this.control = contentControl;
            this.menuGrid = menuGrid;

            this.userService = userService;
            this.activityService = activityService;
            this.locationService = locationService;
            this.membershipService = membershipService;

            routes = new Dictionary<Route, UserControl>
            {
                { Route.Login, new LoginPage(this, menuGrid) },
                { Route.Home, new HomePage() },
                { Route.Activities, new ActivitiesPage() },
                { Route.Members, new MembersPage(this, userService) },
                { Route.Trainers, new TrainersPage() },
                { Route.Locations, new LocationsPage() },
                { Route.Memberships, new MembershipsPage() },
                { Route.Profile, new ProfilePage() },
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
            if (!routes.TryGetValue(route, out UserControl? view))
            {
                MessageBox.Show($"No view registered for route {route}.", nameof(route));
                this.Navigate(Route.Home);
                //throw new ArgumentException($"No view registered for route {route}.", nameof(route));
            }
            else
            {
                control.Content = view;
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
            Profile
        }

    }
}
