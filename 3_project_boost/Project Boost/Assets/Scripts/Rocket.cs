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
	public bool collisionsDisabled = false;

	private Rigidbody rb;
	private AudioSource audioSource;
	bool isTransitioning = false;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		audioSource = GetComponent<AudioSource> ();
	}
	void Update () {
		if (!isTransitioning) {
			RespondToThrustInput ();
			RespondToRotateInput ();
		}
		if (Debug.isDebugBuild) RespondToDebugKeys ();
	}
	void OnCollisionEnter (Collision collision) {

		if (isTransitioning ||  collisionsDisabled) return; // ignore collisions

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

	private void RespondToDebugKeys () {
		if (Input.GetKeyDown (KeyCode.L)) LoadNextLevel ();
		if (Input.GetKeyDown (KeyCode.C)) {
			collisionsDisabled = !collisionsDisabled;
		}
	}
	private void StartSuccessSequence () {
		isTransitioning = true;
		audioSource.Stop ();
		audioSource.PlayOneShot (success);
		successParticles.Play ();
		Invoke ("LoadNextLevel", levelLoadDelay); // parametirise time
	}
	private void StartDeathSequence () {
		isTransitioning = true;
		audioSource.Stop ();
		audioSource.PlayOneShot (death);
		deathParticles.Play ();
		Invoke ("LoadFirstLevel", levelLoadDelay); // parametirise time
	}
	private void LoadFirstLevel () {
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
	}
	private void LoadNextLevel () {
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;
		if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
			nextSceneIndex = 0;
		}
		SceneManager.LoadScene (nextSceneIndex);

	}
	private void RespondToThrustInput () {
		if (Input.GetKey (KeyCode.Space)) { //so it can thrust while rotating
			ApplyThrust ();
		} else {
			StopApplyingThrust ();
		}
	}
	private void StopApplyingThrust () {
		audioSource.Stop ();
		mainEngineParticles.Stop ();
	}
	private void ApplyThrust () {
		float thrustThisFrame = mainThrust * Time.deltaTime;
		rb.AddRelativeForce (Vector3.up * thrustThisFrame);
		if (!audioSource.isPlaying) audioSource.PlayOneShot (mainEngine); // so it doesn't layer up
		mainEngineParticles.Play ();
	}
	private void RespondToRotateInput () {
		if (Input.GetKey (KeyCode.A)) {
			RotateManually (rcsThrust * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.D)) {
			RotateManually (-rcsThrust * Time.deltaTime);

		}
	}
	private void RotateManually (float rotationThisFrame) {
		rb.freezeRotation = true; // take manual control of rotation
		transform.Rotate (Vector3.forward * rotationThisFrame);
		rb.freezeRotation = false; // resume physics control of rotation
	}
}