import java.io.*;
import java.net.*;

public class TestClient1 {
	public static void main(String[] args) {
		System.out.println("Test Client Program...");

		Socket socket = null;
		try {
			socket = new Socket("localhost", 9000);			//소켓으로 9000번 포트 열기

			System.out.println("Connected to Server...");
										// while문 넣으면 자바 버전의 EchoClient 됨
			BufferedReader reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			String line = reader.readLine();				// receive

			System.out.println(line);
		} catch(IOException ioe) {
			System.err.println("Exception generated...");
		} finally {
			try {
				socket.close();
			} catch(Exception ignored) {}
		}
	}
}

