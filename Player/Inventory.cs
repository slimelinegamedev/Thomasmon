using UnityEngine;
using System.Linq;
using System.Collections;

public class Inventory : MonoBehaviour {

	private ArrayList items = new ArrayList ();

	public void Add (Item item) {
		Item nitem = Object.Instantiate (item);
		nitem.gameObject.SetActive (false);
		items.Add(nitem);
	}

	public bool CheckFor (Item item) {
		if (items.Contains (item)) {
			return true;
		} else {
			return false;
		}
	}

	public void Remove (Item item) {
		if (item.category != Item.Category.Basis) {
			items.Remove (item);
		}
	}

	public ArrayList GetAll () {
		return items;
	}
}
