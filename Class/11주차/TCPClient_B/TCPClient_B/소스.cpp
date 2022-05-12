#include <winsock2.h>
#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include <time.h>

#define BUFSIZE 512

SOCKET sock = socket(AF_INET, SOCK_STREAM, 0);

bool connect(char *host, int port, int timeout)

{

	TIMEVAL Timeout;

	Timeout.tv_sec = 0;

	Timeout.tv_usec = timeout;

	struct sockaddr_in address;  /* the libc network address data structure */



	sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);



	address.sin_addr.s_addr = inet_addr(host); /* assign the address */

	address.sin_port = htons(port);            /* translate int2port num */

	address.sin_family = AF_INET;



	//set the socket in non-blocking

	unsigned long iMode = 1;

	int iResult = ioctlsocket(sock, FIONBIO, &iMode);

	if (iResult != NO_ERROR)

	{

		printf("ioctlsocket failed with error: %ld\n", iResult);

	}



	if (connect(sock, (struct sockaddr *)&address, sizeof(address)) == false)

	{

		return false;

	}



	// restart the socket mode

	iMode = 0;

	iResult = ioctlsocket(sock, FIONBIO, &iMode);

	if (iResult != NO_ERROR)

	{

		printf("ioctlsocket failed with error: %ld\n", iResult);

	}



	fd_set Write, Err;

	FD_ZERO(&Write);

	FD_ZERO(&Err);

	FD_SET(sock, &Write);

	FD_SET(sock, &Err);



	// check if the socket is ready

	select(0, NULL, &Write, &Err, &Timeout);

	if (FD_ISSET(sock, &Write))

	{

		return true;

	}



	return false;

}

// 소켓 함수 오류 출력 후 종료
void err_quit(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, (LPCTSTR)lpMsgBuf, NULL, MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 소켓 함수 오류 출력
void err_display(char *msg)
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	printf("[%s] %s", msg, (LPCTSTR)lpMsgBuf);
	LocalFree(lpMsgBuf);
}

//IP & PORT Input Error
void err_ip_port()
{
	LPVOID lpMsgBuf;
	FormatMessage(
		FORMAT_MESSAGE_ALLOCATE_BUFFER |
		FORMAT_MESSAGE_FROM_SYSTEM,
		NULL, WSAGetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
		(LPTSTR)&lpMsgBuf, 0, NULL);
	MessageBox(NULL, TEXT("Input IP or Port"), TEXT("Parameter Error"), MB_ICONERROR);
	LocalFree(lpMsgBuf);
	exit(-1);
}

// 사용자 정의 데이터 수신 함수
int recvn(SOCKET s, char *buf, int len, int flags)
{
	int received;
	char *ptr = buf;
	int left = len;

	while (left > 0){
		received = recv(s, ptr, left, flags);
		if (received == SOCKET_ERROR)
			return SOCKET_ERROR;
		else if (received == 0)
			break;
		left -= received;
		ptr += received;
	}

	return (len - left);
}

int main(int argc, char* argv[])
{
	if (argc != 4) err_ip_port();

	int start_port = atoi(argv[2]), end_port = atoi(argv[3]);

	int retval;

	// 윈속 초기화
	WSADATA wsa;
	if (WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	//Checking
	for (int i = start_port; i <= end_port; i++){
		if (connect(argv[1], i, 1)) printf("%d : alive\n", i);
		else printf("%d : not alive\n", i);
	}

	// 윈속 종료
	WSACleanup();
	return 0;
}