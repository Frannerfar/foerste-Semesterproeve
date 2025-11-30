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
                MembershipTypeName.Text = membershipService.targetMembershipType.name;
            }
        }
    }
}
