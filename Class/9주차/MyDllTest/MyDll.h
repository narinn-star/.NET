#ifdef DLLEXPORT
#define MYDLLTYPE __declspec(dllexport)
#else
#define MYDLLTYPE __declspec(dllimport)
#endif

// �� ���� ������ ���Ѵ�.
extern "C" MYDLLTYPE int AddInteger(int a, int b);
