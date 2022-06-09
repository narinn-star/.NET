import java.io.*;
import java.net.*;

public class PortScanner extends Thread {
	protected static final int SIZE = 16;

	protected InetAddress target;
	protected int id;

	public PortScanner(InetAddress target, int id) throws IOException {
		super("Port Scanner #" + id);

		this.target = target;
		this.id = id;
	}

	public static void main(String[] args) throws IOException {
		if(args.length != 1) {
			System.err.println("Usage: java PortScan [ip/hostname]");
			System.exit(1);
		}

		InetAddress addr = null;
		try {
			addr = InetAddress.getByName(args[0]);
		} catch(UnknownHostException uhe) {
			System.err.println("Invalid target!!");
			System.err.println(2);
		}

		for(int i=0 ; i<SIZE ; i++) {
			new PortScanner(addr, i).start();
			
		}
	}

	public void run() {
		int port =0;
		Socket s = null;

		for(int i=0 ; i<SIZE ; i++) {
			try {
				port = SIZE * id + i;
				s = new Socket(target, port);
				System.out.println("Port #" + port + " is open");
			} catch(IOException ioe) {
			} finally {
				try {
					s.close();
				} catch(Exception ignored) {
				}
			}
		}
	}
}
