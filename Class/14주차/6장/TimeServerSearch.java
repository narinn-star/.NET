import java.net.*;

public class TimeServerSearch {
	public static void main(String[] args) {
		System.out.println("Time Server Searching Tool...");
		System.out.println(" (to exit, press CTRL-C)");

		for(int i=0 ; i<100 ; i++) {
			Worker w = new Worker();
			w.start();
		}
	}
}

class Worker extends Thread {
	public void run() {

		Socket socket = null;
		String server = null;

		while(true) {

			try {
				int a = (int)(Math.random() * 255);
				int b = (int)(Math.random() * 255);
				int c = (int)(Math.random() * 255);
				int d = (int)(Math.random() * 255);

				server =
					String.valueOf(a) + "." +
					String.valueOf(b) + "." +
					String.valueOf(c) + "." +
					String.valueOf(d);

				socket = new Socket(server, 37);

				System.out.println("Server " + server + " \t[OPEN] -" + this.getName());
			} catch(Exception failure) {
				System.out.println("Server " + server + " \t[CLOSED] -" + this.getName());
			} finally {
				try {
					socket.close();
				} catch(Exception ignored) {}
			}
		}
	}
}
