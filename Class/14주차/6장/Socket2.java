import java.net.*;
import java.io.*;

public class Socket2 {
	public static void main(String[] args) {
		Socket socket = null;
		try {
			socket = new Socket("www.naver.com", 80);

			System.out.println("���� �ּ� : " + socket.getInetAddress().getHostName());
			System.out.println("���� ��Ʈ : " + socket.getPort());
			System.out.println("Ŭ���̾�Ʈ �ּ� : " + socket.getLocalAddress().getHostName());
			System.out.println("Ŭ���̾�Ʈ ��Ʈ : " + socket.getLocalPort());
		} catch(IOException ioe) {
			System.err.println("I/O Exception generated...");
			System.exit(1);
		} finally {
			try {
				socket.close();
			} catch(IOException ignored) {}
		}
	}
}
