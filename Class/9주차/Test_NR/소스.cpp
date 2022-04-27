// stdio.h 소스 다 끌어넣어도 디버깅 정상적으로 이루어짐.

//#include <stdio.h>

extern "C"  int __cdecl printf(const char *, ...);


int sum(int, int);	// 함수 선언

int main(){
	int a = 2, b = 5;
	int c;

	c = sum(a, b);
	printf("%d\n", c);
}

///---------------------------

int sum(int a, int b){	// sum 함수 정의
	return a + b;
}