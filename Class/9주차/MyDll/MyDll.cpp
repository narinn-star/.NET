
//* ���� ������Ʈ�ϱ�
extern "C" __declspec(dllexport) int AddInteger(int a, int b)
{
	return a + b;
}

// Sub, Mul �߰��ϱ� "dllexport"
extern "C" __declspec(dllexport) int SubInteger(int a, int b)
{
	return a - b;
}

extern "C" __declspec(dllexport) int MulInteger(int a, int b)
{
	return a * b;
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
