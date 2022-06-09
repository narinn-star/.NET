import java.io.*;
import java.net.*;

public class TestServer1 {
	public static void main(String[] args) {
		System.out.println("Test Server Program...");

		ServerSocket server = null;
		try {
			server = new ServerSocket(5264);

			Socket client = server.accept();

			System.out.println("A Client Connected...");

			BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(client.getOutputStream()));
			writer.write("Welcome to the TestServer...");
			writer.flush();

			try {
				client.close();
			} catch(IOException ignored) {}
		} catch(IOException ioe) {
			System.err.println("Exception generated...");
		} finally {
			try {
				server.close();
			} catch(IOException ignored) {}
		}
	}
}
