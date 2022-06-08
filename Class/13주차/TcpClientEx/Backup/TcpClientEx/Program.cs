using System;
using System.Text;
using System.Net.Sockets;

namespace TcpClientEx
{
  class Program
  {
    // �⺻ ����Ʈ�� microsoft �� �����մϴ�.
    static string _strHostName = "www.microsoft.com";
    // �����¤� 
    static string _strPath = "/";

    static void Main(string[] args)
    {

      // ���� ���� 
      // Socket sckTcp = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
      // IPHostEntry ipEntry = Dns.Resolve(_strHostName);
      // IPEndPoint ipEndPoint = new IPEndPoint(ipEntry.AddressList[0], 80);
      TcpClient tclient = new TcpClient(_strHostName, 80);

      try
      {
        // TcpClient �� �����ϸ鼭 ����Ǿ����Ƿ� ����κ��� �ʿ䰡 ���������ϴ�.
        //sckTcp.Connect( ipEndPoint );

        // ������ ����Ǿ��ٸ�  
        if (tclient.Connected)
        { 
          Console.WriteLine("������ ����Ǿ����ϴ�. ");
          string strContent = socketSendReceive(tclient);
          Console.WriteLine(strContent);
        }
        else
        {
          Console.WriteLine("������ ������ ������� �ʾҽ��ϴ�. ");
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception Thrown: " + ex.ToString());
      }
      finally
      {
        tclient.Close();
      }

    }

    // ������ ��û�޼ҵ� Get �޼ҵ� ���ڿ��� �����Ͽ� ���� �������� ������ ��û�ϰ�
    // ���� ���� �޼����� �������� ������ �����ش�. 
    private static string socketSendReceive(TcpClient s)
    {
      NetworkStream ns = s.GetStream();

      // �������� ���� �޼ҵ� ���ڿ��� �����մϴ�. 
      Encoding ASCII = Encoding.ASCII;
      string Get = "GET " + _strPath + " HTTP/1.1\r\n Host: " + _strHostName + "\r\nConnection: Close\r\n\r\n";
      Byte[] ByteGet = ASCII.GetBytes(Get);

      Byte[] RecvBytes = new Byte[1024];
      String strRetPage = null;

      if (s == null)
        return ("Connection failed");

      // �������� ��û ����� �����ϴ�..
      //s.Send(ByteGet);  
      ns.Write(ByteGet, 0, ByteGet.Length);


      // ���� Ȩ�������� ������ �޽��ϴ�.
      // Int32 bytes = s.Receive(RecvBytes, RecvBytes.Length, SocketFlags.None);
      int bytes = ns.Read(RecvBytes, 0, RecvBytes.Length);

      // ó�� 1024 bytes �� �н��ϴ�.
      strRetPage = "Default HTML page on " + _strHostName + ":\r\n";
      strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);

      while (bytes > 0)
      {
        //bytes = s.Receive(RecvBytes, RecvBytes.Length, 0);
        bytes = ns.Read(RecvBytes, 0, RecvBytes.Length);
        strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);
      }

      return strRetPage;
    }

  }
}
