using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        //TODO
        //String versturen naar server
        //hashcheck
        //layout
        //IP koppelen aan namen -> txt bestand in hun map
        //Personen kunnen Toevoegen


        public List<string> personen = new List<string>(); //lijst van alle zenders en ontvangers
        public TcpClient client = new TcpClient("192.168.1.1", 8888); //connecteren met de server
        public NetworkStream stream;
        public byte[] buf;

        public MainWindow()
        {
            stream = client.GetStream();

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
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.Multiselect = false;
            string filepath;
            if (openFileDialog1.ShowDialog() == true)
            {
                filepath = openFileDialog1.FileName;
                textBoxGekozenBestand.Text = File.ReadAllText(filepath);
            }
        }

        //encrypteren en decrypteren
        private void buttonEncrypterenDecrypteren_Click(object sender, RoutedEventArgs e)
        {
            if (radioButtonDecrypteren.IsChecked == true)
            {


            }
            else if (radioButtonEncrypteren.IsChecked == true)
            {
                
            }
        }


        public void NaarServer(string text)
        {
            buf = Encoding.UTF8.GetBytes(text + "\n");
            stream.Write(buf, 0, text.Length + 1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stream.Close();
        }

        public void Personen()
        {
            foreach (string naam in Directory.GetDirectories(@".\Personen"))
            {
                personen.Add(naam.Remove(0, 11));
            }
        }
    }
}
