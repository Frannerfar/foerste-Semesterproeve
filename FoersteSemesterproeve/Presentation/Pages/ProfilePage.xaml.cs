using FoersteSemesterproeve.Domain.Services;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class ProfilePage : UserControl
    {
        // Klassens fields
        NavigationRouter router;
        UserService userService;
        MembershipService membershipService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        /// <param name="membershipService"></param>
        public ProfilePage(NavigationRouter router, UserService userService, MembershipService membershipService)
        {
            InitializeComponent();
            // Her gemmes input i constructoren i fields
            this.router = router;
            this.userService = userService;
            this.membershipService = membershipService;

            // If statement der kører hvis authenticatedUser ikke er null
            if(userService.authenticatedUser != null)
            {
                // Diverse textbokse på siden som bliver fyldt med det data der er i authenticatedUsers forskellige værdier
                FirstNameBox.Text = userService.authenticatedUser.firstName;
                LastNameBox.Text = userService.authenticatedUser.lastName;
                EmailBox.Text = userService.authenticatedUser.email;
                CityBox.Text = userService.authenticatedUser.city;
                AddressBox.Text = userService.authenticatedUser.address;
                // Ved de sidste to textboxe sker der en konvertering, da textbox tager imod string men det vi prøver at lægge ind i textboxen er int
                PostalBox.Text = userService.authenticatedUser.postal.ToString();
                DatePicker.Text = userService.authenticatedUser.dateofBirth.ToString();

                // Et for loop der looper igennem indholdet af list membershipTypes
                for(int i = 0; i < membershipService.membershipTypes.Count; i++)
                {
                    // Tilføjer hver iteration til en Combobox
                    MembershipComboBox.Items.Add(membershipService.membershipTypes[i].name);
                    // if statement der kører hvis iterationen i for loopet er det samme som authenticatedUsers membershipType
                    if (membershipService.membershipTypes[i] == userService.authenticatedUser.membershipType)
                    {
                        // Comboboxens predefinerede index sættes her til at være iterationens membershipType
                        MembershipComboBox.SelectedIndex = i;
                    }
                }
            }
        }

        /// <summary>
        /// Funktion tilhørende en knap der viser siden med tilmeldte aktiviteter
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivitiesButton_Click(object sender, RoutedEventArgs e)
        {
            // Sætter authenticatedUser til være targetUser sådan at de rigtige data bliver vist på den side man skal hen til
            userService.targetUser = userService.authenticatedUser;
            // Funktion der router hen til den næste side
            router.Navigate(NavigationRouter.Route.UserActivities);
        }

        /// <summary>
        /// Funktion tilhørende en knap der gør det muligt for brugeren at ændre i textboxe
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // Diverse textboxe bliver sat til enten at være enabled eller disabled
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            EmailBox.IsEnabled = true;
            CityBox.IsEnabled = true;
            AddressBox.IsEnabled = true;
            PostalBox.IsEnabled = true;
            DatePicker.IsEnabled = false;
            MembershipComboBox.IsEnabled = true;

            // Denne knap kollapser når den bliver trykket
            EditInfoButton.Visibility = Visibility.Collapsed;
            // Disse to knapper bliver visible og tager den første knaps plads når den bliver trykket på
            SaveEditInfoButton.Visibility = Visibility.Visible;
            CancelEditModeButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Funktion tilhørerende en knap der gemmer inputtet fra textboxe i authenticatedUser
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // if statement der kører hvis authenticatedUser ikke er null
            if(userService.authenticatedUser != null)
            {
                // if statament der kører hvis bare en enkelt af disse boxe er null
                if(FirstNameBox == null ||
                    LastNameBox == null ||
                    EmailBox == null ||
                    CityBox == null ||
                    AddressBox == null ||
                    PostalBox == null ||
                    DatePicker == null ||
                    MembershipComboBox == null)
                {
                    // Brugeren modtager så denne besked
                    MessageBox.Show("Can't leave a box empty");
                    return;
                }   

                // if statement der kører hvis ingen af de boxe er null
                if(FirstNameBox != null &&
                    LastNameBox != null &&
                    EmailBox != null &&
                    CityBox != null &&
                    AddressBox != null &&
                    PostalBox != null &&
                    DatePicker != null &&
                    MembershipComboBox != null)
                {
                    // laver en TryParse funktion for at tjekke om det er det rigtige type input der bliver tastet ind
                    // Vi får en string ind som vi gerne vil lave om til en int
                    // Dette gemmer vi så i en bool for at kunne bruge sandt/falsk til at validere inputtet
                    bool isPostal = int.TryParse(PostalBox.Text, out int postalBoxInput);
                    // if statement der kører hvis boolen er false, og at det så ikke var en int der blev tastet ind
                    if(isPostal == false)
                    {
                        // Brugeren får vist denne besked
                        MessageBox.Show("Postal Box only takes numbers");
                    }

                    // Variabel af typen DateTime
                    DateTime pickedDateTime;
                    // if statement der kører hvis datepickeren har en value
                    if (DatePicker.SelectedDate.HasValue)
                    {
                        // her sættes den valgte value over i variablen over if statementet
                        pickedDateTime = DatePicker.SelectedDate.Value;
                        Debug.WriteLine("Datepicker set to Correct time");
                    }
                    else
                    {
                        //hvis der ikke bliver valgt noget i datepickeren så får brugeren denne besked
                        MessageBox.Show("You have to choose a birthdate");
                        return;
                    }

                    // her konverteres der date fra DateTime til DateOnly 
                    DateOnly pickedDateOnly = DateOnly.FromDateTime(pickedDateTime);

                    // authenticatedUser får her sat sine værdier ud fra det der er skrevet/valgt i de forskellige boxe 
                    userService.authenticatedUser.firstName = FirstNameBox.Text;
                    userService.authenticatedUser.lastName = LastNameBox.Text;
                    userService.authenticatedUser.email = EmailBox.Text;
                    userService.authenticatedUser.city = CityBox.Text;
                    userService.authenticatedUser.address = AddressBox.Text;
                    userService.authenticatedUser.postal = postalBoxInput;
                    userService.authenticatedUser.dateofBirth = pickedDateOnly;
                    userService.authenticatedUser.membershipType = membershipService.membershipTypes[MembershipComboBox.SelectedIndex];
                    
                    // diverse boxe bliver nu sat til at disabled således at brugeren ikke kan ændre i dem
                    FirstNameBox.IsEnabled = false;
                    LastNameBox.IsEnabled = false;
                    EmailBox.IsEnabled = false;
                    CityBox.IsEnabled = false;
                    AddressBox.IsEnabled = false;
                    PostalBox.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    MembershipComboBox.IsEnabled = false;

                    // edit knappen bliver synlig igen mens de to andre knapper kolapser
                    EditInfoButton.Visibility = Visibility.Visible;
                    SaveEditInfoButton.Visibility = Visibility.Hidden;
                    CancelEditModeButton.Visibility = Visibility.Hidden;    
                }
            }

        }

        /// <summary>
        /// funktion tilhørende cancel knappen
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelEditModeButton_Click(object sender, RoutedEventArgs e)
        {
            // diverse boxe bliver sat til disabled
            FirstNameBox.IsEnabled = false;
            LastNameBox.IsEnabled = false;
            EmailBox.IsEnabled = false;
            CityBox.IsEnabled = false;
            AddressBox.IsEnabled = false;
            PostalBox.IsEnabled = false;
            DatePicker.IsEnabled = false;
            MembershipComboBox.IsEnabled = false;

            // edit knappen bliver igen synlig og de to andre knapper kolapser
            EditInfoButton.Visibility = Visibility.Visible;
            SaveEditInfoButton.Visibility = Visibility.Hidden;
            CancelEditModeButton.Visibility = Visibility.Hidden;

            // boxe bliver nu sat til den data der allerede er i authenticatedUser
            // hvilket vil sige at hvis brugeren har skrevet noget i en box og trykker cancel, så bliver det ikke gemt
            FirstNameBox.Text = userService.authenticatedUser.firstName;
            LastNameBox.Text = userService.authenticatedUser.lastName;
            EmailBox.Text = userService.authenticatedUser.email;
            CityBox.Text = userService.authenticatedUser.city;
            AddressBox.Text = userService.authenticatedUser.address;
            PostalBox.Text = userService.authenticatedUser.postal.ToString();
            DatePicker.Text = userService.authenticatedUser.dateofBirth.ToString();

            // looper list membershipTypes igen
            for (int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                // tilføjer de forskellige iterationer til comboboxen
                MembershipComboBox.Items.Add(membershipService.membershipTypes[i].name);
                // if statement der kører hvis den pågældende iterations membershipType er den samme som authenticatedUsers MembershipType
                if (membershipService.membershipTypes[i] == userService.authenticatedUser.membershipType)
                {
                    // den førnævnte iteration bliver sat som det valgte index i comboboxen
                    MembershipComboBox.SelectedIndex = i;
                }
            }
        }

    }
}
