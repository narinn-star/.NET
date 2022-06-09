using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace _190529_TcpServerEx
{
    class Program
    {
        static string _strHostIP = "127.0.0.1";
        static byte[] recevbyte = new byte[513];

        static void Main(string[] args)
        {
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(_strHostIP), 9000);
            TcpListener listener = new TcpListener(IPAddress.Any, 9000);

            listener.Start();
            Encoding ASCII = Encoding.ASCII;

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Socket s = client.Client;
                string address = s.RemoteEndPoint.ToString(); //123.123.123.123:12345
                //array[0]는 IP array[1]은 Port
                string[] array = address.Split(new char[] { ':' });
                Console.WriteLine("[TCP 서버] 클라이언트 접속 : IP 주소 = {0}, 포트번호 = {1}", array[0], array[1]);

                try
                {
                    if (client.Connected)
                    {
                        NetworkStream ns = client.GetStream();
                        while (true)
                        {
                            int bytes = ns.Read(recevbyte, 0, recevbyte.Length);
                            String strbyte = ASCII.GetString(recevbyte, 0, bytes);
                            Console.WriteLine("[TCP/{0}:{1}] {2}", array[0], array[1], strbyte);
                            ns.Write(recevbyte, 0, bytes);
                        }
                    }
                    else
                    {
                        Console.WriteLine("이전에 소켓이 연결되지 않았습니다. ");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Thrown: " + ex.ToString());
                }
                finally
                {
                    client.Close();
                }
            }
        }
    }
}
