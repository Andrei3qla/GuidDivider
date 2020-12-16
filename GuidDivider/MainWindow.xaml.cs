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

namespace GuidDivider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MainString { get; set; }
        public Guid FirstGuid { get; set; }
        public Guid SecondGuid { get; set; }
        public bool IsOk { get; set; }

        public MainWindow()
        {
            IsOk = false;
            InitializeComponent();
        }

        private void SourceInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            CountLetters.Content = SourceInput.Text.Length.ToString();
            if (SourceInput.Text.Length == 72)
            {
                MainString = SourceInput.Text;
                if (Guid.TryParse(MainString.Substring(0, 36), out var firstG))
                {
                    FirstGuid = firstG;
                    if (Guid.TryParse(MainString.Substring(36, 36), out var secondG))
                    {
                        SecondGuid = secondG;
                        IsOk = true;

                        ImageIcon.Source = new BitmapImage(new Uri("/GuidDivider;component/Icons/yes.png", UriKind.Relative));
                        FirstOutput.Text = FirstGuid.ToString().ToLower();
                        SecondOutput.Text = SecondGuid.ToString().ToLower();

                        ButtonClick.IsEnabled = true;
                    }
                }
            }

            else if (IsOk)
            {
                ImageIcon.Source = new BitmapImage(new Uri("/GuidDivider;component/Icons/no.png", UriKind.Relative));
                IsOk = false;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(SecondGuid.ToString().ToLower());
        }

        private void SourceInput_GotFocus(object sender, RoutedEventArgs e)
        {
            SourceInput.Focus();
            SourceInput.SelectAll();
            e.Handled = true;
        }
    }
}
