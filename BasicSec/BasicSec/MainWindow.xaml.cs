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
using MahApps.Metro.Controls;

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        //TODO
        //String versturen naar server
        //layout

        public List<string> contacten = new List<string>(); //lijst van alle zenders en ontvangers
        //public TcpClient client = new TcpClient("192.168.1.1", 8888); //connecteren met de server
        public NetworkStream stream;
        public byte[] buf;

        public MainWindow()
        {
            //stream = client.GetStream();

            if (!Directory.Exists(@".\Contacten"))
            {
                Directory.CreateDirectory(@".\Contacten");
            }

            InitializeComponent();

            ContactenUpdate();

            listBoxZenders.ItemsSource = contacten;
            listBoxZenders.SelectedIndex = 0;
            listBoxOntvangers.ItemsSource = contacten;
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


        public void NaarServerSturen(string text)
        {
            buf = Encoding.UTF8.GetBytes(text + "\n");
            stream.Write(buf, 0, text.Length + 1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //stream.Close();
        }

        public void ContactenUpdate()
        {
            foreach (string naam in Directory.GetDirectories(@".\Contacten"))
            {
                contacten.Add(naam.Remove(0, 12));
            }
            buttonCheck();
        }

        public void buttonCheck()
        {
            if (contacten.Count > 0)
            {
                listBoxZenders.SelectedIndex = 0;
                listBoxOntvangers.SelectedIndex = 0;
                buttonEncrypterenDecrypteren.IsEnabled = true;
            }
            else buttonEncrypterenDecrypteren.IsEnabled = false;

        }

        void ContactenUpdaten(object sender, EventArgs e)
        {
            contacten.Clear();
            ContactenUpdate();
            listBoxZenders.Items.Refresh();
            listBoxOntvangers.Items.Refresh();
        }

        private void BoodschapOpenen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.Multiselect = false;
            string filepath;
            if (openFileDialog1.ShowDialog() == true)
            {
                filepath = openFileDialog1.FileName;
                textBoxBoodschap.Text = File.ReadAllText(filepath);
            }
        }

        private void BoodschapOpslaan_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, textBoxBoodschap.Text);
        }

        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ContactToevoegen_Click(object sender, RoutedEventArgs e)
        {
            ContactenToevoegen nieuwContact = new ContactenToevoegen();
            nieuwContact.Show();
            nieuwContact.Closed += new EventHandler(ContactenUpdaten);
        }

        private void ContactVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (contacten.Count > 0)
            {
                ContactVerwijderen wegContact = new ContactVerwijderen();
                wegContact.Show();
                wegContact.Closed += new EventHandler(ContactenUpdaten);
            }
            else MessageBox.Show("Maak eerst contacten aan");
        }

    }
}
