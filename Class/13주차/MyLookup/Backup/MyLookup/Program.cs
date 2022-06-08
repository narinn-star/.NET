using System;
using System.Net;

namespace MyLookup
{
  class Program
  {
    static void Main(string[] args)
    {
      // �����θ��� ���� ������ �����մϴ�.
      string strHostName = "";

      try
      {
        // ���ڷ� �����θ��� �ִ����� �Ǻ��մϴ�.
        if (args.Length > 0)
        {
          // ���ڷ� �־��� �����θ��� ������ �Ҵ��մϴ�.
          strHostName = args[0];
        }
        else
        {
          // �ȳ� �޽����� ����Ͽ� �����θ��� �Է��ϵ��� �մϴ�.
          Console.WriteLine("�����θ��� �Է��ϼ���");
          Console.Write(">");

          // �Էµ� �����θ��� ������ �Ҵ��մϴ�.
          strHostName = Console.ReadLine();
        }
        Console.WriteLine();

        // Dns�� �޼ҵ带 �̿��Ͽ� IPHostEntry ��ü�� ������ ����ϴ�.
        IPHostEntry hostInfo = Dns.GetHostByName(strHostName);

        // ȣ��Ʈ�� ���� ������ ���� ����մϴ�. 
        Console.WriteLine("1. Real Name");
        Console.WriteLine(hostInfo.HostName);
        Console.WriteLine();

        // �������� ������ �ִ� IP ����Ʈ�� ����մϴ�.
        Console.WriteLine("2. IP Addresses");
        foreach (IPAddress ip in hostInfo.AddressList)
        {
          Console.WriteLine(ip.ToString());
        }
        Console.WriteLine();

        // �ش� �������� ��Ī ����Ʈ�� ����մϴ�
        Console.WriteLine("3. Aliases ");
        foreach (string alise in hostInfo.Aliases)
        {
          Console.WriteLine(alise);
        }

      }
      catch (Exception ex)
      {
        Console.WriteLine("Exception caught!!!");
        Console.WriteLine("Source : " + ex.Source);
        Console.WriteLine("Message : " + ex.Message);
      }
    }
  }
}
