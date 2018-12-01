using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		Thrust ();
		Rotate ();
	}
	private void Thrust () {
		if (Input.GetKey (KeyCode.Space)) {
			rb.AddRelativeForce (Vector3.up);
			if (!audioSource.isPlaying) audioSource.Play ();
		} else {
			audioSource.Stop ();
		}
	}

	private void Rotate () {
		rb.freezeRotation = true; // take manual control of rotation
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.forward);
		} else if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (-Vector3.forward);
		}
		rb.freezeRotation = false; // resume physics control of rotation
	}

}