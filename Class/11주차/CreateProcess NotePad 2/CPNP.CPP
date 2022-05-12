#define UNICODE
#define _UNICODE

#include <windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND,UINT,WPARAM,LPARAM);
HINSTANCE g_hInst;
HWND hWndMain;
LPCTSTR lpszClass=TEXT("Class");

int APIENTRY _tWinMain(HINSTANCE hInstance,HINSTANCE hPrevInstance
	  ,PTSTR lpszCmdParam,int nCmdShow)
{
	HWND hWnd;
	MSG Message;
	WNDCLASS WndClass;
	g_hInst=hInstance;
	
	WndClass.cbClsExtra=0;
	WndClass.cbWndExtra=0;
	WndClass.hbrBackground=(HBRUSH)GetStockObject(WHITE_BRUSH);
	WndClass.hCursor=LoadCursor(NULL,IDC_ARROW);
	WndClass.hIcon=LoadIcon(NULL,IDI_APPLICATION);
	WndClass.hInstance=hInstance;
	WndClass.lpfnWndProc=(WNDPROC)WndProc;
	WndClass.lpszClassName=lpszClass;
	WndClass.lpszMenuName=NULL;
	WndClass.style=CS_HREDRAW | CS_VREDRAW;
	RegisterClass(&WndClass);

	hWnd=CreateWindow(lpszClass,lpszClass,WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,CW_USEDEFAULT,
		NULL,(HMENU)NULL,hInstance,NULL);
	ShowWindow(hWnd,nCmdShow);
	hWndMain=hWnd;
	
	while(GetMessage(&Message,0,0,0)) {
		TranslateMessage(&Message);
		DispatchMessage(&Message);
	}
	return Message.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd,UINT iMessage,WPARAM wParam,LPARAM lParam)
{
	HDC hdc;
	PAINTSTRUCT ps;
	STARTUPINFO si={ sizeof(si) };
//	TCHAR szCommandLine[] = TEXT("NotePad");				// 제목없음의 메모장 하나가 실행됨
	TCHAR szCommandLine[]=TEXT("NotePad a.txt");			// a.txt가 없으면 새로 만들겠습니까? 라는 메시지가 뜨고, 있으면 내용이 열어짐
//	TCHAR *szCommandLine=TEXT("NotePad");					// 실행 안되는디

	switch(iMessage) {
	case WM_CREATE:
		return 0;
	
	case WM_LBUTTONDOWN:
		PROCESS_INFORMATION pi;
		CreateProcess(NULL, szCommandLine, NULL,NULL,FALSE,0,NULL,NULL,&si, &pi);
		//CreateProcess(NULL, TEXT("NOTEPAD"),NULL,NULL,FALSE,0,NULL,NULL,&si, &pi);
		return 0;
	case WM_PAINT:
		hdc=BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
		return 0;
	case WM_DESTROY:
		PostQuitMessage(0);
		return 0;
	}
	return(DefWindowProc(hWnd,iMessage,wParam,lParam));
}