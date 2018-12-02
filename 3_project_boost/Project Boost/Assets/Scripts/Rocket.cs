using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 100f;
	[SerializeField] float levelLoadDelay = 2f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip success;
	[SerializeField] AudioClip death;
	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem successParticles;
	[SerializeField] ParticleSystem deathParticles;

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
			RespondToThrustInput ();
			RespondToRotateInput ();
		}
	}

	void OnCollisionEnter (Collision collision) {

		if (state != State.Alive) return; // ignore collisions

		switch (collision.gameObject.tag) {
			case "Friendly":
				// do nothing
				break;
			case "Finish":
				StartSuccessSequence ();
				break;
			default:
				StartDeathSequence ();
				break;
		}
	}

	private void StartSuccessSequence () {
		state = State.Transcending;
		audioSource.Stop ();
		audioSource.PlayOneShot (success);
		successParticles.Play ();
		Invoke ("LoadNextLevel", levelLoadDelay); // parametirise time
	}
	private void StartDeathSequence () {
		state = State.Dying;
		audioSource.Stop ();
		audioSource.PlayOneShot (death);
		deathParticles.Play ();
		Invoke ("LoadFirstLevel", levelLoadDelay); // parametirise time
	}
	private void LoadFirstLevel () {
		SceneManager.LoadScene (0);
	}
	private void LoadNextLevel () {
		SceneManager.LoadScene (1);
	}
	private void RespondToThrustInput () {
		if (Input.GetKey (KeyCode.Space)) { //so it can thrust while rotating
			ApplyThrust ();
		} else {
			audioSource.Stop ();
			mainEngineParticles.Stop ();
		}
	}
	private void ApplyThrust () {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		rb.AddRelativeForce (Vector3.up * thrustThisFrame);
		if (!audioSource.isPlaying) audioSource.PlayOneShot (mainEngine); // so it doesn't layer up
		mainEngineParticles.Play ();
	}
	private void RespondToRotateInput () {
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