import java.io.*;
import java.net.*;

public class Address {
	static InetAddress[] addrList = null;

	public static void main(String[] args) {
		if(args.length != 1) {
			System.err.println("Usage: java Address [IP / DomainName]");
			System.exit(1);
		}

		try {
			addrList = InetAddress.getAllByName(args[0]);
		} catch(UnknownHostException uhe) {
			System.err.println("Invalid Address");
			System.exit(2);
		}

		System.out.println("Name: " + addrList[0].getHostName());
		for(int i=0 ; i<addrList.length ; i++) {
			System.out.println(" IP : " + addrList[i].getHostAddress());
		}
	}
}
