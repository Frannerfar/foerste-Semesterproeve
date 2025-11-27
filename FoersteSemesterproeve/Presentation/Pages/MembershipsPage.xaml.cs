using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for MembershipsPage.xaml
    /// </summary>
    public partial class MembershipsPage : UserControl
    {
        NavigationRouter router;
        MembershipService membershipService;
        public MembershipsPage(NavigationRouter router, MembershipService membershipServiceInput)
        {
            InitializeComponent();

            this.router = router;
            membershipService = membershipServiceInput;


            for(int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                None.Text = membershipService.membershipTypes[0].name;
                noneList.Items.Add($"Monthly Pay {membershipService.membershipTypes[0].monthlyPayDKK} DKK");
                noneList.Items.Add($"Yearly Pay {membershipService.membershipTypes[0].yearlyPayDKK} DKK");
                
                Standard.Text = membershipService.membershipTypes[1].name;
                standardList.Items.Add($"Monthly Pay {membershipService.membershipTypes[1].monthlyPayDKK} DKK");
                standardList.Items.Add($"Yearly Pay {membershipService.membershipTypes[1].yearlyPayDKK} DKK");
                    
                Pro.Text = membershipService.membershipTypes[2].name;
                proList.Items.Add($"Monthly Pay {membershipService.membershipTypes[2].monthlyPayDKK} DKK");
                proList.Items.Add($"Yearly Pay {membershipService.membershipTypes[2].yearlyPayDKK} DKK");
                break;
            }


        }
    }
}
