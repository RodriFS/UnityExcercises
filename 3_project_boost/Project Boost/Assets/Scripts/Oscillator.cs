using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector = new Vector3 (10f, 10f, 10f);
	[SerializeField] float period = 4f;
	[Range (0, 1)][SerializeField] float movementFactor; // 0 for not moved, q for fully moved

	private Vector3 startingPos;
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}

	// Update is called once per frame
	void Update () {

		if (period <= Mathf.Epsilon) return;

		float cycles = Time.time / period; // grows continually from 0. Time.time is how much running time
		const float tau = Mathf.PI * 2;
		float rawSinWave = Mathf.Sin (cycles * tau);

		movementFactor = Mathf.Abs (rawSinWave);

		Vector3 offset = movementVector * movementFactor;
		transform.position = startingPos + offset;
	}
}