import java.net.*;
import java.io.*;

public class Socket2 {
	public static void main(String[] args) {
		Socket socket = null;
		try {
			socket = new Socket("www.naver.com", 80);

			System.out.println("서버 주소 : " + socket.getInetAddress().getHostName());
			System.out.println("서버 포트 : " + socket.getPort());
			System.out.println("클라이언트 주소 : " + socket.getLocalAddress().getHostName());
			System.out.println("클라이언트 포트 : " + socket.getLocalPort());
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
