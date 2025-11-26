using FoersteSemesterproeve.Presentation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static FoersteSemesterproeve.Presentation.NavigationRouter;

namespace FoersteSemesterproeve.Presentation
{
    internal class NavigationRouter
    {
        Route currentRoute;
        ContentControl control;
        Dictionary<Route, UserControl> routes;

        public NavigationRouter(ContentControl mainContentController)
        {
            this.control = mainContentController;
            routes = new Dictionary<Route, UserControl>
            {
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
                throw new ArgumentException($"No view registered for route {route}.", nameof(route));
            }

            control.Content = view;
            currentRoute = route;
        }

        public enum Route
        {
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
