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
    /// <created>26-11-2025</created>
    /// <updated>27-11-2025</updated>
    public partial class AddUserPage : UserControl
    {
        NavigationRouter router;
        UserService userService;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="router"></param>
        /// <param name="userService"></param>
        public AddUserPage(NavigationRouter router, UserService userService)
        {
            InitializeComponent();
            this.router = router;
            this.userService = userService;

            AdminCheckbox.IsChecked = false;
            TrainerCheckbox.IsChecked = false;
            DatePicker.SelectedDate = DateTime.Now;

            foreach (MembershipType membershipType in userService.membershipService.membershipTypes)
            {
                MembershipComboBox.Items.Add(membershipType.name);
            }
            MembershipComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>27-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            int? postal = null;

            if (!ValidateRequiredTextBox(FirstNameBox, FirstNameFlag))
            {
                Debug.WriteLine("Firstname not containing anything");
                flag = true;
            }

            if (!ValidateRequiredTextBox(LastNameBox, LastNameFlag))
            {
                Debug.WriteLine("Lastname not containing anything");
                flag = true;
            }

            if (!ValidateRequiredTextBox(EmailBox, EmailFlag))
            {
                Debug.WriteLine("Email not containing anything");
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

            DateTime pickedDateTime; 
            if(DatePicker.SelectedDate.HasValue)
            {
                pickedDateTime = DatePicker.SelectedDate.Value;
                Debug.WriteLine("Datepicker set to Correct time");
            }
            else
            {
                pickedDateTime = DateTime.Now;
                Debug.WriteLine("Datepicker set to time now");
            }
            DateOnly pickedDateOnly = DateOnly.FromDateTime(pickedDateTime);

            if (!flag)
            {
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
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
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
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="datePicker"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool ValidateDatePicker(DatePicker datePicker, Label flag)
        {
            if (datePicker.SelectedDate is not DateTime dt)
            {
                flag.Visibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                flag.Visibility = Visibility.Visible;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
        /// <param name="textBox"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool ValidateRequiredTextBox(TextBox textBox, Label flag)
        {
            if(isValidString(textBox.Text)) 
            { 
                flag.Visibility = Visibility.Collapsed;
                return true; 
            }
            else
            {
                flag.Visibility = Visibility.Visible;
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>28-11-2025</created>
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
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Users);
        }
    }
}
