using UnityEngine;
using System.Collections;

public class InterSceneData : MonoBehaviour {

	// Static references
	public static InterSceneData main;

	// Position data
	public float posX;
	public float posY;
	public Vector2 heading;

	public string lastScene;
	public string lastArea;
	public string destinatedSpawn;

	// Battle information
	public Pokemon 	battle_opponent;
	public Pokemon 	battle_friendly;
	public uint		battle_trainer;
	public bool 	battle_isTrainer;

	// Player information
	public Inventory inventory;
	public Pokemons pokemons;
	public string collectedItems;
	public string defeatedTrainers;
	public string playerName;
	public float minutesPlayed;
	public int money;
	public int badges;

	// Initialisation
	void Awake () {
		// If no instance, initialize one
		if (main == null) {
			DontDestroyOnLoad(gameObject);
			main = this;
			this.destinatedSpawn = "DEFAULT";
			this.collectedItems = ";";
			this.defeatedTrainers = ";";
			this.inventory = gameObject.AddComponent<Inventory> ();
			this.pokemons = gameObject.AddComponent<Pokemons> ();
			this.battle_opponent = new Pokemon ();
			this.battle_friendly = new Pokemon ();
		} else if (main != this) {
			Destroy(gameObject);
		}
	}
}
