import java.io.*;
import java.net.*;
import java.util.*;

public class FileManager {
	protected static final int PORT = 5264;

	protected static BufferedReader netIn = null;
	protected static PrintWriter netOut = null;
	protected static BufferedReader conIn = null;
	protected static PrintWriter conOut = null;

	protected static final String EOR = "END_OF_RESPONSE";

	protected static File currentDir = null;

	public static void main(String[] args) {
		if(args.length < 1) {
			System.err.println("Syntax: java FileManager" +
				" [server/client] [server address]");
			System.exit(1);
		}

		if(args[0].equals("server")) {
			fmServer();
		} else if((args.length > 1) && (args[0].equals("client"))) {
			fmClient(args[1]);
		} else {
			System.err.println("Invalid Command-line Arguments");
			System.exit(1);
		}
	}

	static void fmServer() {
		System.out.println("File Manager Server Program...");

		ServerSocket server = null;
		try {
			server = new ServerSocket(PORT);
		} catch(IOException ioe) {
			System.err.println("Failed to open server...");
			System.exit(1);
		}

		Socket client = null;
		try {
			client = server.accept();

			System.out.println("Connection from... " +
				client.getInetAddress().getHostName());
		} catch(IOException ioe) {
			System.err.println("Failed to accept client...");
			System.exit(1);
		}

		try {
			netIn = new BufferedReader(new InputStreamReader(
				client.getInputStream()));
			netOut = new PrintWriter(new OutputStreamWriter(
				client.getOutputStream()));
			conIn = new BufferedReader(new InputStreamReader(
				System.in));
			conOut = new PrintWriter(new OutputStreamWriter(
				System.out));
		} catch(IOException ioe) {
			System.err.println("Failed to get stream...");
			System.exit(1);
		}

		netOut.println("Welcome to the FileManager Server...");
		netOut.println(EOR);
		netOut.flush();

		currentDir = new File(System.getProperty("user.dir"));

		String cmdLine;
		while(true) {
			try {
				cmdLine = netIn.readLine();

				processCmd(cmdLine);
			} catch(IOException ioe) {
				System.err.println("Exception in processing Command...");
				System.exit(1);
			}
		}
	}

	static void fmClient(String addr) {
		System.out.println("File Manager Client Program...");

		Socket socket = null;
		try {
			socket = new Socket(addr, PORT);

			System.out.println("Connected to... " +
				socket.getInetAddress().getHostName());
		} catch(IOException ioe) {
			System.err.println("Failed to connect to server...");
			System.exit(1);
		}

		try {
			netIn = new BufferedReader(new InputStreamReader(
				socket.getInputStream()));
			netOut = new PrintWriter(new OutputStreamWriter(
				socket.getOutputStream()));
			conIn = new BufferedReader(new InputStreamReader(
				System.in));
			conOut = new PrintWriter(new OutputStreamWriter(
				System.out));
		} catch(IOException ioe) {
			System.err.println("Failed to get stream...");
			System.exit(1);
		}

		try {
			String line;
			while( !(line = netIn.readLine()).equals(EOR) ) {
				conOut.println(line);
				conOut.flush();
			}

			while(true) {
				conOut.print("COMMAND> ");
				conOut.flush();
				line = conIn.readLine();

				netOut.println(line );
				netOut.flush();

				getResponse();

				conOut.println();
				conOut.flush();
			}
		} catch(IOException ioe) {
			System.err.println("Exception in communication...");
			System.exit(1);
		}
	}

	static void processCmd(String cmdLine) {
		if(cmdLine == null) {
			cmdBadCommand();
			netOut.println(EOR);
			netOut.flush();
			return;
		}

		StringTokenizer tokens = new StringTokenizer(cmdLine.trim());

		String cmd = null;
		String arg1 = null;
		String arg2 = null;
		if(tokens.hasMoreTokens()) {
			cmd = tokens.nextToken();

			if(tokens.hasMoreTokens()) {
				arg1 = tokens.nextToken();
			}
			if(tokens.hasMoreTokens()) {
				arg2 = tokens.nextToken();
			}
		} else {
			cmdBadCommand();
			netOut.println(EOR);
			netOut.flush();
			return;
		}

		if(cmd.equals("help")) {
			cmdHelp();
		} else if((cmd.equals("list")) ||
				cmd.equals("l")) {
			cmdList();
		} else if((cmd.equals("rename")) ||
				cmd.equals("ren")) {
			cmdRename(arg1, arg2);
		} else if((cmd.equals("delete")) ||
				cmd.equals("del")) {
			cmdDelete(arg1);
		} else if((cmd.equals("makedir")) ||
				cmd.equals("md")) {
			cmdMakedir(arg1);
		} else if((cmd.equals("changedir")) ||
				cmd.equals("cd")) {
			cmdChangedir(arg1);
		} else if((cmd.equals("find")) ||
				cmd.equals("f")) {
			cmdFind(arg1);
		} else {
			cmdBadCommand();
		}

		netOut.println(EOR);
		netOut.flush();
	}

	static void cmdHelp() {
		netOut.println("FILE MANAGER COMMAND");
		netOut.println("----------------------------------" );
		netOut.println(" help" );
		netOut.println(" list(l)" );
		netOut.println(" rename(ren) [old name] [new name]" );
		netOut.println(" find(f) [Filename/dir name]" );
		netOut.println(" delete(del) [Filename/dir name]" );
		netOut.println(" makedir(md) [dir name]" );
		netOut.println(" changedir(cd) [dir name]" );
		netOut.println("----------------------------------" );
	}

	static void cmdDelete(String arg) {
		if(arg == null) {
			cmdBadCommand();
			return;
		}

		File file = new File(currentDir, arg);

		if(file.delete()) {
			netOut.println("File " + file.getName() + " deleted..." );
		} else {
			netOut.println("Failed to delete file..." );
		}
	}

	static void cmdRename(String arg1, String arg2) {
		if((arg1 == null) || (arg2 == null)) {
			cmdBadCommand();
			return;
		}

		File file = new File(currentDir, arg1);
		File target = new File(currentDir, arg2);

		if(target.exists()) {
			target.delete();
		}
		file.renameTo(target);

		netOut.println("File " + arg1 + " -> " + arg2 );
	}

	static void cmdList() {
		File[] list = currentDir.listFiles();

		netOut.println("File list in " + currentDir.getName() );
		netOut.println("==================================================" );

		for(int i=0 ; i<list.length ; i++) {
			if(list[i].isFile()) {
				netOut.println(list[i].getName() + "\t\t" + 
				  (list[i].canRead()?"r":"_") + 
				  (list[i].canWrite()?"w":"_") + "  " +
				  list[i].length() + "\t" +
				  (list[i].isHidden()?"hidden":"") );
			} else if(list[i].isDirectory()) {
				netOut.println("[" + list[i].getName() + "]\t\t" +
				  (list[i].canRead()?"r":"_") + 
				  (list[i].canWrite()?"w":"_") );
			}
		}

		netOut.println("==================================================" );
		netOut.println("Total " + list.length + " items listed..." );
	}

	static void cmdMakedir(String arg) {
		if(arg == null) {
			cmdBadCommand();
			return;
		}

		File dir = new File(currentDir, arg);

		if(dir.exists()) {
			// does nothing
		} else {
			dir.mkdir();
		}

		netOut.println("Directory " + arg + " constructed..." );
	}

	static void cmdChangedir(String arg) {
		if(arg == null) {
			cmdBadCommand();
			return;
		}

		File newDir = new File(currentDir, arg);

		if(!(newDir.exists()) || !(newDir.isDirectory())) {
			netOut.println("Invalid Directory..." );
		} else {
			currentDir = newDir;
			netOut.println("Moved to " + arg );
		}
	}

	static void cmdFind(String arg) {
		if(arg == null) {
			cmdBadCommand();
			return;
		}

		File file = new File(arg);

		if(file.exists()) {
			netOut.println("File " + arg + " exists...");
		} else {
			netOut.println("File " + arg + " not found");
		}
	}

	static void cmdBadCommand() {
		netOut.println("Bad Command...");
	}

	static void getResponse() throws IOException {
		String line;

		while(!((line = netIn.readLine()).equals(EOR))) {
			conOut.println(line);
		}
		conOut.flush();
	}
}

