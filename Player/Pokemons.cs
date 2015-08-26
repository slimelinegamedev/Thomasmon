using UnityEngine;
using System.Collections;

public class Pokemons : MonoBehaviour {

	private ArrayList pokemon = new ArrayList ();
	
	public void Add (Pokemon pok) {
		Pokemon npok = pok;
		pokemon.Add(npok);
	}
	
	public bool CheckFor (Pokemon pok) {
		if (pokemon.Contains (pok)) {
			return true;
		} else {
			return false;
		}
	}
	
	public void Remove (Pokemon pok) {
		pokemon.Remove (pok);
	}
	
	public ArrayList GetAll () {
		return pokemon;
	}
}
