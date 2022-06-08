using System;
using System.Net;

namespace IPAddressEx
{
  class Program
  {
    static void Main(string[] args)
    {
      // 1. �б����� ������ ���� �մϴ�.
      Console.WriteLine("IPAddress �� �б����� ������ ���� �մϴ�.");
      Console.WriteLine("IPAddress.Any : {0}", IPAddress.Any.ToString());
      Console.WriteLine("IPAddress.Broadcast : {0}", IPAddress.Broadcast.ToString());
      Console.WriteLine("IPAddress.Loopback : {0}", IPAddress.Loopback.ToString());
      Console.WriteLine("IPAddress.None : {0}", IPAddress.None.ToString());
      Console.WriteLine();

      // 2. IPAddress �ν��Ͻ� �����ϱ� 
      IPAddress ipAddress = IPAddress.Parse("211.233.62.105");

      // 3. � �ּ�ü�踦 ������ �ִ��� ��Ÿ���ϴ�.
      Console.WriteLine("AddressFamily : {0}", ipAddress.AddressFamily.ToString());

      // 4. Loopback ������ üũ�մϴ�.
      if (IPAddress.IsLoopback(ipAddress))
        Console.WriteLine("{0} is Loopback.", ipAddress.ToString());
      else
        Console.WriteLine("{0} is not Loopback", ipAddress.ToString());
    }
  }
}
