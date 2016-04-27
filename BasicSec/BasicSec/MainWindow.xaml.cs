using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string filepath; //pad van het bestand
        public List<Persoon> personen = new List<Persoon>(); //lijst van alle zenders en ontvangers


        public MainWindow()
        {
            Personen();

            InitializeComponent();

            listBoxZenders.ItemsSource = personen;
            listBoxZenders.SelectedIndex = 0;
            listBoxOntvangers.ItemsSource = personen;
            listBoxOntvangers.SelectedIndex = 1;
        }

        //RadioButtons
        private void radioButton(object sender, RoutedEventArgs e)
        {
            if (radioButtonEncrypteren.IsChecked == true)
            {
                radioButtonDecrypteren.IsChecked = false;
                buttonEncrypterenDecrypteren.Content = "Encrypteren";
                labelHashCheck.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (radioButtonDecrypteren.IsChecked == true)
            {
                radioButtonEncrypteren.IsChecked = false;
                buttonEncrypterenDecrypteren.Content = "Decrypteren";
                labelHashCheck.Visibility = System.Windows.Visibility.Visible;
            }
        }

        //Bestand selecteren
        private void buttonBestand_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == true)
            {
                filepath = openFileDialog1.FileName;
                textBoxGekozenBestand.Text = filepath;
            }
        }

        //Personen laden
        private void Personen()
        {
            List<string> namen = new List<string>();
            foreach (string s in Directory.GetDirectories(@".\Personen"))
            {
                namen.Add(s.Remove(0, 11));
            }

            foreach (string naam in namen)
            {
                Persoon persoon = new Persoon(naam);

                if (!File.Exists(@".\Personen\" + naam + @"\Private.txt"))
                {
                    //private key aanmaken
                    StreamReader reader = new StreamReader(@".\Personen\" + naam + @"\Private.txt");
                    persoon.privateKey = reader.ReadLine();
                    reader.Close();
                    reader.Dispose();
                }
                else
                {
                    StreamReader reader = new StreamReader(@".\Personen\" + naam + @"\Private.txt");
                    persoon.privateKey = reader.ReadLine();
                    reader.Close();
                    reader.Dispose();
                }

                if (!File.Exists(@".\Personen\" + naam + @"\Private.txt"))
                {
                    //public key aanmaken
                    StreamReader reader = new StreamReader(@".\Personen\" + naam + @"\Public.txt");
                    persoon.publicKey = reader.ReadLine();
                    reader.Close();
                    reader.Dispose();
                }
                else
                {
                    StreamReader reader = new StreamReader(@".\Personen\" + naam + @"\Public.txt");
                    persoon.publicKey = reader.ReadLine();
                    reader.Close();
                    reader.Dispose();
                }

                personen.Add(persoon);
            }
        }

        //encrypteren en decrypteren
        private void buttonEncrypterenDecrypteren_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonDecrypteren.IsChecked == true)
            {
                //Decrypteren
                //HashCheck
            }
            else if (radioButtonEncrypteren.IsChecked == true)
            {
                //Encrypteren
            }
        }
    }
}
