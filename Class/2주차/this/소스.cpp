#include <iostream>

using namespace std;

class A {
public : 
	int num;
public:
    void print_This() {
        cout << "Class A�� this �ּ� : " << this << endl;
    }

	void mul(int a) {
		cout << "num * a = " << this->num * a << endl;
	}

    A* return_A() {
        return this;
    }
};

int main(void) {

    A a, b;  //

	a.num = 2;
	b.num = 5;

	a.mul(3);
	b.mul(5);


    cout << "a�� �ּҰ� : " << &a << endl;  //a instance pointer
    a.print_This();

	cout << "b�� �ּҰ� : " << &b << endl;  //a instance pointer
	b.print_This();

   //cout << "a.return_A() : " << a.return_A() << endl;

    return 0;
}
