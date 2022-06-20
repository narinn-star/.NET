using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _20203179C
{
    class Program
    {
        static string _strHostIP = "127.0.0.1";
        static string send_str, send_msg, receive_msg, nick;
        static NetworkStream ns;

        static void Main(string[] args)
        {
            TcpClient tclient = new TcpClient(_strHostIP, 9000);        // TcpClient : 소켓 생성 후 connect가 자동으로 완성
            byte[] recev = new byte[513];
            Byte[] sendByte = new Byte[1024];
            int n = 1;

            try
            {
                // 소켓이 연결되었다면(true)
                if (tclient.Connected)
                {
                    Console.WriteLine("소켓이 연결되었습니다. 종료하려면 Ctrl + Z");
                    ns = tclient.GetStream();                      
                    
                    while (true)
                    {
                        if (n == 1) 
                        {
                            Console.Write("닉네임 : ");
                            nick = Console.ReadLine();
                            if (nick == null)
                                return;
                            byte[] sendnick = Encoding.ASCII.GetBytes(nick);

                            ns.Write(sendnick, 0, sendnick.Length);

                            n++;
                        }
                        
                        else //send 보내보쟈
                        { 
                            //Console.Write("보낼 데이터 : ");
                            //send_msg = nick;
                            //sendByte = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(send_msg);
                            //ns.Write(sendByte, 0, sendByte.Length);

                            send_msg = nick + " : " + Console.ReadLine();
                            //send_str = Console.ReadLine();
                            if (send_msg == null)
                                break;

                            sendByte = Encoding.ASCII.GetBytes(send_msg);    // ASCII == UTF8, 문자열을 아스키 코드로 변환 후 byte로 변환
                            ns.Write(sendByte, 0, sendByte.Length);                 // Write() : stream의 메소드, send -> write

                            Thread thread = new Thread(new ParameterizedThreadStart(Reiceve));
                            thread.Start(tclient);

                            
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Thrown : " + ex.ToString());
            }
            finally
            {
                tclient.Close();
            }
        }

        public static void Reiceve(Object sender)
        {
            while (true)
            {
                Byte[] receiveByte = new Byte[1024];
                ns.Read(receiveByte, 0, receiveByte.Length);
                receive_msg = Encoding.Default.GetString(receiveByte).Trim('\0');
                receiveByte = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(receive_msg); //받은 바이트를 문자열로 하여 null 값을 지우고 다시 바이트 형식 변환
                Console.WriteLine(receive_msg);
            }
        }
        
        
    }
}
