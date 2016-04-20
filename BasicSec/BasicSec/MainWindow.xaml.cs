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

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void radioButton(object sender, RoutedEventArgs e)
        {
            if (radioButtonEncrypteren.IsChecked == true)
            {
                radioButtonDecrypteren.IsChecked = false;
                buttonEncrypterenDecrypteren.Content = "Encrypteren";
                labelHashCheck.IsEnabled = false;
            }
            else if (radioButtonDecrypteren.IsChecked == true)
            {
                radioButtonEncrypteren.IsChecked = false;
                buttonEncrypterenDecrypteren.Content = "Decrypteren";
                labelHashCheck.IsEnabled = true;
            }
        }
    }
}
