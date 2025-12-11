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
    /// Interaction logic for AddMembershipTypePage.xaml
    /// </summary>
    public partial class AddMembershipTypePage : UserControl
    {
        NavigationRouter router;
        MembershipService membershipService;

        public AddMembershipTypePage(NavigationRouter router, MembershipService membershipService)
        {
            InitializeComponent();

            this.router = router;
            this.membershipService = membershipService;
        }

        private void CancelAddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.MembershipTypes);
        }

        private void SaveNewMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            bool isMonthlyInteger = int.TryParse(MonthlyInput.Text, out int monthlyInputInteger);
            bool isYearlyInteger = int.TryParse(YearlyInput.Text, out int yearlyInputInteger);

            if(string.IsNullOrEmpty(TitleInput.Text))
            {
                MessageBox.Show("Title can not be empty");
                return;
            }
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

            membershipService.AddMembershipType(TitleInput.Text, monthlyInputInteger, yearlyInputInteger);

            router.Navigate(NavigationRouter.Route.MembershipTypes);
        }
    }
}
