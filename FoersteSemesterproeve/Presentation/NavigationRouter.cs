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
    public class NavigationRouter
    {
        Route currentRoute;
        ContentControl control;
        Grid menuGrid;
        Dictionary<Route, UserControl> routes;

        public NavigationRouter(ContentControl mainContentController, Grid menuGrid)
        {
            this.control = mainContentController;
            this.menuGrid = menuGrid;
            routes = new Dictionary<Route, UserControl>
            {
                { Route.Login, new LoginPage(this, menuGrid) },
                { Route.Home, new HomePage() },
                { Route.Activities, new ActivitiesPage() },
                { Route.Members, new MembersPage() },
                { Route.Trainers, new TrainersPage() },
                { Route.Locations, new LocationsPage() },
                { Route.Memberships, new MembershipsPage() },
                { Route.Profile, new ProfilePage() },
            };
        }


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
