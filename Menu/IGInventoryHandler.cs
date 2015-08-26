using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IGInventoryHandler : MonoBehaviour, IEventSystemHandler {

	public GameObject InventoryUI;
	public GameObject InventoryText;

	public void onInventoryButtonPressed (BaseEventData data) {
		Debug.Log ("Opening inventory");
		InventoryUI.SetActive (true);

		Text text = InventoryText.GetComponent<Text> ();
		object[] items = InterSceneData.main.inventory.GetAll ().ToArray ();

		text.text = "Items:\n";

		Debug.Log (InterSceneData.main.inventory.GetAll ());
		Debug.Log (InterSceneData.main.inventory.GetAll ().ToArray ());
		Debug.Log (items);
		
		foreach (object obj in items) {
			Item item = obj as Item;
			text.text += item.iname + ": " + item.description + "\n";
		}
	}

	public void onInventoryCloseButtonPressed (BaseEventData data) {
		Debug.Log ("Closing inventory");
		InventoryUI.SetActive (false);
	}
}
