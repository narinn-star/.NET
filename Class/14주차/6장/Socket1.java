import java.net.*;
import java.io.*;

public class Socket1 {
	public static void main(String[] args) throws UnknownHostException {		
		InetAddress addr = InetAddress.getByName("naver.com");		//DNS 서버
		//InetAddress addr = InetAddress.getByName("203.241.228.129");	//IP

		Socket socket = null;
		try {
			socket = new Socket(addr, 80);			//80번 포트로 연결하는 소켓 생성 -> TCP 소켓 (자바에서 Socket 쓰면 바로 TCP, DataSocket 쓰면 UDP)

			BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			OutputStream out = socket.getOutputStream();

			//out.write(("GET /index.html HTTP/1.0"+"\r\n\n").getBytes()); 	//naver
			out.write(("GET / HTTP/1.0"+"\r\n\n").getBytes());		// send를 write로 사용
			out.flush();

			String line;
			while((line = in.readLine()) != null) {
				System.out.println(line);
			}
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
