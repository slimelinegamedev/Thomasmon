using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public string destinationScene;
	public string destinationWaypointName;

	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log ("Warping to " + destinationScene + ", Waypoint " + destinationWaypointName);
		InterSceneData.main.destinatedSpawn = destinationWaypointName;
		Application.LoadLevel (destinationScene);
	}
}
