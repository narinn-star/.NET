//* ���� ������Ʈ�ϱ�
extern "C" __declspec(dllexport) int AddInteger(int a, int b)
{
	return a+b;
}
//*/

/* ��� ���Ϸ� ������Ʈ�ϱ�
#define DLLEXPORT
#include "MyDll.h"

extern "C" MYDLLTYPE int AddInteger(int a, int b)
{
	return a+b;
}
//*/
