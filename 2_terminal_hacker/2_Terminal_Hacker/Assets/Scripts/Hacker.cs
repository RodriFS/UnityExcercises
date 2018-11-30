using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// game state
	private int level;
	private enum Screen { MainMenu, Password, Win };
 private Screen currentScreen = Screen.MainMenu;

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
		Terminal.WriteLine ("Enter your selection:");
	}

	void OnUserInput (string input) {
		if (input == "menu") {
			currentScreen = Screen.MainMenu;
			ShowMainMenu ();
		} else if (currentScreen == Screen.MainMenu) {
			RunMainMenu (input);
		}
	}

	void RunMainMenu (string input) {
		switch (input) {
			case "1":
				level = 1;
				StartGame ();
				break;
			case "2":
				level = 2;
				StartGame ();
				break;
			default:
				Terminal.WriteLine ("Please choose a valid level");
				break;
		}
	}

	void StartGame () {
		currentScreen = Screen.Password;
		Terminal.WriteLine ("You have chosen level " + level);
		Terminal.WriteLine ("Please enter your password:");
	}

	// Update is called once per frame
	void Update () {

	}
}