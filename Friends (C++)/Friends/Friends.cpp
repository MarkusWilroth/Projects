#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <map>
using namespace std;

int main() { //Beh�ver g� snabbare!
	string input;
	int cases;
	int n, m; //N �r inv�nare, och m �r grupper per case
	//string *friendArr;
	int a, b; //person a och person b;
	

	getline(cin, input); //F�r hur m�nga cases det finns
	cases = stoi(input);

	for (int i = 0; i < cases; i++) {
		int mostFriends = 0;
		getline(cin, input); //F�r info om case, N (inv�nare) och M (grupper)
		stringstream ss(input);
		ss >> n;	//S�tter in inv�nare
		ss >> m;	//Sitter in hur m�nga grupper

		string *friendArr = new string[n+1]; //H�ller koll p� vilka p� allas v�nner (+1 eftersom a b�rjar p� 1, finns ingen person 0)

		for (int j = 0; j < m; j++) { //G�r igenom varje grupp
			getline(cin, input);		//Kollar gruppens personer (A och B) Fungeras som id f�r persoerna

			stringstream ss(input); //Borde inte g�ra om de till in... kan anv�nda char
			ss >> a;
			if (friendArr[a] != "") {
				friendArr[a] += " "; //Fixar mellanrum mellan talen s� att de g�r att skilljas �t
			}
			
			ss >> b;
			if (friendArr[b] != "") {
				friendArr[b] += " ";
			}

			friendArr[a] += to_string(b);
			friendArr[b] += to_string(a);
		}

		//Alla vet vilka de �r v�nner med
		map<int, int> countedFriends;
		map<int, int>::iterator it;

		for (int j = 0; j <= n; j++) {

			it = countedFriends.find(j);
			if (it != countedFriends.end()) {
				continue;
			}

			int amountOfFriends = 0;
			string infriendGroup = friendArr[j] + " ";
			pair<int, int> newCounted(j, 0);
			countedFriends.insert(newCounted);
			amountOfFriends++;

			bool isDone = false;
			int value;

			while (!isDone) {
				stringstream ss(infriendGroup);
				isDone = true;

				while (ss >> value) {
					it = countedFriends.find(value);
					if (it != countedFriends.end()) { //Hittade en med den nyckeln
						continue; //Hoppar tillbaka till while o kollar n�sta v�rde
					} else { //Hittade ingen med den nyckelen
						infriendGroup += friendArr[value] + " ";
						pair<int, int> newCounted(value, 0);
						countedFriends.insert(newCounted);
						amountOfFriends++;
						isDone = false;
						break;
					}
				}
			}

			int friends = amountOfFriends;
			if (friends >= mostFriends) {
				mostFriends = friends;
			}
		}
		cout << mostFriends << endl;
	}
}

//string *AddToList(map<int, int> &countedFriends, string &infriendGroup, int playerToCheck) {
//	int friendId, friendCounter = 0;
//	vector<int> friendToAdd;
//	stringstream ss(friendArr[playerToCheck]);
//
//	map<int, int>::iterator it;
//	while (ss >> friendId) {
//		/*bool canAdd = true;
//		for (int oldFriend : infriendGroup) {
//			if (oldFriend == friendId) {
//				canAdd = false;
//				break;
//			}
//		}
//
//		if (canAdd) {
//			infriendGroup.push_back(friendId);
//		}*/
//		/*it = infriendGroup.find(friendId);
//		if (it == infriendGroup.end()) {
//			pair<int, int> newFriend;
//			newFriend.first = friendId;
//			newFriend.second = 0;
//			infriendGroup.insert(newFriend);
//		}*/
//
//		/*pair<int, int> newFriend;
//		newFriend.first = friendId;
//		newFriend.second = 0;
//		infriendGroup.insert(newFriend);*/
//
//		//friendToAdd.push_back(friendId);
//		/*pair<int, int> newFriend;
//		newFriend.first = friendId;
//		newFriend.second = 0;
//		infriendGroup.insert(newFriend);*/
//	}
//	pair<int, int> newFriend;
//	newFriend.first = playerToCheck;
//	newFriend.second = 0;
//	countedFriends.insert(newFriend);
//
//	map<int, int>::iterator oldIt;
//	for (map<int, int>::iterator it = infriendGroup.begin(); it != infriendGroup.end(); ++it) {
//		bool isChecked = false;
//
//		oldIt = countedFriends.find(it->first);
//		if (oldIt != countedFriends.end()) { //Hittade en med den nyckeln
//			isChecked = true;
//		}
//
//		/*for (int checkedFriend : countedFriends) {
//			if (it->first == checkedFriend) {
//				isChecked = true;
//				break;
//			}
//		}*/
//		if (!isChecked) {
//			countedFriends = *AddToList(countedFriends, infriendGroup, friendArr, it->first);
//			break;
//		}
//	}
//
//	//cout << "Test: " << countedFriends.size();
//	if (friendCounter == 0) {
//		friendCounter = infriendGroup.size();
//	}
//	return &countedFriends;
//}

