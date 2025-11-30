using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for EditMembershipTypePage.xaml
    /// </summary>
    public partial class EditMembershipTypePage : UserControl
    {
        NavigationRouter router;
        MembershipService membershipService;

        public EditMembershipTypePage(NavigationRouter router, MembershipService membershipService)
        {
            InitializeComponent();

            this.router = router;
            this.membershipService = membershipService;
            if(membershipService.targetMembershipType != null)
            {
                TitleInput.Text = membershipService.targetMembershipType.name;
                MonthlyInput.Text = membershipService.targetMembershipType.monthlyPayDKK.ToString();
                YearlyInput.Text = membershipService.targetMembershipType.yearlyPayDKK.ToString();
            }
        }

        private void CancelAddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.Memberships);
        }

        private void SaveNewMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if(membershipService.targetMembershipType != null)
            {
                if(TitleInput.Text == null || MonthlyInput.Text == null || YearlyInput.Text == null)
                {
                    MessageBox.Show("You have to fill out all boxes");
                    return;
                }

                if(TitleInput.Text != null && MonthlyInput.Text != null && YearlyInput != null)
                {
                    bool isMonthlyInteger = int.TryParse(MonthlyInput.Text, out int monthlyInputInteger);
                    bool isYearlyInteger = int.TryParse(YearlyInput.Text, out int yearlyInputInteger);

                    if(isMonthlyInteger == true)
                    {
                        membershipService.targetMembershipType.monthlyPayDKK = monthlyInputInteger;
                    }

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

                    membershipService.targetMembershipType.name = TitleInput.Text;

                    router.Navigate(NavigationRouter.Route.Memberships);
                }

            }

            else
            {
                MessageBox.Show("TargetMembershipType is null");
            }

        }
    }
}
