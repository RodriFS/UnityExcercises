using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// Game configuration data
	private const string menuHint = "You may type menu at any time.";
	private string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
	private string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
	private string[] level3Passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };

	// Game state
	private int level;
	private enum Screen { MainMenu, Password, Win };
 private Screen currentScreen = Screen.MainMenu;
 private string password;

 // Use this for initialization
 void Start () {
 ShowMainMenu ();
	}

	void ShowMainMenu () {
		Terminal.ClearScreen ();
		Terminal.WriteLine ("What would you like to hack into?");
		Terminal.WriteLine ("");
		Terminal.WriteLine ("Press 1 for the local library");
		Terminal.WriteLine ("Press 2 for the police station");
		Terminal.WriteLine ("Press 3 for the NASA!");
		Terminal.WriteLine ("Enter your selection:");
	}

	void OnUserInput (string input) {
		string normalizedInput = input.ToLower ();
		if (normalizedInput == "menu") {
			currentScreen = Screen.MainMenu;
			ShowMainMenu ();
		} else if (normalizedInput == "quit" || normalizedInput == "close" || normalizedInput == "exit") {
			Terminal.WriteLine ("If on the web, close the tab!");
			Application.Quit ();
		} else if (currentScreen == Screen.MainMenu) {
			RunMainMenu (normalizedInput);
		} else if (currentScreen == Screen.Password) {
			CheckPassword (normalizedInput);
		}
	}

	void RunMainMenu (string input) {
		bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
		if (isValidLevelNumber) {
			level = int.Parse (input);
			AskForPassword ();
		}
	}

	void AskForPassword () {
		currentScreen = Screen.Password;
		Terminal.ClearScreen ();
		SetRandomPassword ();
		Terminal.WriteLine ("Enter your password, hint:" + password.Anagram ());
		Terminal.WriteLine (menuHint);
	}

	void SetRandomPassword () {
		switch (level) {
			case 1:
				password = level1Passwords[Random.Range (0, level1Passwords.Length)];
				break;
			case 2:
				password = level2Passwords[Random.Range (0, level2Passwords.Length)];
				break;
			case 3:
				password = level3Passwords[Random.Range (0, level3Passwords.Length)];
				break;
			default:
				Terminal.WriteLine ("Please choose a valid level");
				Terminal.WriteLine (menuHint);
				break;
		}
	}

	void CheckPassword (string input) {
		if (input == password) {
			DisplayWinScreen ();
		} else {
			AskForPassword ();
		}
	}

	void DisplayWinScreen () {
		currentScreen = Screen.Win;
		Terminal.ClearScreen ();
		ShowLevelReward ();
		Terminal.WriteLine (menuHint);
	}

	void ShowLevelReward () {
		switch (level) {
			case 1:
				Terminal.WriteLine ("Have a book...");
				Terminal.WriteLine ("Play again for a greater challenge");
				Terminal.WriteLine (@"
    ________
   /       //
  /       //
 /______ //
(_______(/
				");
				break;
			case 2:
				Terminal.WriteLine ("You got the prison key!");
				Terminal.WriteLine ("Play again for a greater challenge");
				Terminal.WriteLine (@"
 ___
/0  \_______
\___/\/\/-\/
				");
				break;
			case 3:
				Terminal.WriteLine (@"
 _ __   __ _ ___  __ _
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___)\__,_|
				");
				Terminal.WriteLine ("Welcome to NASA's internal system!");
				break;
			default:
				Debug.LogError ("Invalid level reached");
				break;
		}
	}
}