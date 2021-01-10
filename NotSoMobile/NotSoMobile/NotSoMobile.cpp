#include <iostream>
#include <sstream>
#include <string>
#include <list>
using namespace std;

int GetWeight();
void Case();
bool CheckEquil(int wl, int dl, int wr, int dr);

int main() {
	string input;
	int amountOfCases, value;
	int caseCounter = 0;
	bool isEquil = true;

	getline(cin, input); //Läser av första raden (Hur många cases)
	amountOfCases = stoi(input);
	
	while (getline(cin, input)) {
		if (input == "") {
			caseCounter++;
			if (caseCounter > amountOfCases) {
				break;
			} else {
				if (caseCounter != 1) {
					cout << endl;
				}
				Case();
			}
		}
	}
}

void Case() {
	int run = GetWeight();

	if (run == -1) {
		cout << "NO" << endl;
	} else {
		cout << "YES" << endl;
	}
}

int GetWeight() {
	string input;
	int wl, dl, wr, dr;
	int totalWeight = 0;

	bool isEquil;

	getline(cin, input);

	stringstream ss(input);
	ss >> wl;
	ss >> dl;
	ss >> wr;
	ss >> dr;

	if (wl == 0) {
		wl = GetWeight();//Kolla nästa input?

		if (wl == -1) {
			return -1;
		}
	}
	
	if (wr == 0) {
		wr = GetWeight();

		if (wr == -1) {
			return -1;
		}
	}

	isEquil = CheckEquil(wl, dl, wr, dr);

	if (isEquil) {
		totalWeight = wl + wr;
		return totalWeight;
	} else {
		return (-1);
	}
}

bool CheckEquil(int wl, int dl, int wr, int dr) {
	if (wl * dl == wr * dr) {
		return true;
	} else {
		return false;
	}
}
