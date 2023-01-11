using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SanlabSimülasyonServer
{
    //Server Side
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Bağlantı Bekleniyor.");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client bağlandı.");
                NetworkStream stream = client.GetStream();
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        }
                    }
                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine("Client: " + request);
                    string date = DateTime.Now.ToString("MM/dd/yyyy");
                    sw.WriteLine(request + " " + date);
                    sw.Flush(); // Client'a mesajı gönderiyoruz.
                }
                catch (Exception e)
                {
                    Console.WriteLine("Hata var");
                    sw.WriteLine(e.ToString());
                }
            }
        }
    }
}
