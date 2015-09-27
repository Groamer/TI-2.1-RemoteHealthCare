﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FietsClient
{
    public class TcpConnection
    {
        public TcpClient client;
        private NetworkStream serverStream;
        private string userID;
        private bool isConnectedFlag;

        public TcpConnection()
        {
            // create a connection
            client = new TcpClient();

            connect();
        }

        public bool isConnected()
        {
            return isConnectedFlag;
        }

        public void connect()
        {
                try
                {
                    client.Connect("192.168.178.17", 1288);

                    // create streams
                    serverStream = client.GetStream();
                    Thread t = new Thread(recieve);
                    t.Start();
                    isConnectedFlag = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Thread.Sleep(1000);
                    isConnectedFlag = false;
                }
        }

        public void recieve ()
        {
            while (true)
            {
                byte[] bytesFrom = new byte[(int)client.ReceiveBufferSize];
                serverStream.Read(bytesFrom, 0, (int)client.ReceiveBufferSize);
                String response = Encoding.ASCII.GetString(bytesFrom);
                String[] response_parts = response.Split('|');

                if (response_parts.Length > 0)
                {
                    switch (response_parts[0])
                    {
                        case "0":   //login
                            if (response_parts.Length == 4)
                            {
                                if (response_parts[1] == "1" && response_parts[2] == "1")
                                {
                                    new DoctorForm().Show();
                                }
                                else if(response_parts[2] == "0" && response_parts[1] == "1")
                                {
                                    new PatientForm().Show();
                                }
                                else
                                {
                                    new Login("Geen gebruiker gevonden");
                                }
                            }
                            break;
                    }
                }
            }
        }

        public void sendLogin(string username, string password)
        {
            // send command ( cmdID | username | password )
            sendString("0|" + username + "|" + password);

        }

        public void sendString(string s)
        {
            byte[] b = Encoding.ASCII.GetBytes(s);
            serverStream.Write(b, 0, b.Length);
            serverStream.Flush();
        }

    }
}