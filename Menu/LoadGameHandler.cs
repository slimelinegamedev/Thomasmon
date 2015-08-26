using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadGameHandler : MonoBehaviour, IEventSystemHandler {

	public GameObject MsgPanel;

	public void onLoadGameButtonPressed (BaseEventData data) {
		if (GameObject.Find ("InterSceneDataController").GetComponent<InterSceneDataController> ().Load ()) {
			InterSceneData.main.destinatedSpawn = "LAST";
			Application.LoadLevel (InterSceneData.main.lastArea);
		} else {
			MsgPanel.SetActive (true);
		}
	}

	public void onErrorMsgAcknowledged (BaseEventData data) {
		MsgPanel.SetActive (false);
	}
}
