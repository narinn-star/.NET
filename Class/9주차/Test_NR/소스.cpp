// stdio.h �ҽ� �� ����־ ����� ���������� �̷����.

//#include <stdio.h>

extern "C"  int __cdecl printf(const char *, ...);


int sum(int, int);	// �Լ� ����

int main(){
	int a = 2, b = 5;
	int c;

	c = sum(a, b);
	printf("%d\n", c);
}

///---------------------------

int sum(int a, int b){	// sum �Լ� ����
	return a + b;
}