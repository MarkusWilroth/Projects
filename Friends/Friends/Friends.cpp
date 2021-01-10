#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <map>
using namespace std;

int main() { //Behöver gå snabbare!
	string input;
	int cases;
	int n, m; //N är invånare, och m är grupper per case
	//string *friendArr;
	int a, b; //person a och person b;
	

	getline(cin, input); //Får hur många cases det finns
	cases = stoi(input);

	for (int i = 0; i < cases; i++) {
		int mostFriends = 0;
		getline(cin, input); //Får info om case, N (invånare) och M (grupper)
		stringstream ss(input);
		ss >> n;	//Sätter in invånare
		ss >> m;	//Sitter in hur många grupper

		string *friendArr = new string[n+1]; //Håller koll på vilka på allas vänner (+1 eftersom a börjar på 1, finns ingen person 0)

		for (int j = 0; j < m; j++) { //Går igenom varje grupp
			getline(cin, input);		//Kollar gruppens personer (A och B) Fungeras som id för persoerna

			stringstream ss(input); //Borde inte göra om de till in... kan använda char
			ss >> a;
			if (friendArr[a] != "") {
				friendArr[a] += " "; //Fixar mellanrum mellan talen så att de går att skilljas åt
			}
			
			ss >> b;
			if (friendArr[b] != "") {
				friendArr[b] += " ";
			}

			friendArr[a] += to_string(b);
			friendArr[b] += to_string(a);
		}

		//Alla vet vilka de är vänner med
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
						continue; //Hoppar tillbaka till while o kollar nästa värde
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

