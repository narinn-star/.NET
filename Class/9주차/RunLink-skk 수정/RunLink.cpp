#include <windows.h>

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass = TEXT("RunLink");

int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance
	, LPSTR lpszCmdParam, int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst = hInstance;

	WndClass.cbClsExtra = 0;
	WndClass.cbWndExtra = 0;
	WndClass.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	WndClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	WndClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	WndClass.hInstance = hInstance;
	WndClass.lpfnWndProc = WndProc;
	WndClass.lpszClassName = lpszClass;
	WndClass.lpszMenuName = NULL;
	WndClass.style = CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd = CreateWindow(lpszClass, lpszClass, WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
		NULL, (HMENU)NULL, hInstance, NULL);
	ShowWindow(hWnd, nCmdShow);

	while (GetMessage(&Message, NULL, 0, 0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return (int)Message.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT ps;
	HDC hdc;
	static HINSTANCE hInst;
	int(*pFunc)(int, int);
	TCHAR str[128];
	switch (iMessage) {
	case WM_RBUTTONDOWN:
		if ((hInst = LoadLibrary("mydll.dll")) == NULL) // ���� �����.
		{
			MessageBox(hWnd, "�� DLL�� ���׿�", "error", MB_OK);
			return 0;
		}
		else
		{
			MessageBox(hWnd, "OK Loading! ", "OK", MB_OK);
			return 0;
		}
	case WM_LBUTTONDOWN:
		if ((pFunc = (int(*)(int, int))GetProcAddress(hInst, "AddInteger")) == NULL) {
			MessageBox(hWnd, "�Լ��� ��� �ִ�?", "error", MB_OK);
			//	����ó��
			return 0;
		}
		else{
			hdc = GetDC(hWnd);
			wsprintf(str, "5+2 = %d", (*pFunc)(5, 2));
			TextOut(hdc, 10, 10, str, lstrlen(str));
			ReleaseDC(hWnd, hdc);
		}
		if ((pFunc = (int(*)(int, int))GetProcAddress(hInst, "SubInteger")) == NULL) {
			MessageBox(hWnd, "�Լ��� ��� �ִ�?", "error", MB_OK);
			//	����ó��
			return 0;
		}
		else{
			hdc = GetDC(hWnd);
			wsprintf(str, "5-2 = %d", (*pFunc)(5, 2));
			TextOut(hdc, 10, 200, str, lstrlen(str));
			ReleaseDC(hWnd, hdc);
		}
		if ((pFunc = (int(*)(int, int))GetProcAddress(hInst, "MulInteger")) == NULL) {
			MessageBox(hWnd, "�Լ��� ��� �ִ�?", "error", MB_OK);
			//	����ó��
			return 0;
		}
		else{
			hdc = GetDC(hWnd);
			wsprintf(str, "5*2 = %d", (*pFunc)(5, 2));
			TextOut(hdc, 10, 400, str, lstrlen(str));
			ReleaseDC(hWnd, hdc);
		}
		return 0;
	case WM_PAINT:
		hdc = BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		FreeLibrary(hInst);
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd, iMessage, wParam, lParam));
}

