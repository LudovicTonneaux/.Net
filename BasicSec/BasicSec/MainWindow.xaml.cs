using Microsoft.Win32;
using System;
using System.Collections;
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
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace BasicSec
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        private List<string> contacten = new List<string>(); //lijst van alle zenders en ontvangers
       private  NetworkStream stream;
        private byte[] buf;
       
        public MainWindow()
        {
            InitializeComponent();
            //Thread thread = Thread.CurrentThread;
            //this.DataContext = new
            //{
            //    ThreadId = thread.ManagedThreadId
            //};
           
                Thread thread = new Thread(() =>
                {
                    TcpServer server = new TcpServer(8889, listStatus);
                    //System.Windows.Threading.Dispatcher.Run();
                });
                thread.Start();
           
                
            
            if (!Directory.Exists(@".\Contacten"))
            {
                Directory.CreateDirectory(@".\Contacten");
            }
            
            ContactenUpdate();
        
            listBoxZenders.ItemsSource = contacten;
            listBoxZenders.SelectedIndex = 0;
            //listBoxOntvangers.ItemsSource = contacten;
            //listBoxOntvangers.SelectedIndex = 1;
            
           // IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            
           
        }

       
               
                



        //RadioButtons
        //private void radioButton(object sender, RoutedEventArgs e)
        //{
        //    if (radioButtonEncrypteren.IsChecked == true)
        //    {
        //        radioButtonDecrypteren.IsChecked = false;
        //        buttonEncrypterenDecrypteren.Content = "Encrypteren";
        //        labelHashCheck.Visibility = System.Windows.Visibility.Hidden;
        //    }
        //    else if (radioButtonDecrypteren.IsChecked == true)
        //    {
        //        radioButtonEncrypteren.IsChecked = false;
        //        buttonEncrypterenDecrypteren.Content = "Decrypteren";
        //        labelHashCheck.Visibility = System.Windows.Visibility.Visible;
        //    }
        //}

        //encrypteren en decrypteren
        private void buttonEncrypterenDecrypteren_Click(object sender, RoutedEventArgs e)
        {
            //    if (radioButtonDecrypteren.IsChecked == true)
            //    {
            //        if (File.Exists(@".\Boodschap.txt"))
            //        {
            //            File.Delete(@".\Boodschap.txt");
            //        }

            //        StreamWriter outputFile = new StreamWriter(@".\Boodschap.txt");
            //        outputFile.WriteLine(textBoxBoodschap.Text);
            //        outputFile.Close();
            //        outputFile.Dispose();

            string sourceip = GetIP(listBoxZenders.SelectedItem.ToString());
            //string destinationip = GetIP(listBoxOntvangers.SelectedItem.ToString());
            string filelocation = System.IO.Path.GetFullPath(@".\Boodschap.txt");
            string destinationusername = listBoxZenders.SelectedItem.ToString();
            //string destinationusername = listBoxOntvangers.SelectedItem.ToString();

            //        NaarServerSturen(sourceip + ";" + destinationip + ";" + filelocation + ";" + sourceusername + ";" + destinationusername);

            //    }
            //    else if (radioButtonEncrypteren.IsChecked == true)
            //    {
            //        if (File.Exists(@".\Boodschap.txt"))
            //        {
            //            File.Delete(@".\Boodschap.txt");
            //        }

            //        StreamWriter outputFile = new StreamWriter(@".\Boodschap.txt");
            //        outputFile.WriteLine(textBoxBoodschap.Text);
            //        outputFile.Close();
            //        outputFile.Dispose();

            //        string sourceip = GetIP(listBoxZenders.SelectedItem.ToString());
            //        string destinationip = GetIP(listBoxOntvangers.SelectedItem.ToString());
            //        string filelocation = System.IO.Path.GetFullPath(@".\Boodschap.txt");
            //        string sourceusername = listBoxZenders.SelectedItem.ToString();
            //        string destinationusername = listBoxOntvangers.SelectedItem.ToString();
            string path=  Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            
        
            string textPath = path + "\\CRYPTO\\CryptoSavedFile" + 0 + ".txt";
            int i = 1;
            while (File.Exists(textPath))
            {
                textPath = path + "\\CryptoSavedFile" + i + ".txt";
                i++;
            }
            File.WriteAllText(textPath, textBoxBoodschap.Text);
            if (textBoxBoodschap.IsEnabled == true)
            {
                NaarServerSturen(sourceip + ";" + "192.168.1.55" + ";" + filelocation + ";" + "ik" + ";" +
                                 destinationusername);
            }
            else if (textBoxBoodschap.IsEnabled==false)
            {
                NaarServerSturen(sourceip + ";" + "192.168.1.55" + ";" + textPath + ";" + "ik" + ";" +
                                 destinationusername);
            }
            
               
            }

        public string GetIP(string naam)
        {
            StreamReader reader = new StreamReader(@".\Contacten\" + naam + @"\IP.txt");
            string ip = reader.ReadLine();
            reader.Close();
            reader.Dispose();
            return ip;
        }

        public void NaarServerSturen(string text)
        {
             TcpClient client = new TcpClient("127.0.0.1", 8889); //connecteren met de server
          
         
            stream = client.GetStream();
            buf = Encoding.UTF8.GetBytes(text + "\n");
            stream.Write(buf, 0, text.Length + 1);
            stream.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
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
            if (contacten.Count >= 1)
            {
                listBoxZenders.SelectedIndex = 0;
                //listBoxOntvangers.SelectedIndex = 1;
                buttonEncrypterenDecrypteren.IsEnabled = true;
            }
            else buttonEncrypterenDecrypteren.IsEnabled = false;

        }

        void ContactenUpdaten(object sender, EventArgs e)
        {
            contacten.Clear();
            ContactenUpdate();
            listBoxZenders.Items.Refresh();
            //listBoxOntvangers.Items.Refresh();
        }

        private void BoodschapOpenen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.Multiselect = false;
            string filepath;
            if (openFileDialog1.ShowDialog() == true)
            {
                filepath = openFileDialog1.FileName;
                textBoxBoodschap.Text = filepath;
                textBoxBoodschap.IsEnabled = false;
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

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }
    }
}
