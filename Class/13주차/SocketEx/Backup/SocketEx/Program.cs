using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace SocketEx
{
  class Program
  {
	// �⺻������ ����� ������ �����մϴ�
    static string _strHostName = "www.microsoft.com";
    static string _strPath = "/";

    static void Main(string[] args)
    {

      // ���� ���� 
      Socket sckTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      //Dns ���� �˾ƺ� Resolve()�޼ҵ带 ����Ͽ� Host�� ���� ������ �����ɴϴ�
	  IPHostEntry ipEntry = Dns.Resolve(_strHostName);
      // ������ �ּ��� ù��° �����ǿ� 80�� ��Ʈ�� ����� ���� ����
	  IPEndPoint ipEndPoint = new IPEndPoint(ipEntry.AddressList[0], 80);

      try
      {
		// ������ ������ ������ ������ �õ��մϴ�.
        sckTcp.Connect(ipEndPoint);
		
		// ������ ����Ǿ��ٸ� 
        if (sckTcp.Connected)
        { 
          Console.WriteLine("������ ����Ǿ����ϴ�. ");
          
		  // ������ ���ڷ� Http ���������� ����� ����� ������ 
		  // socketSendReceive �޼ҵ带 ȣ���մϴ�.
		  string strContent = socketSendReceive(sckTcp);
          
		  // ���� ���� �ҽ��� ������ ����մϴ�.
		  Console.WriteLine(strContent);
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

    // ������ ��û�޼ҵ� Get �޼ҵ� ���ڿ��� �����Ͽ� ���� �������� ������ ��û�ϰ�
    // ���� ���� �޼����� �������� ������ �����ش�. 
    private static string socketSendReceive(Socket s)
    {
      // �������� ���� �޼ҵ� ���ڿ��� �����մϴ�. 
      string Get = "GET " + _strPath + " HTTP/1.1\r\n Host: " + _strHostName + "\r\nConnection: Close\r\n\r\n";
      
	  // ASCII ���ڵ��� ����Ͽ� Byte �迭�� ��ȯ �մϴ�.
	  Encoding ASCII = Encoding.ASCII;
	  Byte[] ByteGet = ASCII.GetBytes(Get);

	  // ������ ���� ����Ʈ �迭�� �����մϴ�.
      Byte[] RecvBytes = new Byte[1024];
      String strRetPage = null;

      if (s == null)
        return ("Connection failed");

      // �������� ��û ����� �����ϴ�..
      s.Send(ByteGet);

      // ���� Ȩ�������� ������ �޽��ϴ�.
      Int32 bytes = s.Receive(RecvBytes, RecvBytes.Length, SocketFlags.None);

      // ó�� 1024 bytes �� �н��ϴ�.
      strRetPage = "Default HTML page on " + _strHostName + ":\r\n";
      strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);

	  // ó�� ������ �ִ��� Ȯ�� �� �� ���� ������ ���������� �ݺ������� ����
      while (bytes > 0)
      {
        bytes = s.Receive(RecvBytes, RecvBytes.Length, 0);
        strRetPage = strRetPage + ASCII.GetString(RecvBytes, 0, bytes);
      }

      return strRetPage;
    }// end socketSendReceive
  }// end Class 
}// end Namespace
