using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class IGPokemonHandler : MonoBehaviour {

	public GameObject PokemonUI;
	public GameObject PokemonText;
	
	public void onPokemonButtonPressed (BaseEventData data) {
		Debug.Log ("Opening pokemon list");
		PokemonUI.SetActive (true);
		
		Text text = PokemonText.GetComponent<Text> ();
		object[] pokemons = InterSceneData.main.pokemons.GetAll ().ToArray ();
		
		text.text = "-- UI in PreAlpha state --\nPokemon:\n";
		
		foreach (object obj in pokemons) {
			Pokemon pokemon = obj as Pokemon;
			text.text += pokemon.name + ": Level: " + pokemon.level.ToString () + " EP: " + pokemon.experience.ToString () + "\n";
		}
	}
	
	public void onPokemonCloseButtonPressed (BaseEventData data) {
		Debug.Log ("Closing pokemon list");
		PokemonUI.SetActive (false);
	}
}
