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
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for ContactenToevoegen.xaml
    /// </summary>
    public partial class ContactenToevoegen : Window
    {
        public ContactenToevoegen()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string naam = textBoxNaam.Text;
            string ip = textBoxIP.Text;

            if (Directory.Exists(@".\Contacten\" + naam))
            {
                MessageBox.Show("Dit contact bestaat al");
            }
            else
            {
                Directory.CreateDirectory(@".\Contacten\" + naam);
                using (StreamWriter outputFile = new StreamWriter(@".\Contacten\" + naam + @"\IP.txt"))
                {
                    outputFile.WriteLine(ip);
                    MessageBox.Show("Dit contact is goed toegevoegd!");
                }
            }
        }
    }
}
