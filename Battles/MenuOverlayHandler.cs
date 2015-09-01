using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuOverlayHandler : MonoBehaviour {

	public bool controlsLocked;

	public GameObject separator;
	public GameObject attack_menu;
	public GameObject item_menu;

	public void onAttackButtonPressed (BaseEventData data) {
		if (!controlsLocked) {
			separator.SetActive (true);
			attack_menu.SetActive (true);
		}
	}

	public void onItemButtonPressed (BaseEventData data) {
		if (!controlsLocked) {
			separator.SetActive (true);
			item_menu.SetActive (true);
		}
	}

	public void onBackButtonPressed (BaseEventData data) {
		item_menu.SetActive (false);
		attack_menu.SetActive (false);
		separator.SetActive (false);
	}
}
