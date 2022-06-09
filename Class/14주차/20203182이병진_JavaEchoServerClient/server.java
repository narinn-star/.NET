import java.io.*;
import java.net.*;

public class server {
	public static void main(String[] args) {
		System.out.println("Test Server Program...");

		ServerSocket server = null;		
		try {
			server = new ServerSocket(9000);

			while(true) {
				Socket client = server.accept();

				System.out.println("A Client Connected...");

				Handler handler = new Handler(client);
				handler.start();
			}
		} catch(IOException ioe) {
			System.err.println("Exception generated...");
		} finally {
			try {
				server.close();
			} catch(IOException ignored) {}
		}
	}
}

class Handler extends Thread {
	protected Socket socket;

	public Handler(Socket socket) {
		this.socket = socket;
	}

	public void run() {
		try {
			BufferedReader reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));					

			while(socket.isConnected()){
				String line = reader.readLine();
				if(line != null){
					System.out.println(line);
					writer.write(line + '\n');					
					writer.flush();
				}				
			}
		} catch(IOException ignored) {
		} finally {
			try {
				socket.close();
			} catch(IOException ignored) {}
		}
	}
}


