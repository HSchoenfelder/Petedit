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
using System.Windows.Shapes;

namespace PetriNetEditor
{
    /// <summary>
    /// Interaction logic for QueryDialog.xaml
    /// </summary>
    public partial class QueryDialog : Window
    {
        public QueryDialog()
        {
            InitializeComponent();

            var rawIcon = System.Drawing.SystemIcons.Question;
            Icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                rawIcon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Tag = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Tag = true;
        }
    }
}
