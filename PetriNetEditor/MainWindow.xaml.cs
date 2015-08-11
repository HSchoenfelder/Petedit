using Microsoft.Win32;
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

namespace PetriNetEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((MainViewModel)DataContext).ErrorNotice += HandleErrorNotice;
            Binding bind = new Binding("SaveFile");
            bind.Mode = BindingMode.OneWay;
            SetBinding(SaveFileProperty, bind);
            bind = new Binding("Modified");
            bind.Mode = BindingMode.OneWay;
            SetBinding(ModifiedProperty, bind);
        }

        public string SaveFile
        {
            get { return (string)GetValue(SaveFileProperty); }
            set { SetValue(SaveFileProperty, value); }
        }

        public static readonly DependencyProperty SaveFileProperty =
            DependencyProperty.Register(
            "SaveFile", 
            typeof(String), 
            typeof(MainWindow),
            new PropertyMetadata(null, OnSaveFilePropertyChanged));

        public string TitleString
        {
            get { return (string)GetValue(TitleStringProperty); }
            set { SetValue(TitleStringProperty, value); }
        }

        public static readonly DependencyProperty TitleStringProperty =
            DependencyProperty.Register(
            "TitleString",
            typeof(String),
            typeof(MainWindow),
            new PropertyMetadata("Petrinet Editor"));

        public bool Modified
        {
            get { return (bool)GetValue(ModifiedProperty); }
            set { SetValue(ModifiedProperty, value); }
        }

        public static readonly DependencyProperty ModifiedProperty =
            DependencyProperty.Register(
            "Modified",
            typeof(bool),
            typeof(MainWindow),
            new PropertyMetadata(false));

        private static void OnSaveFilePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mw = (MainWindow)d;
            if (e.NewValue != null)
                mw.TitleString = "Petrinet Editor - " + System.IO.Path.GetFileName((String)e.NewValue);
            else
                mw.TitleString = "Petrinet Editor";
        }

        private void HandleSetTokenCountClicked(object sender, ExecutedRoutedEventArgs e)
        {
            TokenDialog tokenDialog = new TokenDialog();
            tokenDialog.Owner = this;
            if (tokenDialog.ShowDialog() == true)
            {
                TextBox tokenBox = (TextBox)((FrameworkElement)VisualTreeHelper.GetParent((Shape)e.Parameter)).FindName("tokenBox");
                tokenBox.Text = tokenDialog.TokenCount;
            }
        }

        private void HandleSetTokenCountCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter is Ellipse)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void HandleNewClicked(object sender, RoutedEventArgs e)
        {
            if (PerformPotentialOptionalSave() == false)
                return;

            ((MainViewModel)DataContext).NewFileCommand.Execute(null);
        }

        private void HandleOpenClicked(object sender, RoutedEventArgs e)
        {
            if (PerformPotentialOptionalSave() == false)
                return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNML Files|*.pnml|XML Files|*.xml";
            openFileDialog.DefaultExt = ".pnml";
            if (openFileDialog.ShowDialog() == true)
                ((MainViewModel)DataContext).LoadFileCommand.Execute(openFileDialog.FileName);
        }

        private void HandleSaveClicked(object sender, RoutedEventArgs e)
        {
            if (SaveFile == null)
                HandleSaveAsClicked(sender, e);
            else
                ((MainViewModel)DataContext).SaveFileCommand.Execute(null);
        }

        private void HandleSaveAsClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNML Files|*.pnml|XML Files|*.xml|All Files|*.*";
            saveFileDialog.DefaultExt = ".pnml";
            if (saveFileDialog.ShowDialog() == true)
                ((MainViewModel)DataContext).SaveFileCommand.Execute(saveFileDialog.FileName);
        }

        private void HandleExitClicked(object sender, RoutedEventArgs e)
        {
            if (PerformPotentialOptionalSave() == false)
                return;
            Application.Current.Shutdown();
        }

        private void HandleErrorNotice(object sender, NotificationEventArgs e)
        {
            var rawIcon = System.Drawing.SystemIcons.Error;
            var icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                rawIcon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            InfoDialog errorDialog = new InfoDialog("Error", icon);
            errorDialog.Owner = this;
            errorDialog.Message = e.Message;
            errorDialog.ShowDialog();
        }

        private bool PerformPotentialOptionalSave()
        {
            if (Modified)
            {
                QueryDialog queryDialog = new QueryDialog();
                queryDialog.Owner = this;
                if (queryDialog.ShowDialog() == true)
                    HandleSaveClicked(this, new RoutedEventArgs());
                else 
                {
                    if (queryDialog.Tag == null)
                        return false;
                    else if((bool)queryDialog.Tag)
                        return false;
                }
            }
            return true;
        }
    }
}
