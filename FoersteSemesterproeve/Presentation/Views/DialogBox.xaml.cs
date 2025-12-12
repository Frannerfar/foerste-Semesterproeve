using System;
using System.Windows;


namespace FoersteSemesterproeve.Views
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    /// <author>Martin</author>
    public partial class DialogBox : Window
    {
        /// <summary>
        ///     Constructor til DialogBox
        /// </summary>
        /// <author>Martin</author>
        /// <param name="text"></param>
        public DialogBox(string text)
        {
            InitializeComponent();

            // parameteren fra constructoren sættes som text i vinduets tekstblok
            DialogBoxText.Text = text;
        }

        /// <summary>
        ///     Bruges af "Yes" button til at sætte DialogResult til true
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult sættes til true og returneres ved luk af vinduet
            DialogResult = true;
            // vinduet lukkes
            this.Close();
        }

        /// <summary>
        ///     Bruges af "No" button til at sætte DialogResult til false
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult sættes til false og returneres ved luk af vinduet
            DialogResult = false;
            // vinduet lukkes
            this.Close();
        }
    }
}
