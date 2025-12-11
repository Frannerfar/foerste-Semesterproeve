using System;
using System.Windows;


namespace FoersteSemesterproeve.Views
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    /// <author>Martin</author>
    /// <created>26-11-2025</created>
    public partial class DialogBox : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <param name="text"></param>
        public DialogBox(string text)
        {
            InitializeComponent();
            DialogBoxText.Text = text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
