using FoersteSemesterproeve.Domain.Services;
using System;
using System.Windows;
using System.Windows.Controls;


namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditMembershipTypePage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class EditMembershipTypePage : UserControl
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
        public EditMembershipTypePage(NavigationRouter router, MembershipService membershipService)
        {
            InitializeComponent();
            // håndtering af constructorens input
            this.router = router;
            this.membershipService = membershipService;

            // hvis targetMembershipType ikke er null køres dette
            if(membershipService.targetMembershipType != null)
            {
                // tre forskellige textboxe bliver sat til at være targetMembershipTypes data
                // to af dataene er int derfor konverters disse fra int til string for at kunne sætte i textbox
                TitleInput.Text = membershipService.targetMembershipType.name;
                MonthlyInput.Text = membershipService.targetMembershipType.monthlyPayDKK.ToString();
                YearlyInput.Text = membershipService.targetMembershipType.yearlyPayDKK.ToString();
            }
        }

        /// <summary>
        /// funktion for cancel knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelAddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // router tilbage til forrige side
            router.Navigate(NavigationRouter.Route.MembershipTypes);
        }

        /// <summary>
        /// funktion for save knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveNewMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // hvis targetMembershipType ikke er null køres dette
            if(membershipService.targetMembershipType != null)
            {
                // hvis bare en af textboxenes input er null, køres dette
                if(TitleInput.Text == null || MonthlyInput.Text == null || YearlyInput.Text == null)
                {
                    // brugeren får vist dette og bliver sendt ud af loopet
                    MessageBox.Show("You have to fill out all boxes");
                    return;
                }

                // hvis alle textboxene ikke er lig med null, køres dette
                if(TitleInput.Text != null && MonthlyInput.Text != null && YearlyInput != null)
                {
                    // TryParse for at forsøge at konvertere string inputtet i textbox til en int værdi
                    // sættes i bool for at køre validering og fordi int.TryParse er en bool
                    bool isMonthlyInteger = int.TryParse(MonthlyInput.Text, out int monthlyInputInteger);
                    bool isYearlyInteger = int.TryParse(YearlyInput.Text, out int yearlyInputInteger);

                    // hvis konverteringen lykkedes og det var tal der blev skrevet ind i begge boxe, køres de to næste if statements
                    if(isMonthlyInteger == true)
                    {
                        membershipService.targetMembershipType.monthlyPayDKK = monthlyInputInteger;
                    }

                    // hvis konverteringen mislykkedes og det ikke var et tal der blev skrevet ind i begge boxe, køres de næste to else og brugeren bliver sendt ud af loopet
                    else
                    {
                        MessageBox.Show("You have to write a number in Monthly Pay");
                        return;
                    }

                    if (isYearlyInteger == true)
                    {
                        membershipService.targetMembershipType.yearlyPayDKK = yearlyInputInteger;
                    }

                    else
                    {
                        MessageBox.Show("You have to write a number in Yearly Pay");
                        return;
                    }

                    // sætter texbox data i targetMembershipTypes name
                    membershipService.targetMembershipType.name = TitleInput.Text;

                    // router tilbage til siden med membershipTypes
                    router.Navigate(NavigationRouter.Route.MembershipTypes);
                }

            }

            else
            {
                MessageBox.Show("TargetMembershipType is null");
            }

        }
    }
}
