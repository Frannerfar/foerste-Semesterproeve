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
            router.Navigate(NavigationRouter.Route.Memberships);
        }

        private void SaveNewMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if(membershipService.targetMembershipType != null)
            {
                membershipService.AddMembershipType(
                    TitleInput.Text,
                    int.Parse(MonthlyInput.Text),
                    int.Parse(YearlyInput.Text));
            }

            router.Navigate(NavigationRouter.Route.Memberships);
        }
    }
}
