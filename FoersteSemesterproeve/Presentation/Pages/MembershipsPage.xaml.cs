using FoersteSemesterproeve.Domain.Models;
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
    /// Interaction logic for MembershipsPage.xaml
    /// </summary>
    public partial class MembershipsPage : UserControl
    {
        public MembershipsPage()
        {
            InitializeComponent();

            MembershipType noneMember = new MembershipType("NONE", 0, 0);
            None.Text = noneMember.name;
            noneList.Items.Add($"{noneMember.monthlyPayDKK} DKK MONTHLY");
            noneList.Items.Add($"{noneMember.yearlyPayDKK} DKK YEARLY");

            MembershipType standardMember = new MembershipType("STANDARD", 200, 2400);
            Standard.Text = standardMember.name;
            standardList.Items.Add($"{standardMember.monthlyPayDKK} DKK MONTHLY");
            standardList.Items.Add($"{standardMember.yearlyPayDKK} DKK YEARLY");
            standardList.Items.Add("FREE ACCESS TO THE GYM");
            
            MembershipType proMember = new MembershipType("PRO", 400, 4800);
            Pro.Text = proMember.name;
            proList.Items.Add($"{proMember.monthlyPayDKK} DKK MONTHLY");
            proList.Items.Add($"{proMember.yearlyPayDKK} DKK YEARLY");
            proList.Items.Add("FREE ACCESS TO THE GYM");
            proList.Items.Add("PERSONALIZED TRAININGPLAN");
        }
    }
}
