using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	// Item data
	public string iname;
	public int iid;
	public Category category;
	public Sprite picture;
	public string description;

	public enum Category {
		General,
		Medicine,
		Ball,
		Machine,
		Letter,
		Battle,
		Basis
	};

	// Spawn item if not already collected
	void Update () {
		int iid = GetComponent<Item> ().iid;

		if (InterSceneData.main.collectedItems.IndexOf (";" + iid.ToString () + ";") != -1)
			Destroy (gameObject);
	}
}
