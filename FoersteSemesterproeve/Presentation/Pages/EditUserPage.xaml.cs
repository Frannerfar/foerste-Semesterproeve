using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditUserPage.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class EditUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;


        /// <summary>
        ///     Constructor til EditUserPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public EditUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();

            // paremetrene fra constructoren sættes i fields
            this.router = router;
            this.userService = userService;


            // hvis targetUser IKKE er null
            if (userService.targetUser != null)
            {
                // SÅ SÆT DATA PÅ ALLE TEKST INPUT FELTERNE UD FRA TARGETUSER
                EditUserText.Text = $"'{userService.targetUser.firstName} {userService.targetUser.lastName}'";
                
                FirstNameBox.Text = userService.targetUser?.firstName;
                LastNameBox.Text = userService.targetUser?.lastName;
                EmailBox.Text = userService.targetUser?.email;
                CityBox.Text = userService.targetUser?.city;
                AddressBox.Text = userService.targetUser?.address;
                PostalBox.Text = userService.targetUser?.postal.ToString();

                // DatePickeren i XAML bliver sat til at have DateTime der matcher targetUser dateofbirth (DateOnly)
                DatePicker.SelectedDate = new DateTime(userService.targetUser.dateofBirth, new TimeOnly(0, 0));
                
                // Hvis targetUser er admin, så tjek boksen af for admin
                if(userService.targetUser.isAdmin) { AdminCheckbox.IsChecked = true; }
                // Hvis targetUser er træner, så tjek boksen af for træner
                if (userService.targetUser.isCoach) { TrainerCheckbox.IsChecked = true; }

                // For hvert medlemskab der eksisterer i systemet
                for(int i = 0; i < userService.membershipService.membershipTypes.Count; i++)
                {
                    // tilføj hvert membershipType navn til dropdown boksen "MembershipComboBox"
                    MembershipComboBox.Items.Add(userService.membershipService.membershipTypes[i].name);
                    // Hvis medlemskabstypen matcher med targetUsers medlemsskabstype
                    if(userService.membershipService.membershipTypes[i] == userService.targetUser?.membershipType)
                    {
                        // så sættes den standard valgte i dropdown menuen til at være denne
                        MembershipComboBox.SelectedIndex = i;
                    }
                }
            }
        }


        /// <summary>
        ///     Bruges til at gemme ændringerne i felternes data i brugeren
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // Hvis targetUser ikke er null
            if (userService.targetUser != null) { 
                // Så sæt targetUsers data til at være teksten fra input felterne
                userService.targetUser.firstName = FirstNameBox.Text;
                userService.targetUser.lastName = LastNameBox.Text;
                userService.targetUser.email = EmailBox.Text;
                userService.targetUser.city = CityBox.Text;
                userService.targetUser.address = AddressBox.Text;
                userService.targetUser.postal = int.Parse(PostalBox.Text);
                
                // DateTime variabel pickedDateTime, da DatePicker returnerer DateTime
                DateTime pickedDateTime;
                // Hvis DatePicker har en value
                if (DatePicker.SelectedDate.HasValue)
                {
                    // sæt pickedDateTime til DatePicker value
                    pickedDateTime = DatePicker.SelectedDate.Value;
                    Debug.WriteLine("Datepicker set to Correct time");
                }
                else
                {
                    // ellers sæt pickedDateTime til lige nu
                    pickedDateTime = DateTime.Now;
                    Debug.WriteLine("Datepicker set to time now");
                }
                // Konverterer fra DateTime til DateOnly
                DateOnly pickedDateOnly = DateOnly.FromDateTime(pickedDateTime);

                // sæt targetUser dateofbirth til nye DateOnly variabel 
                userService.targetUser.dateofBirth = pickedDateOnly;

                // Admin checkbox i XAML IKKE er null
                if (AdminCheckbox.IsChecked != null)
                {
                    // så targetUsers isAdmin til at være værdien af checkboxen
                    userService.targetUser.isAdmin = (bool)AdminCheckbox.IsChecked;
                }
                // Coach checkbox i XAML IKKE er null
                if (TrainerCheckbox.IsChecked != null)
                {
                    // så targetUsers isCoach til at være værdien af checkboxen
                    userService.targetUser.isCoach = (bool)TrainerCheckbox.IsChecked;
                }

                // targetUser medlemsskab sættes til at være det der er valgt i dropdown menuen "MembershipComboBox"
                userService.targetUser.membershipType = userService.membershipService.membershipTypes[MembershipComboBox.SelectedIndex];

                // sæt isCoachText og isAdminText properties på targetUser
                userService.targetUser.CheckBothMarks();
                // Naviger til siden "UsersPage".
                router.Navigate(NavigationRouter.Route.Users);
            }
        }

        /// <summary>
        ///     Bruges til at navigere tilbage til side med regneark over brugere
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Users);
        }

        /// <summary>
        ///     Bruges til at slette brugeren fra listen af brugere
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Hvis targetUser IKKE er null
            if (userService.targetUser != null) 
            {
                // Hvis targetUser er admin
                if (userService.targetUser.isAdmin)
                {
                    MessageBox.Show("You can't delete an admin");
                    return;
                }
                // Instantier en dialogbox og spørg om brugeren skal slettes
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete {userService.targetUser.firstName} {userService.targetUser.lastName}?");
                dialogBox.ShowDialog();
                // Hvis der svares ja
                if (dialogBox.DialogResult == true)
                {
                    // Fjern brugeren ved hjælp af userService
                    userService.users.Remove(userService.targetUser);
                    // Naviger til siden over alle brugere "UsersPage"
                    router.Navigate(NavigationRouter.Route.Users);
                }
            } 
        }

        /// <summary>
        ///     Bruges til at navigere til "UsersActivitiesPage"
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.UserActivities);
        }
    }
}
