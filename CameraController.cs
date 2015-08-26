using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {

		Vector3 newPos = new Vector3 (target.position.x, target.position.y, cam.transform.position.z);
		cam.transform.position = newPos;
	}
}
