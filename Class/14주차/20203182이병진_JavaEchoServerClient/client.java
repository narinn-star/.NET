import java.io.*;
import java.net.*;
import java.util.Scanner;

public class client {
	public static void main(String[] args) {
		System.out.println("Test Client Program...");

		Socket socket = null;
		try {
			socket = new Socket("localhost", 9000);
			Scanner in = new Scanner(System.in);
			
			BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));					
			BufferedReader reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			
			while(true){
				String _readed = in.nextLine();			
				writer.write(_readed + '\n');					
				writer.flush();
				String line = reader.readLine();
				System.out.println(line);
			}
		} catch(IOException ioe) {
			System.err.println("Exception generated...");
		} finally {
			try {
				socket.close();
			} catch(Exception ignored) {}
		}
	}
}

