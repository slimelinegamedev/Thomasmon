using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IGMenuHandler : MonoBehaviour, IEventSystemHandler {

	public GameObject saveButton;
	public GameObject quitButton;
	public GameObject statPanel;
	public GameObject statName;
	public GameObject statGT;
	public GameObject statMoney;
	public GameObject statBadges;

	public GameObject MsgSaved;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		statName.GetComponent<Text> ().text = InterSceneData.main.playerName;
		statGT.GetComponent<Text> ().text = "Spielzeit: " + Mathf.RoundToInt(InterSceneData.main.minutesPlayed).ToString () + " Minuten";
		statMoney.GetComponent<Text> ().text = "Geld: " + InterSceneData.main.money.ToString () + " $";
		statBadges.GetComponent <Text> ().text = "Orden: " + InterSceneData.main.badges.ToString ();
	}

	public void onExitButtonPress (BaseEventData data) {
		InterSceneData.main.destinatedSpawn = "LAST";
		Application.LoadLevel (InterSceneData.main.lastArea);
	}

	public void onSaveButtonPress (BaseEventData data) {
		InterSceneDataController con = GameObject.Find ("InterSceneDataController").GetComponent<InterSceneDataController> ();
		con.Save ();
		MsgSaved.SetActive (true);
	}

	public void onQuitButtonPress (BaseEventData data) {
		Debug.Log ("Exiting. Status: nominal");
		Application.Quit ();
	}

	public void onSavedAcknowledged (BaseEventData data) {
		MsgSaved.SetActive (false);
	}
}
