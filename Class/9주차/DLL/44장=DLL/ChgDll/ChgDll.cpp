#include <windows.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("ChgDll");

int APIENTRY WinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	  ,LPSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;

	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)(COLOR_WINDOW+1);
	WndClass.hCursor=LoadCursor(NULL,IDC_ARROW);
	WndClass.hIcon=LoadIcon(NULL,IDI_APPLICATION);
	WndClass.hInstance=hInstance;
	WndClass.lpfnWndProc=WndProc;
	WndClass.lpszClassName=lpszClass;
	WndClass.lpszMenuName=NULL;
	WndClass.style=CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd=CreateWindow(lpszClass,lpszClass,WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,
		NULL,(HMENU)NULL,hInstance,NULL);
	ShowWindow(hWnd,nCmdShow);

	while (GetMessage(&Message,NULL,0,0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return (int)Message.wParam;
}

HINSTANCE hInst;
TCHAR str[256]="���� ����� �ٲ� ���� ���� ��ư�� �����ʽÿ�";
TCHAR Method[128]="���ϱ�";
TCHAR DllName[MAX_PATH];
LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc;
	int (*pFunc)(int,int);
	int result;
	switch (iMessage) {
	case WM_CREATE:
		lstrcpy(DllName, "AddDll");
		return 0;
	case WM_LBUTTONDOWN:
		if (lstrcmp(DllName, "AddDll") == 0) {
			lstrcpy(DllName, "MultDll");
			lstrcpy(Method,"���ϱ�");
		} else {
			lstrcpy(DllName, "AddDll");
			lstrcpy(Method,"���ϱ�");
		}
		hInst=LoadLibrary(DllName);
		if (hInst == NULL) return 0;
		pFunc=(int(*)(int,int))GetProcAddress(hInst, "CalcInteger");
		result=(*pFunc)(3,4);
		wsprintf(str, "The Result of %s is %d", DllName, result);
		InvalidateRect(hWnd, NULL, TRUE);
		FreeLibrary(hInst);
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		TextOut(hdc, 50,30,Method,lstrlen(Method));
		TextOut(hdc, 50,50,str,lstrlen(str));
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}

