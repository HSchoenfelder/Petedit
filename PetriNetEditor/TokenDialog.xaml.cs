using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PetriNetEditor
{
    /// <summary>
    /// Interaction logic for TokenDialog.xaml
    /// </summary>
    public partial class TokenDialog : Window
    {
        public TokenDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void tokenBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            tokenBox.SelectAll();
            tokenBox.Focus();
        }

        public String TokenCount
        {
            get { return tokenBox.Text; }
        }
        
        private void tokenBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void tokenBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private bool IsTextAllowed(String text)
        {
            Regex reg = new Regex("[^0-9]+");
            return !reg.IsMatch(text);
        }

        
    }
}
