using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewGameHandler : MonoBehaviour, IEventSystemHandler {

	public GameObject NewGamePanel;
	public GameObject NewGameButton;
	public GameObject LoadGameButton;
	public GameObject NewGameOKButton;
	public GameObject NewGameCancelButton;
	public GameObject NewGameNameField;

	// Use this for initialization
	void Start () {
		Button but = NewGameButton.GetComponent<Button> ();
		but.onClick.AddListener (() => onNewGameButtonPressed());

		but = NewGameOKButton.GetComponent<Button> ();
		but.onClick.AddListener (() => onNewGameOKButtonPressed());

		but = NewGameCancelButton.GetComponent<Button> ();
		but.onClick.AddListener (() => onNewGameCancelButtonPressed());

		NewGamePanel.SetActive (false);
		NewGameOKButton.SetActive (false);
		NewGameNameField.SetActive (false);
		NewGameCancelButton.SetActive (false);
	}

	public void onNewGameButtonPressed () {
		NewGameButton.SetActive (false);
		LoadGameButton.SetActive (false);
		NewGamePanel.SetActive (true);
		NewGameNameField.SetActive (true);
		NewGameOKButton.SetActive (true);
		NewGameCancelButton.SetActive (true);
	}

	public void onNewGameOKButtonPressed () {
		InputField ifield = NewGameNameField.GetComponent<InputField> ();
		InterSceneData.main.playerName = ifield.text;
		Application.LoadLevel ("PlayersHouse");
	}

	public void onNewGameCancelButtonPressed () {
		NewGameButton.SetActive (true);
		LoadGameButton.SetActive (true);
		NewGamePanel.SetActive (false);
		NewGameNameField.SetActive (false);
		NewGameOKButton.SetActive (false);
		NewGameCancelButton.SetActive (false);
	}
}
