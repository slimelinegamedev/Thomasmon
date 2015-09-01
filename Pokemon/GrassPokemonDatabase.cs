using UnityEngine;
using System.Collections;

public class GrassPokemonDatabase : MonoBehaviour {

	private ArrayList data = new ArrayList ();
	private ArrayList chances = new ArrayList ();
	
	// Use this for initialization
	void Start () {
		Pokemon[] grass1 = new Pokemon[1];

		Attack tackle = new Attack ();
		Attack tackle2 = new Attack ();
		Attack tackle3 = new Attack ();
		Attack tackle4 = new Attack ();
		
		Pokemon schwalbeini = new Pokemon ();
		
		tackle.aname = "Flügelschlag";
		tackle.ap = 30;
		tackle.maxAp = 30;
		tackle.precise = 100;
		tackle.strenght = 5;
		tackle.type = Attack.Type.physical;
		
		tackle2.aname = "Shellshock";
		tackle2.ap = 20;
		tackle2.maxAp = 30;
		tackle2.precise = 100;
		tackle2.strenght = 10;
		tackle2.type = Attack.Type.physical;
		
		tackle3.aname = "Schnabel";
		tackle3.ap = 10;
		tackle3.maxAp = 30;
		tackle3.precise = 100;
		tackle3.strenght = 5;
		tackle3.type = Attack.Type.physical;
		
		tackle4.aname = "Snowdenfrisur";
		tackle4.ap = 0;
		tackle4.maxAp = 30;
		tackle4.precise = 100;
		tackle4.strenght = 5;
		tackle4.type = Attack.Type.physical;
		
		schwalbeini.name = "Schwalbeini";
		schwalbeini.attack = 20;
		schwalbeini.defense = 15;
		schwalbeini.specialAttack = 13;
		schwalbeini.specialDefense = 8;
		schwalbeini.experience = 0;
		schwalbeini.level = 5;
		schwalbeini.id = 0;
		schwalbeini.pokedexId = 1;
		schwalbeini.trainer = InterSceneData.main.playerName;
		schwalbeini.types = new Pokemon.Type[10];
		schwalbeini.types [0] = Pokemon.Type.normal;
		schwalbeini.maxHp = 45;
		schwalbeini.hp = 45;
		schwalbeini.picture = Resources.LoadAll<Sprite> ("Sprites/pokemon_battle")[15];
		schwalbeini.attacks = new Attack[4];
		schwalbeini.attacks [0] = tackle;
		schwalbeini.attacks [1] = tackle2;
		schwalbeini.attacks [2] = tackle3;
		schwalbeini.attacks [3] = tackle4;
		grass1 [0] = schwalbeini;

		insertPokemons (grass1);
		insertChance (0.9f); // 10% Chance (100 - 90 = 10)
	}
	
	public Pokemon[] getPokemonsForID (uint id) {
		return data.ToArray ()[id] as Pokemon[];
	}

	public float getChanceForID (uint id) {
		return (float)chances.ToArray ()[id];
	}
	
	private void insertPokemons (Pokemon[] pokemons) {
		data.Add (pokemons);
	}

	private void insertChance (float chance) {
		chances.Add (chance);
	}
}
