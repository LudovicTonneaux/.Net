using System;
using System.Collections.Generic;
using System.IO;
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

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for ContactVerwijderen.xaml
    /// </summary>
    public partial class ContactVerwijderen : Window
    {

        public List<string> contacten = new List<string>(); //lijst van alle zenders en ontvangers

        public ContactVerwijderen()
        {
            ContactenUpdate();

            InitializeComponent();

            ButtonCheck();

            listBoxContacten.ItemsSource = contacten;
        }

        private void buttonVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            Directory.Delete(@".\Contacten\" + listBoxContacten.SelectedItem.ToString(), true);
            contacten.Clear();
            ContactenUpdate();
            listBoxContacten.Items.Refresh();
            ButtonCheck();
        }

        public void ContactenUpdate()
        {
            foreach (string naam in Directory.GetDirectories(@".\Contacten"))
            {
                contacten.Add(naam.Remove(0, 12));
            }
        }

        public void ButtonCheck()
        {
            if (contacten.Count == 0)
            {
                buttonVerwijderen.IsEnabled = false;
            }
            else
            {
                buttonVerwijderen.IsEnabled = true;
                listBoxContacten.SelectedIndex = 0;
            }
        }
    }
}
