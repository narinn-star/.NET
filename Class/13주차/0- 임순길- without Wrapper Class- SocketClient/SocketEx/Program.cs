using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
namespace SocketEx
{
  class Program
  {
    static void Main(string[] args)
    {
      // ���� ���� 
      Socket sckTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      //Dns ���� �˾ƺ� Resolve()�޼ҵ带 ����Ͽ� Host�� ���� ������ �����ɴϴ�
      //IPHostEntry ipAddr = Dns.GetHostByName("127.0.0.1");
      // ������ �ּ��� ù��° �����ǿ� 80�� ��Ʈ�� ����� ���� ����
	  IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
      try
      {
		// ������ ������ ������ ������ �õ��մϴ�.
        sckTcp.Connect(ipEndPoint);
				// ������ ����Ǿ��ٸ� 
        if (sckTcp.Connected)
        { 
          Console.WriteLine("������ ����Ǿ����ϴ�. ");
          while (true)
          {
              // ������ ���ڷ� Http ���������� ����� ����� ������ 
              // socketSendReceive �޼ҵ带 ȣ���մϴ�.
              string str = Console.ReadLine();
              // �ѱ� �Էµ� ������ �ֵ��� ���ڵ� �մϴ�.
              Byte[] ByteGet = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(str);
              // ������ ���� ����Ʈ �迭�� �����մϴ�.
              Byte[] RecvBytes = new Byte[1024];
              // �������� ��û ����� �����ϴ�..
              sckTcp.Send(ByteGet);
              // ���� Ȩ�������� ������ �޽��ϴ�.
              Int32 bytes = sckTcp.Receive(RecvBytes, RecvBytes.Length, SocketFlags.None);
              // ���� ���� �ҽ��� ������ ����մϴ�.
              string receive = Encoding.GetEncoding("ks_c_5601-1987").GetString(RecvBytes, 0, bytes); 
              Console.WriteLine(receive);
          }
        }
        else
        {
		  // ������� �ʾҴٸ�
          Console.WriteLine("������ ������ ������� �ʾҽ��ϴ�. ");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception Thrown: " + ex.ToString());
      }
      finally
      {
        sckTcp.Close();
      }
    }
  }// end Class 
}// end Namespace
