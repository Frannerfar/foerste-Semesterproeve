using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class AddUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;

        /// <summary>
        ///     Constructor til AddUserPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public AddUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();

            // parametrene fra constructor sættes i fields
            this.router = router;
            this.userService = userService;

            // checkboxes til admin og trainer bliver sat til at være false fra start
            AdminCheckbox.IsChecked = false;
            TrainerCheckbox.IsChecked = false;

            // DatePicker bliver sat til at være datoen i dag, for at starte et sted
            DatePicker.SelectedDate = DateTime.Now;

            // For hver medlemstype der findes i systemet
            foreach (MembershipType membershipType in userService.membershipService.membershipTypes)
            {
                // Tilføj medlemstype navn til dropdown menuen "MembershipComboBox"
                MembershipComboBox.Items.Add(membershipType.name);
            }
            // Sæt det valgte start element til at være den første i listen
            MembershipComboBox.SelectedIndex = 0;
        }

        /// <summary>
        ///     Bruges til at oprette et User objekt ud fra felterne
        /// </summary>
        /// <author>Martin</author>
        /// <created>27-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // hjælpe funktion - sættes til true, hvis én validering fejler
            bool flag = false;
            // postal sættes her som int? (? betyder nullable), da postal i User forventer User?
            int? postal = null;

            // Hvis TextBox ikke indeholder noget
            if (!ValidateRequiredTextBox(FirstNameBox, FirstNameFlag))
            {
                Debug.WriteLine("Firstname not containing anything");
                // flag sættes til true og bruger bliver dermed aldrig tilføjet
                flag = true;
            }
            // Hvis TextBox ikke indeholder noget
            if (!ValidateRequiredTextBox(LastNameBox, LastNameFlag))
            {
                Debug.WriteLine("Lastname not containing anything");
                // flag sættes til true og bruger bliver dermed aldrig tilføjet
                flag = true;
            }
            // Hvis TextBox ikke indeholder noget
            if (!ValidateRequiredTextBox(EmailBox, EmailFlag))
            {
                Debug.WriteLine("Email not containing anything");
                // flag sættes til true og bruger bliver dermed aldrig tilføjet
                flag = true;
            }
            //if (!ValidateDatePicker(DatePicker, DOBFlag))
            //{
            //    Debug.WriteLine("Datepicker set to time now");
            //    flag = true;
            //}
            if (int.TryParse(PostalBox.Text, out int result))
            {
                Debug.WriteLine("Postal set to a number");
                postal = result;
            }

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
                // sæt pickedDateTime til i lige nu
                pickedDateTime = DateTime.Now;
                Debug.WriteLine("Datepicker set to time now");
            }
            // Konverterer fra DateTime til DateOnly
            DateOnly pickedDateOnly = DateOnly.FromDateTime(pickedDateTime);

            // Hvis intet er fejlet
            if (!flag)
            {
                // tilføj nu bruger til systemet
                userService.AddUser(
                    FirstNameBox.Text,
                    LastNameBox.Text,
                    EmailBox.Text,
                    CityBox.Text,
                    AddressBox.Text,
                    pickedDateOnly,
                    postal,
                    GetCheckBoxValue(AdminCheckbox),
                    GetCheckBoxValue(TrainerCheckbox),
                    userService.membershipService.membershipTypes[MembershipComboBox.SelectedIndex]);

                router.Navigate(NavigationRouter.Route.Users);
            }
        }


        /// <summary>
        ///     Hjælpefunktion der bruges til at tjekke om en checkbox er true
        /// </summary>
        /// <author>Martin</author>
        /// <param name="box"></param>
        /// <returns></returns>
        public bool GetCheckBoxValue(CheckBox box)
        {
            if(box.IsChecked == true)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///     Bruges til at validere om TextBox inputs er valide string (eller om de intet indeholder)
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
        /// <param name="textBox"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool ValidateRequiredTextBox(TextBox textBox, Label flag)
        {   
            // Hvis textBox.Text ikke er null eller empty
            if(isValidString(textBox.Text)) 
            { 
                // så skjul label med tekst om forkert input
                flag.Visibility = Visibility.Collapsed;
                return true; 
            }
            // hvis textBox.Text ER empty
            else
            {
                // sæt label til at være synligt
                flag.Visibility = Visibility.Visible;
                return false;
            }
        }


        /// <summary>
        ///     Bruges til at validere om en string er en valid string, eller om den er empty
        /// </summary>
        /// <author>Martin</author>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool isValidString(string input)
        {
            if(string.IsNullOrEmpty(input))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Bruges til at gå tilbage til UsersPage
        /// </summary>
        /// <author>Martin</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Users);
        }
    }
}
