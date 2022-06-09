import java.io.*;
import java.net.*;

public class TestServer3 {
	public static void main(String[] args) {
		System.out.println("Test Server Program...");

		ServerSocket server = null;
		try {
			server = new ServerSocket(9000);		//포트 9000번 TCPListener처럼 ServerSocket 사용

			while(true) {
				Socket client = server.accept();		//accept 사용

				System.out.println("A Client Connected...");

				Handler handler = new Handler(client);	//Handler : Thread
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
			//while문 들어면 자바 버전의 Echo 서버가 됨.
			BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
			writer.write("Welcome to the TestServer...");
			writer.flush();

		} catch(IOException ignored) {
		} finally {
			try {
				socket.close();
			} catch(IOException ignored) {}
		}
	}
}


