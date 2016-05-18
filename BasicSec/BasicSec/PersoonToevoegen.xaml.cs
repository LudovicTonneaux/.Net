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
    /// Interaction logic for PersoonToevoegen.xaml
    /// </summary>
    public partial class PersoonToevoegen : Window
    {
        public PersoonToevoegen()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string naam = textBoxNaam.Text;
            string ip = textBoxIP.Text;

            if (Directory.Exists(@".\Personen\" + naam))
            {
                MessageBox.Show("Deze persoon bestaat al");
            }
            else
            {
                Directory.CreateDirectory(@".\Personen\" + naam);
                using (StreamWriter outputFile = new StreamWriter(@".\Personen\" + naam + @"\IP.txt"))
                {
                    outputFile.WriteLine(ip);
                    MessageBox.Show("De persoon is goed toegevoegd!");
                }
            }
        }
    }
}
