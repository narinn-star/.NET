import java.io.*;
import java.net.*;
import java.util.*;

public class PSScanner {
	protected static int[] ports = {0, 1, 2, 3, 23, 52};
	protected static PrintWriter log;

	public static void main(String[] args) throws IOException {
		System.out.println("Port Scanner Monitoring Program...");

		log = new PrintWriter(new FileWriter("PSLog.txt"));

		for(int i=0 ; i<ports.length ; i++){
			Scanner scanner = new Scanner(i, ports[i], log);
			scanner.start();
		}
	}
}

class Scanner extends Thread {
	protected int port;
	protected PrintWriter log;

	public Scanner(int id, int port, PrintWriter log) {
		super("PS Scanner #" + id);

		this.port = port;
		this.log = log;
	}
	
	public void run() {
		while(true) {
			try {
				ServerSocket server = new ServerSocket(port);

				Socket client = server.accept();

				log.println("PORT[" + port + "]\t\t" + 
					client.getInetAddress().getHostName() + ":" +
					client.getPort() + "-" +
					new Date().toString());
				log.flush();

				try {
					client.close();
				} catch(Exception ignored) {}
			} catch(IOException alsoIgnored) {}
		}
	}
}
