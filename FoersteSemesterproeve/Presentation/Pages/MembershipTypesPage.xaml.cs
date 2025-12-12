using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FoersteSemesterproeve.Presentation.Pages
{
    /// <summary>
    /// Interaction logic for MembershipsPage.xaml
    /// </summary>
    /// <author>Marcus</author>
    public partial class MembershipTypesPage : UserControl
    {
        // fields
        NavigationRouter router;
        MembershipService membershipService;
        UserService userService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="router"></param>
        /// <param name="membershipServiceInput"></param>
        /// <param name="userService"></param>
        public MembershipTypesPage(NavigationRouter router, MembershipService membershipServiceInput, UserService userService)
        {
            InitializeComponent();
            //håndtering af constructorens input
            this.router = router;
            membershipService = membershipServiceInput;
            this.userService = userService;
            // sætter her gridet til at være centralt
            GridMembershipTypes.HorizontalAlignment = HorizontalAlignment.Center;
            // funktionen der printer siden ud med alle membershipTypes
            DrawMembershipTypes();
        }

        /// <summary>
        /// funktion tilhørende tilføj membershipType
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            //router hen til næste side
            router.Navigate(NavigationRouter.Route.AddMembershipType);
        }

        /// <summary>
        /// funktion tilhørende edit knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // sender er klikket så derfor typecaster vi til Button da det kun kan være Button
            // sætter sender i editButton
            Button editButton = (Button)sender;
            // editButton.Tag har et membershipType i sig så derfor typecaster vi til MembershipType
            // sætter editButton.Tag i membershipType
            MembershipType membershipType = (MembershipType)editButton.Tag;

            // sætter membershipType i targetMembershipType
            membershipService.targetMembershipType = membershipType;

            // router til næste side
            router.Navigate(NavigationRouter.Route.EditMembershipType);
        }

        /// <summary>
        /// funktion der printer siden ud med alle membershipTypes
        /// </summary>
        /// <author>Marcus</author>
        private void DrawMembershipTypes()
        {
            // starter med at fjerne alt i gridet for at printe det hele ud på ny for at opdatere siden
            GridMembershipTypes.Children.Clear();
            GridMembershipTypes.ColumnDefinitions.Clear();

            // variabler
            int rows = 0;
            int columns = 0;
            int iRemainder = 0;
            int amountOfItemsPerRow = 4;
            // looper listen med membershipTypes ud
            for (int i = 0; i < membershipService.membershipTypes.Count; i++)
            {
                // modulus dividere to variabler og returnere derefter den resterende mængde
                // returneringen fra modulus sættes i iRemainder
                iRemainder = i % amountOfItemsPerRow;
                // hvis variablen iRemainder er lig med nul køres dette
                if(iRemainder == 0)
                {
                    // tilføjer et row per iteration og lægger 1 til variablen rows
                    GridMembershipTypes.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                    rows++;
                    // hvis variablen columns er lig nul køres dette
                    if(columns == 0)
                    {
                        // tilføjer en column per iteration
                        GridMembershipTypes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 0, MaxWidth = 900 });
                    }
                }

                // hvis iRemainder ikke er nul og variablen columns er mindre end variablen amountOfItemsPerRow, køres dette
                if(iRemainder != 0 && columns < amountOfItemsPerRow)
                {
                    // tilføjer en column per iteration og lægger 1 til variablen columns
                    GridMembershipTypes.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star), MinWidth = 0, MaxWidth = 900 });
                    columns++;
                }

                // instantierer nyt stackpanel og sætter diverse værdier til den
                StackPanel membershipStack = new StackPanel();
                membershipStack.Orientation = Orientation.Vertical;
                membershipStack.Margin = new Thickness(25, 25, 25, 25);
                
                // instantierer tre nye textblocke og sætter diverse værdier
                // tilføjer alle tre textblocke til stackpanel fra før
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
                // lopper listen af users ud
                for(int j = 0; j < userService.users.Count; j++)
                {
                    // hvis iterationen af users membershipType er det samme som iterationen af membershipTypes, køres dette 
                    if (membershipService.membershipTypes[i] == userService.users[j].membershipType)
                    {
                        // der bliver tilføjet 1 til variablen amountOfUsers
                        amountOfUsers++;
                    }
                }

                // instantierer ny textblock og tilføjer til stackpanel fra før
                TextBlock membersOnThisMembership = new TextBlock();
                membersOnThisMembership.Text = $"Current members: {amountOfUsers}";
                membersOnThisMembership.TextAlignment = TextAlignment.Center;
                membersOnThisMembership.Margin = new Thickness(0, 10, 0, 0);
                membershipStack.Children.Add(membersOnThisMembership);

                // instantierer to knapper, edit og delete
                // giver dem diverse værdier, tag, funktion ved klik og tilføjer dem til stackpanel fra før
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

                // instantierer et border som gives diverse værdier
                Border border = new Border();
                border.Padding = new Thickness(5);
                border.Margin = new Thickness(10);
                border.BorderBrush = new SolidColorBrush(Colors.LightGray);
                border.BorderThickness = new Thickness(1);
                border.CornerRadius = new CornerRadius(20);
                // tilføjer først border til det allerede eksisterende grid
                GridMembershipTypes.Children.Add(border);
                // sætter så stackpanel fra før til at være child til border
                border.Child = membershipStack;
                // to funktioner der sætter hvor mange rows og columns der skal være i gridet
                Grid.SetRow(border, rows);
                Grid.SetColumn(border, iRemainder);
            }
        }

        /// <summary>
        /// funktion der hører til slet knap
        /// </summary>
        /// <author>Marcus</author>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMembershipTypeButton_Click(object sender, RoutedEventArgs e)
        {
            // sender er klikket så vi typecaster sender til typen Button da det kun kan være en Button
            // sætter her sender i button
            Button button = (Button)sender;
            // button.Tag har membershipType med sig derfor typecaster vi til typen MembershipType
            // sætter her button.Tag i membershipType
            MembershipType membershipType = (MembershipType)button.Tag;

            // looper listen med users us
            for(int i = 0; i < userService.users.Count; i++)
            {
                // if statement køres hvis iterationens membershipType er det samme som variablen membershipType
                if (userService.users[i].membershipType == membershipType)
                {
                    // brugeren får dette vist og bliver taget ud af loopet igen
                    MessageBox.Show("You can't delete a membership type that is in use");
                    return;
                }

            }

            // køres hvis membershipType ikke er null
            if(membershipType != null)
            {
                // brugeren får dette vist
                DialogBox dialogBox = new DialogBox($"Are you sure you want to delete {membershipType.name}?");
                dialogBox.ShowDialog();
                // hvis brugeren trykker ja i dialogboxen bliver DialogResult sat til true og if statementet køres derfor
                if (dialogBox.DialogResult == true)
                {
                    // det specifikke membershipType fjernes og funktionen DrawMembershipType køres igen
                    membershipService.DeleteMembershipTypeByObject(membershipType);
                    DrawMembershipTypes();
                }
                
            }
        }
    }
}
