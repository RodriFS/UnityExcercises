using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}
	// Use this for initialization
	void Start () {
		Invoke ("LoadFirstLevel", 2f);
	}

	private void LoadFirstLevel () {
		SceneManager.LoadScene (1);
	}
}