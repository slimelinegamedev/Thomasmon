using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IGMenuOpenHandler : MonoBehaviour, IEventSystemHandler {

	public void onMenuButtonPress (BaseEventData data) {
		Application.LoadLevelAdditive ("IGMenu");
	}
}
