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
        UserService userService;
        public MembershipsPage(NavigationRouter router, MembershipService membershipServiceInput, UserService userService)
        {
            InitializeComponent();

            this.router = router;
            membershipService = membershipServiceInput;
            this.userService = userService;

            GridMembershipTypes.HorizontalAlignment = HorizontalAlignment.Center;

            DrawMembershipTypes();
        }

        private void AddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            router.Navigate(NavigationRouter.Route.AddMembershipType);
        }

        private void EditMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = (Button)sender;
            MembershipType membershipType = (MembershipType)editButton.Tag;

            membershipService.targetMembershipType = membershipType;

            router.Navigate(NavigationRouter.Route.EditMembershipType);
        }

        private void DrawMembershipTypes()
        {
            GridMembershipTypes.Children.Clear();
            GridMembershipTypes.ColumnDefinitions.Clear();

            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int amountOfItemsPerRow = 4;
            for (int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                // modulus dividere to variabler og returnere derefter den resterende mængde
                iRemainder = i % amountOfItemsPerRow;
                if(iRemainder == 0)
                {
                    GridMembershipTypes.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    rows++;
                    if(columns == 0)
                    {
                        GridMembershipTypes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 0, MaxWidth = 900 });
                    }
                }

                if(iRemainder != 0 && columns < amountOfItemsPerRow)
                {
                    GridMembershipTypes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 0, MaxWidth = 900 });
                    columns++;
                }

                StackPanel membershipStack = new StackPanel();
                membershipStack.Orientation = Orientation.Vertical;
                membershipStack.Margin = new Thickness(25, 25, 25, 25);
                

                TextBlock membershipTypeName = new TextBlock();
                membershipTypeName.Text = membershipService.membershipTypes[i].name;
                membershipTypeName.FontSize = 20;
                membershipTypeName.Margin = new Thickness(0, 0, 0, 10);
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

                int amountOfUsers = 0;
                for(int j = 0; j < userService.users.Count; j++)
                {
                    if (membershipService.membershipTypes[i] == userService.users[j].membershipType)
                    {
                        amountOfUsers++;
                    }
                }

                TextBlock membersOnThisMembership = new TextBlock();
                membersOnThisMembership.Text = $"Current members: {amountOfUsers}";
                membersOnThisMembership.TextAlignment = TextAlignment.Center;
                membersOnThisMembership.Margin = new Thickness(0, 10, 0, 0);
                membershipStack.Children.Add(membersOnThisMembership);

                Button editButton = new Button();
                editButton.Content = "Edit";
                editButton.FontSize = 10;
                editButton.Margin = new Thickness(0, 20, 0, 10);
                editButton.Tag = membershipService.membershipTypes[i];
                editButton.Click += EditMembershipTypeButton_Click;
                membershipStack.Children.Add(editButton);

                Button deleteButton = new Button();
                deleteButton.Content = "Delete";
                deleteButton.FontSize = 10;
                deleteButton.Margin = new Thickness(0, 10, 0, 10);
                deleteButton.Tag = membershipService.membershipTypes[i];
                deleteButton.Click += DeleteMembershipTypeButton_Click;
                membershipStack.Children.Add(deleteButton);

                Border border = new Border();
                border.Padding = new Thickness(5);
                border.Margin = new Thickness(10);
                border.BorderBrush = new SolidColorBrush(Colors.LightGray);
                border.BorderThickness = new Thickness(1);
                border.CornerRadius = new CornerRadius(20);
                GridMembershipTypes.Children.Add(border);
                border.Child = membershipStack;
                Grid.SetRow(border, rows);
                Grid.SetColumn(border, iRemainder);
            }
        }

        private void DeleteMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            MembershipType membershipType = (MembershipType)button.Tag;

            for(int i = 0; i < userService.users.Count; i++)
            {
                if (userService.users[i].membershipType == membershipType)
                {
                    MessageBox.Show("You can't delete a membership type that is in use");
                    return;
                }

            }

            if(membershipType != null)
            {
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete {membershipType.name}?");
                dialogBox.ShowDialog();
                if (dialogBox.DialogResult == true)
                {
                    membershipService.membershipTypes.Remove(membershipType);
                    DrawMembershipTypes();
                }
                
            }
        }
    }
}
