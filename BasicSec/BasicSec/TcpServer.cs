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
using System.Windows.Media;
using System.Windows.Threading;

namespace BasicSec
{
    class TcpServer
    {
        private TcpListener _server;
        private AutoResetEvent connectionWaitHandle = new AutoResetEvent(false);
        private Boolean _isRunning;

        public TcpServer(int port,TextBox statusTextBox)
        {
            Console.WriteLine("i come here");
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            while (true)
            {
                //IAsyncResult result = _server.BeginAcceptTcpClient(HandleAsyncConnection, _server);
                //connectionWaitHandle.WaitOne(); // Wait until a client has begun handling an event
                //connectionWaitHandle.Reset(); // Reset wait handle or the loop goes as fast as it can (after first request)
                TcpClient client = _server.AcceptTcpClient();
                StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
                
                String sData = null;

                while (client.Connected)
                {
                    // reads from stream
                    while (sReader.EndOfStream == false)
                    {
                        sData += sReader.ReadLine();
                    }
                    
                        Application.Current.Dispatcher.Invoke(
                       DispatcherPriority.Background,
                       new Action(() =>
                           // shows content on the console.
                           statusTextBox.Text =  System.DateTime.Now + sData + Environment.NewLine + statusTextBox.Text

                           ));
                    client.Close();
                }
                   





                    // to write something back.
                    // sWriter.WriteLine("Meaningfull things here");
                    // sWriter.Flush();

                }
            }
        

        //public void HandleAsyncConnection(IAsyncResult obj)
        //{
        //    // retrieve client from parameter passed to thread
        //    TcpClient client = _server.AcceptTcpClient();

        //    // sets two streams
           
        //    StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8);
        //    // you could use the NetworkStream to read and write, 
        //    // but there is no forcing flush, even when requested

        //    Boolean bClientConnected = true;
        //    String sData = null;

        //    while (bClientConnected)
        //    {
        //        // reads from stream
        //        sData = sReader.ReadLine();

        //        // shows content on the console.
        //        ((MainWindow) System.Windows.Application.Current.MainWindow).listStatus.Text +=Environment.NewLine +  new DateTime().TimeOfDay+sData;
        //        Console.WriteLine("Client &gt; " + sData);

        //        // to write something back.
        //        // sWriter.WriteLine("Meaningfull things here");
        //        // sWriter.Flush();
        //    }
        //}
    }
}
