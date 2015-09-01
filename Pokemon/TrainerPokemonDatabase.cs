using UnityEngine;
using System.Collections;

public class TrainerPokemonDatabase : MonoBehaviour {

	private ArrayList data = new ArrayList ();

	// Use this for initialization
	void Start () {
		Attack tackle = new Attack ();
		Attack tackle2 = new Attack ();
		Attack tackle3 = new Attack ();
		Attack tackle4 = new Attack ();
		
		Pokemon nikuh = new Pokemon ();
		
		tackle.aname = "Milchmelker";
		tackle.ap = 30;
		tackle.maxAp = 30;
		tackle.precise = 100;
		tackle.strenght = 10;
		tackle.type = Attack.Type.physical;
		
		tackle2.aname = "Gemuhe";
		tackle2.ap = 20;
		tackle2.maxAp = 30;
		tackle2.precise = 100;
		tackle2.strenght = 10;
		tackle2.type = Attack.Type.physical;
		
		tackle3.aname = "Hufstampfer";
		tackle3.ap = 10;
		tackle3.maxAp = 30;
		tackle3.precise = 100;
		tackle3.strenght = 10;
		tackle3.type = Attack.Type.physical;
		
		tackle4.aname = "Dubstepkanone";
		tackle4.ap = 40;
		tackle4.maxAp = 50;
		tackle4.precise = 100;
		tackle4.strenght = 10;
		tackle4.type = Attack.Type.physical;
		
		nikuh.name = "Nikuh";
		nikuh.attack = 20;
		nikuh.defense = 15;
		nikuh.specialAttack = 13;
		nikuh.specialDefense = 8;
		nikuh.experience = 300;
		nikuh.level = 6;
		nikuh.id = 0;
		nikuh.pokedexId = 1;
		nikuh.trainer = InterSceneData.main.playerName;
		nikuh.types = new Pokemon.Type[10];
		nikuh.types [0] = Pokemon.Type.grass;
		nikuh.maxHp = 50;
		nikuh.hp = 50;
		nikuh.picture = Resources.LoadAll<Sprite> ("Sprites/pokemon_battle")[245];
		nikuh.attacks = new Attack[4];
		nikuh.attacks [0] = tackle;
		nikuh.attacks [1] = tackle2;
		nikuh.attacks [2] = tackle3;
		nikuh.attacks [3] = tackle4;

		insertPokemon (nikuh);
	}

	public Pokemon getPokemonForID (uint id) {
		return data.ToArray ()[id] as Pokemon;
	}

	private void insertPokemon (Pokemon pokemon) {
		data.Add (pokemon);
	}
}
