using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SanlabSimülasyon
{
     class Program
    {
        //Client Side
        static void Main(string[] args)
        {
        connection:
            try
            {
                //127.0.0.1 => Local Host IP adres
                TcpClient client = new TcpClient("127.0.0.1", 1302);
                string messageToSend = "Merhaba";
                byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);

                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
                Console.WriteLine("Server'a veri gönderiliyor.");

                StreamReader sr = new StreamReader(stream); 
                string response = sr.ReadLine(); //serverdean gelen cevabı okuyoruz.
                Console.WriteLine("Server: " + response);

                stream.Close();
                client.Close();
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Bağlantı gerçekleşmedi.");
                goto connection; // Eğer herhangi bir hata olursa tekrar bağlanmaya çalışıyorum.
            }
        }
    }
}
