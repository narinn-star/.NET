#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>

#define BUFSIZE 512

// 소켓 함수 오류 출력 후 종료
void err_quit(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER|
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, msg, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 소켓 함수 오류 출력
void err_display(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER|
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

int main(int argc, char* argv[])
{
	int retval;

	// 윈속 초기화
	WSADATA wsa;
	if(WSAStartup(MAKEWORD(2,2), &wsa) != 0)
		return -1;

	// socket()
	SOCKET listen_sock = socket(AF_INET, SOCK_STREAM, 0);
	if(listen_sock == INVALID_SOCKET) err_quit("socket()");	
	
	// bind()
	SOCKADDR_IN serveraddr;
	ZeroMemory(&serveraddr, sizeof(serveraddr));
	serveraddr.sin_family = AF_INET;
	serveraddr.sin_port = htons(9001);//* port 번호
	serveraddr.sin_addr.s_addr = htonl(INADDR_ANY);//* 누구로부터
	retval = bind(listen_sock, (SOCKADDR *)&serveraddr, sizeof(serveraddr));
	if(retval == SOCKET_ERROR) err_quit("bind()");
	
	// listen()
	retval = listen(listen_sock, SOMAXCONN);//* 전화오길 기다림
	if(retval == SOCKET_ERROR) err_quit("listen()");

	// 데이터 통신에 사용할 변수
	SOCKET client_sock;//* 받는 전화기 하나 만든거임 , 이 받는 전화기는 계속 누군가 전화오기를 기다림 이 전화는 다른애들도 전화 받아야 하니까 고정적으로 기다리고있음
	SOCKADDR_IN clientaddr;
	int addrlen;
	char buf[BUFSIZE+1];

	while(1){//* 사용자가 접속하면 while문 안으로 들어감
		// accept()
		addrlen = sizeof(clientaddr);
		client_sock = accept(listen_sock, (SOCKADDR *)&clientaddr, &addrlen);//* 들어오면 accept 안으로 들어가서 함수가 실행됨, 여기서 또 새 전화기 하나 만들어서 전화함
		if(client_sock == INVALID_SOCKET){
			err_display("accept()");
			continue;
		}
		printf("\n[TCP 서버] 클라이언트 접속: IP 주소=%s, 포트 번호=%d\n", 
			inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));

		// 클라이언트와 데이터 통신
		while(1){//* 여기 서버는 누구 한명이 접속을 하면 while문에서 계속 갇혀있음. 근데 들어와있는 사람이 다시 나가면 위에 있는 while문 내부에 있는 client_sock으로 다시 가서 block되어있고 새로운 
			     //* 사용자가 들어오면 다시 client_sock이라는 다음 사용자만을 위한 휴대폰을 하나 더 만듬
			// 데이터 받기
			retval = recv(client_sock, buf, BUFSIZE, 0);//* 여기서 recv 함수를 호출하고 event를 기다리면서 blocked 되어있음
			if(retval == SOCKET_ERROR){
				err_display("recv()");
				break;
			}
			else if(retval == 0)
				break;

			// 받은 데이터 출력
			buf[retval] = '\0';
			printf("[TCP/%s:%d] %s\n", inet_ntoa(clientaddr.sin_addr),
				ntohs(clientaddr.sin_port), buf);

			// 데이터 보내기
			retval = send(client_sock, _strupr(buf), retval, 0);//* server는 받은 데이터를 그대로 다시 client 에게 보여주고 다시 위로 recv로 가서 또 block된상태로 기다린다(82번째줄)
			if(retval == SOCKET_ERROR){
				err_display("send()");
				break;
			}
		}

		// closesocket()
		closesocket(client_sock);
		printf("[TCP 서버] 클라이언트 종료: IP 주소=%s, 포트 번호=%d\n", 
			inet_ntoa(clientaddr.sin_addr), ntohs(clientaddr.sin_port));
	}

	// closesocket()
	closesocket(listen_sock);

	// 윈속 종료
	WSACleanup();
	return 0;
}