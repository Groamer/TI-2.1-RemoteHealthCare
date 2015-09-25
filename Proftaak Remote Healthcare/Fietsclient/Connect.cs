using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Fietsclient
{
    class Connect
    {
        private TcpClient server;
        private NetworkStream networkStream;

        public void LoginServer(String username, String password)
        {
            String ip = "192.168.1.102"; //lokaal adres (tijdelijk)
            int port = 1288; //deze port staat vast

            try
            {
                server.Connect(ip, port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            if (server.Connected)
            {
                String logIn = "|0|" + username + "|" + password;
                SendString(logIn);

                Receive();
            }
        }

        //ontvang antwoord van server
        private void Receive()
        {
            String response = null;

            while (true)
            {
                byte[] bytesFrom = new byte[(int)server.ReceiveBufferSize];
                networkStream.Read(bytesFrom, 0, (int)server.ReceiveBufferSize);
                response = Encoding.ASCII.GetString(bytesFrom);
                String[] response_parts = response.Split('|');

                //indien combinatie wachtwoord/gebruikersnaam niet goed is
                if (response == "0|0|0")
                {
                    Console.WriteLine("Inloggen mislukt");
                }
                else
                {
                    //indien er is ingelogd, nu kijken of een client/doctor is ingelogd
                    switch (response_parts[2])
                    {
                        case "0":
                            //methode om client GUI te laden
                            break;

                        case "1":
                            //methode om doctor GUI te laden
                            break;
                    }
                }
                
                //stop lus
                break;
            }
        }

        //verzend naar server: 0|gebruikersnaam|wachtwoord
        private void SendString(string s)
        {
            byte[] b = Encoding.ASCII.GetBytes(s);
            networkStream.Write(b, 0, b.Length);
            networkStream.Flush();
        }
    }
}