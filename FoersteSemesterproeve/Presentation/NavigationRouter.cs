using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Presentation.Pages;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FoersteSemesterproeve.Presentation
{


    /// <summary>
    ///     Håndterer navigation mellem forskellige pages / usercontrolsi samme Window
    /// </summary>
    /// <author>Martin</author>
    public class NavigationRouter
    {
        Route currentRoute;
        ContentControl MainContent;
        Grid menuGrid;

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
        ///     Constructor til NavigationRouter
        /// </summary>
        /// <author>Martin</author>
        /// <param name="contentControl"></param>
        /// <param name="menuGrid"></param>
        /// <param name="userProfileButton"></param>
        /// <param name="adminButtons"></param>
        /// <param name="userService"></param>
        /// <param name="activityService"></param>
        /// <param name="locationService"></param>
        /// <param name="membershipService"></param>
        /// <param name="HomeButton"></param>
        /// <param name="ActivitiesButton"></param>
        /// <param name="TrainersButton"></param>
        /// <param name="LocationsButton"></param>
        /// <param name="MembersButton"></param>
        /// <param name="MembershipsButton"></param>
        public NavigationRouter(ContentControl contentControl, Grid menuGrid, Button userProfileButton, List<Button> adminButtons, UserService userService, ActivityService activityService, LocationService locationService, MembershipService membershipService, Button HomeButton, Button ActivitiesButton, Button TrainersButton, Button LocationsButton, Button MembersButton, Button MembershipsButton)
        {

            // parametre fra constructoren sættes alle i fields, så de er tilgængelige i resten af klassen
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

            // SolidColorBrush objekter oprettes ud fra pre-definerede colors oppe i fields
            // SolidColorBrush sættes i menuStaticItem og menuActiveItem, men med forskellige farver.
            menuStaticItem = new SolidColorBrush(menuStaticItemColor);
            menuActiveItem = new SolidColorBrush(menuActiveItemColor);

            // Sætter HomeButton til at være default aktiv button
            currentActiveMenuButton = HomeButton;
            // Sæt button til at have farve som aktiv.
            SetMenuButtonActive(currentActiveMenuButton, HomeButton);
        }

        /// <summary>
        ///     Bruges til at navigere til en ny "side".
        /// </summary>
        /// <author>Martin</author>
        /// <param name="route"></param>
        public void Navigate(Route route)
        {
            // parameteren route (enum Route længere ned i filen) sammenlignes med forskellige cases
            switch (route)
            {
                // Hvis route == Route.Login
                case Route.Login:
                    // Så sættes MainContent (som er en reference til ControlContent controller i MainWindow.xaml)
                    // til at være indholdet af et nyt instantieret "page" / UserControl
                    MainContent.Content = new LoginPage(this, menuGrid, userService, userProfileButton, adminButtons);
                    break;
                case Route.Home:
                    // Så sættes MainContent (som er en reference til ControlContent controller i MainWindow.xaml)
                    // til at være indholdet af et nyt instantieret "page" / UserControl
                    MainContent.Content = new HomePage(this, userService, activityService);
                    // Kalder funktionen SetMenuButtonActive, der sætter den nye button til at være den aktive og fjerner den gamle som aktiv.
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
        ///     Sørger for at visuelt sætte indikation på, hvilken route man befinder sig på.
        ///     Dette gøres ved at sætte den nuværende aktive button tilbage til vores standard menu farve. 
        ///     Derefter sættes den nye Button der er blevet klikket på, til få den nye aktive farve, samt til at være den nye aktive Button.
        /// </summary>
        /// <author>Martin</author>
        /// <param name="originalButton"></param>
        /// <param name="newButton"></param>
        private void SetMenuButtonActive(Button originalButton, Button newButton)
        {
            if (originalButton != null)
            {
                // hovedmenuens button der tidligere var aktiv sættes til at være den 
                // oprindeligefarve "menuStaticItem" (SolidColorBrush med Color)
                originalButton.Background = menuStaticItem;
            }
            // Nye button sættes til en anden baggrundsfarve "menuActiveItem" (SolidColorBrush med Color)
            newButton.Background = menuActiveItem;
            // Nye button sættes til at være active
            currentActiveMenuButton = newButton;
        }

        /// <summary>
        ///     Bruges til at at sætte den valgte button til oprindelige farve (SolidColorBrush med Color)
        /// </summary>
        /// <author>Martin</author>
        /// <param name="originalButton"></param>
        private void ResetButtonActive(Button originalButton)
        {
            // hovedmenuens button der tidligere var aktiv sættes til at være den
            // oprindeligefarve "menuStaticItem" (SolidColorBrush med Color)
            originalButton.Background = menuStaticItem;
        }

        /// <summary>
        ///     Enum til navngivning af unikke værdier.
        ///     Bruges til routing.
        /// </summary>
        /// <author>Martin</author>
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
