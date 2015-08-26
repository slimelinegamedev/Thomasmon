using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationSignController : MonoBehaviour {

	public int duration;

	// Use this for initialization
	void Start () {
		StartCoroutine (displayLocation ());
	}

	IEnumerator displayLocation () {
		GameObject text_go = transform.Find ("LocationText").gameObject;
		Text text = text_go.GetComponent<Text> ();
		text.text = Application.loadedLevelName.Replace ("Players", InterSceneData.main.playerName + "s ");
		yield return new WaitForSeconds (duration);
		gameObject.SetActive (false);
	}
}
