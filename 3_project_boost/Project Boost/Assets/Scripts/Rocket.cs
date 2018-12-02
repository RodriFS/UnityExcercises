using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;

	private Rigidbody rb;
	private AudioSource audioSource;
	enum State { Alive, Dying, Transcending };
 State state = State.Alive;
 // Use this for initialization
 void Start () {
 rb = GetComponent<Rigidbody> ();
 audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (state == State.Alive) {
			Thrust ();
			Rotate ();
		}
	}

	void OnCollisionEnter (Collision collision) {

		if (state != State.Alive) return; // ignore collisions

		switch (collision.gameObject.tag) {
			case "Friendly":
				// do nothing
				break;
			case "Finish":
				state = State.Transcending;
				Invoke ("LoadNextLevel", 1f); // parametirise time
				break;
			default:
				state = State.Dying;
				Invoke ("LoadFirstLevel", 1f); // parametirise time
				break;
		}
	}

	private void LoadFirstLevel () {
		SceneManager.LoadScene (0);
	}
	private void LoadNextLevel () {
		SceneManager.LoadScene (1);
	}
	private void Thrust () {

		float thrustThisFrame = mainThrust * Time.deltaTime;
		if (Input.GetKey (KeyCode.Space)) { //so it can thrust while rotating
			rb.AddRelativeForce (Vector3.up * thrustThisFrame);
			if (!audioSource.isPlaying) audioSource.Play (); // so it doesn't layer up
		} else {
			audioSource.Stop ();
		}
	}

	private void Rotate () {
		rb.freezeRotation = true; // take manual control of rotation

		float rotationThisFrame = rcsThrust * Time.deltaTime;

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.forward * rotationThisFrame);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (-Vector3.forward * rotationThisFrame);
		}

		rb.freezeRotation = false; // resume physics control of rotation
	}

}