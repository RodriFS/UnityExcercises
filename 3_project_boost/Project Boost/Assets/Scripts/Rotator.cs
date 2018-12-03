using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	[SerializeField] Vector3 rotationSpeed = new Vector3 (10f, 10f, 10f);
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 rotation = rotationSpeed * Time.deltaTime;
		transform.Rotate (rotation);
	}
}