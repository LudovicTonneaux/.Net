﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicSec
{
    class TcpServer
    {
        private TcpListener _server;
        private AutoResetEvent connectionWaitHandle = new AutoResetEvent(false);
        private Boolean _isRunning;

        public TcpServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            while (true)
            {
                IAsyncResult result = _server.BeginAcceptTcpClient(HandleAsyncConnection, _server);
                connectionWaitHandle.WaitOne(); // Wait until a client has begun handling an event
                connectionWaitHandle.Reset(); // Reset wait handle or the loop goes as fast as it can (after first request)
            }
        }

        public void HandleAsyncConnection(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;
            String sData = null;

            while (bClientConnected)
            {
                // reads from stream
                sData = sReader.ReadLine();

                // shows content on the console.
                ((MainWindow) System.Windows.Application.Current.MainWindow).listStatus.Text +=Environment.NewLine +  new DateTime().TimeOfDay+sData;
                Console.WriteLine("Client &gt; " + sData);

                // to write something back.
                // sWriter.WriteLine("Meaningfull things here");
                // sWriter.Flush();
            }
        }
    }
}