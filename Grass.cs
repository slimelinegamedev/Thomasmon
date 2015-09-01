using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	public uint grassID;

	void OnTriggerEnter2D (Collider2D col) {
		GameObject player = col.gameObject;
		GrassPokemonDatabase db = player.GetComponent<GrassPokemonDatabase> ();

		Pokemon[] possiblePokemon = db.getPokemonsForID (grassID);
		float chance = db.getChanceForID (grassID);

		if (Random.value > chance) {
			int chosen = Random.Range(0, possiblePokemon.Length - 1);
			StartCoroutine (battle (player, possiblePokemon[chosen]));
		}
	}

	IEnumerator battle (GameObject player, Pokemon opponent) {
		PlayerMovementController pmc = player.GetComponent<PlayerMovementController> ();
		TalkController tcontroller = player.GetComponent<TalkController> ();
		
		pmc.isAllowedToMove = false;

		InterSceneData.main.battle_trainer = 1337;

		yield return StartCoroutine (tcontroller.showMessage ("Ein wildes " + opponent.name + " erscheint!", 2f));

		InterSceneData.main.battle_friendly = InterSceneData.main.pokemons.GetAll ().ToArray () [0] as Pokemon;
		InterSceneData.main.battle_opponent = opponent;
		
		Application.LoadLevel ("BattleScene");
	}
}
