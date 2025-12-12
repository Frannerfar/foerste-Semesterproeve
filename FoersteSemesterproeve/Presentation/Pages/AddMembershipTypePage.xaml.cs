using FoersteSemesterproeve.Domain.Services;
using System.Windows;
using System.Windows.Controls;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for AddMembershipTypePage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class AddMembershipTypePage : UserControl
    {
        // fields
        NavigationRouter router;
        MembershipService membershipService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="membershipService"></param>
        public AddMembershipTypePage(NavigationRouter router, MembershipService membershipService)
        {
            InitializeComponent();
            // håndtering af constructorens input her
            this.router = router;
            this.membershipService = membershipService;
        }

        /// <summary>
        /// funktion for cancel knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelAddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // router tilbage til MembershipTypes siden
            router.Navigate(NavigationRouter.Route.MembershipTypes);
        }

        /// <summary>
        /// funktion save knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveNewMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // kører int.TryParse for at konvertere dataen i textbox til en int så det kan gemmes som en int
            // gemmes i bool for at tjekke om det var et tal eller ej
            bool isMonthlyInteger = int.TryParse(MonthlyInput.Text, out int monthlyInputInteger);
            bool isYearlyInteger = int.TryParse(YearlyInput.Text, out int yearlyInputInteger);

            // hvis textboxen er null eller tom, køres denne if statement og brugeren bliver sendt udenfor denne funktion
            if(string.IsNullOrEmpty(TitleInput.Text))
            {
                MessageBox.Show("Title can not be empty");
                return;
            }
            // hvis de to bools vi testede i før der det samme som false, og derfor ikke er tal, bliver disse to if statements kørt
            // brugeren bliver igen her sendt ud af funktionen
            if(isMonthlyInteger == false)
            {
                MessageBox.Show("Monthly Pay only takes numbers");
                return;
            }

            if(isYearlyInteger == false)
            {
                MessageBox.Show("Yearly Pay only takes numbers");
                return;
            }

            // tilføjer her de tre textboxes input til funktionen AddMembershipType som tilføjer som tilføjer det nye membershipType til listen over membershipTypes
            membershipService.AddMembershipType(TitleInput.Text, monthlyInputInteger, yearlyInputInteger);

            // router her tilbage til siden med MembershipTypes
            router.Navigate(NavigationRouter.Route.MembershipTypes);
        }
    }
}
