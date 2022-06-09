import java.net.*;

public class InetAddress1 {
	public static void main(String[] args) throws UnknownHostException{
		InetAddress addr1 = InetAddress.getByName("18.29.1.34");
		InetAddress addr2 = InetAddress.getByName("www.drbook.co.kr");
		InetAddress addr3 = InetAddress.getLocalHost();

		System.out.println("addr1 = " + addr1.getHostName());
		System.out.println("addr2 = " + addr2.getHostAddress());
		System.out.println("addr3 = " + addr3.getHostName() + "(" + addr3.getHostAddress() + ")");
	}
}
