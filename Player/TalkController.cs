using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(AudioSource))]

public class TalkController : MonoBehaviour, IEventSystemHandler {

	public LayerMask layer;
	public Rigidbody2D rbody;
	public AudioSource asrc;
	public AudioClip click;
	public AudioClip tata;
	public GameObject textBox;
	public GameObject textBox_text;
	public PlayerMovementController mcon;

	private Vector2 heading;
	private bool actionPressed;
	private bool shouldClose;

	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		mcon = GetComponent <PlayerMovementController> ();
		if (asrc == null) {
			asrc = GetComponent <AudioSource> ();
		}

		textBox = GameObject.FindGameObjectsWithTag ("TextBox") [0];
		textBox_text = GameObject.FindGameObjectsWithTag ("TextBox_Text") [0];

		textBox.SetActive (false);

		actionPressed = false;
		shouldClose = false;
	}

	void Update () {
		Vector2 movement = mcon.movement;
		if (movement != Vector2.zero) {
			heading = movement;
		}
	}

	public void onActionButtonPressed (BaseEventData data) {
		if (textBox.activeSelf && shouldClose == true) {
			textBox.SetActive (false);
			GetComponent<PlayerMovementController> ().isAllowedToMove = true;
		} else if (textBox.activeSelf == true && shouldClose == false) {
			actionPressed = true;
		} else {
			StartCoroutine (displayText());
		}
	}

	IEnumerator displayText() {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, heading, layer);
		if (hit.collider != null) {
			Vector3 target_heading = hit.point - new Vector2 (transform.position.x, transform.position.y);
			if (target_heading.sqrMagnitude < 0.02) {
				if (hit.transform.gameObject.tag == "Talkable" || hit.transform.gameObject.tag == "Item") {
					shouldClose = false;
					string[] displText = new string[1];
					if (hit.transform.gameObject.tag == "Item") {
						Item item = hit.transform.gameObject.GetComponent<Item> ();
						InterSceneData.main.inventory.Add (item);
						InterSceneData.main.collectedItems += item.iid.ToString () + ";";
						displText[0] = "<PLAYER_NAME> verstaut " + item.iname + " im Rucksack";
						asrc.clip = tata;
						Destroy (hit.transform.gameObject);
					} else {
						TalkContent content = hit.transform.gameObject.GetComponent <TalkContent> ();
						displText = content.content;
						asrc.clip = click;
					}

					GetComponent<PlayerMovementController> ().isAllowedToMove = false; 
					textBox.SetActive (true);
					Text tbt = textBox_text.GetComponent<Text> ();
					int i = 1;
					foreach  (string str in displText) {
						asrc.Play ();
						string nstr = str.Replace ("<PLAYER_NAME>", InterSceneData.main.playerName);
						tbt.text = nstr;
						if (i != displText.Length) {
							while (actionPressed != true) {
								yield return new WaitForSeconds (0.1f);
							}
						}
						actionPressed = false;
						i++;
					}
					shouldClose = true;
				}
			}
		}
		yield return null;
	}

	public void onTextBoxClicked (BaseEventData data) {
		if (shouldClose == true) {
			textBox.SetActive (false);
			GetComponent<PlayerMovementController> ().isAllowedToMove = true;
		} else {
			actionPressed = true;
		}
	}

	public IEnumerator showMessage (string message, float duration) {
		textBox_text.GetComponent<Text> ().text = message;
		textBox.SetActive (true);
		yield return new WaitForSeconds (duration);
		textBox.SetActive (false);
	}
}
