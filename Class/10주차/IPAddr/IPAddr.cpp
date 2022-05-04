#include <winsock2.h>
#include <stdio.h>

int main(int argc, char* argv[])
{
	WSADATA wsa;
	if(WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
		return -1;

	// ������ IP �ּ� ���
	char *ipaddr = "147.46.114.70";
	printf("IP �ּ� = %s\n", ipaddr);

	// inet_addr() �Լ� ����
	printf("IP �ּ�(��ȯ ��) = 0x%x\n", inet_addr(ipaddr));

	// inet_ntoa() �Լ� ����
	IN_ADDR temp;
	temp.s_addr = inet_addr(ipaddr);
	printf("IP �ּ�(��ȯ ��) = %s\n", inet_ntoa(temp));

	WSACleanup();
	return 0;
}