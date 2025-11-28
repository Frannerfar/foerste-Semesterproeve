using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
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

            GridMembershipTypes.HorizontalAlignment = HorizontalAlignment.Center;

            for(int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                GridMembershipTypes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 0, MaxWidth = 900 });

                StackPanel membershipStack = new StackPanel();
                Grid.SetRow(membershipStack, 0);
                Grid.SetColumn(membershipStack, i);
                membershipStack.Orientation = Orientation.Vertical;
                membershipStack.Margin = new Thickness(25, 0, 25, 0);
                GridMembershipTypes.Children.Add(membershipStack);

                TextBlock membershipTypeName = new TextBlock();
                membershipTypeName.Text = membershipService.membershipTypes[i].name;
                membershipTypeName.FontSize = 20;
                membershipTypeName.TextAlignment = TextAlignment.Center;
                membershipStack.Children.Add(membershipTypeName);

                TextBlock membershipTypeMonthly = new TextBlock();
                membershipTypeMonthly.Text = $"MONTHLY {membershipService.membershipTypes[i].monthlyPayDKK}DKK";
                membershipTypeMonthly.TextAlignment = TextAlignment.Center;
                membershipStack.Children.Add(membershipTypeMonthly);

                TextBlock membershipTypeYearly = new TextBlock();
                membershipTypeYearly.Text = $"YEARLY {membershipService.membershipTypes[i].yearlyPayDKK}DKK";
                membershipTypeYearly.TextAlignment = TextAlignment.Center;
                membershipStack.Children.Add(membershipTypeYearly);

                Button editButton = new Button();
                editButton.Content = "Edit";
                editButton.FontSize = 10;
                editButton.Tag = membershipService.membershipTypes[i];
                editButton.Click += EditMembershipTypeButton_Click;
                membershipStack.Children.Add(editButton);

                Button deleteButton = new Button();
                deleteButton.Content = "Delete";
                deleteButton.FontSize = 10;
                deleteButton.Click += DeleteMembershipTypeButton_Click;
                membershipStack.Children.Add(deleteButton);
            }


        }

        private void AddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddMembershipType);
        }

        private void EditMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.EditMembershipType);
        }

        private void DeleteMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            MembershipType membershipType = (MembershipType)button.Tag;

            if(membershipType != null)
            {
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete {membershipType.name}?");
                dialogBox.ShowDialog();
                if(dialogBox.DialogResult == true)
                {
                    membershipService.DeleteMembershipTypeByObject(membershipType);
                }
            }
        }
    }
}
