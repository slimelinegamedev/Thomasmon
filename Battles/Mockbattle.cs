using UnityEngine;
using System.Collections;

public class Mockbattle : MonoBehaviour {

	public bool mockbattleActive = false;
	
	void Start () {
		if (mockbattleActive) {
			Attack tackle = new Attack ();
			Attack tackle2 = new Attack ();
			Attack tackle3 = new Attack ();
			Attack tackle4 = new Attack ();

			Pokemon bulbasaur = new Pokemon ();

			tackle.aname = "Tackle";
			tackle.ap = 30;
			tackle.maxAp = 30;
			tackle.precise = 100;
			tackle.strenght = 20;
			tackle.type = Attack.Type.physical;

			tackle2.aname = "Yet another Tackle";
			tackle2.ap = 20;
			tackle2.maxAp = 30;
			tackle2.precise = 100;
			tackle2.strenght = 20;
			tackle2.type = Attack.Type.physical;

			tackle3.aname = "Even more Tackle";
			tackle3.ap = 10;
			tackle3.maxAp = 30;
			tackle3.precise = 100;
			tackle3.strenght = 20;
			tackle3.type = Attack.Type.physical;

			tackle4.aname = "Much Tackle";
			tackle4.ap = 0;
			tackle4.maxAp = 30;
			tackle4.precise = 100;
			tackle4.strenght = 20;
			tackle4.type = Attack.Type.physical;

			bulbasaur.name = "Bisasam";
			bulbasaur.attack = 20;
			bulbasaur.defense = 15;
			bulbasaur.specialAttack = 13;
			bulbasaur.specialDefense = 8;
			bulbasaur.experience = 300;
			bulbasaur.level = 6;
			bulbasaur.id = 0;
			bulbasaur.pokedexId = 1;
			bulbasaur.trainer = InterSceneData.main.playerName;
			bulbasaur.types = new Pokemon.Type[10];
			bulbasaur.types [0] = Pokemon.Type.grass;
			bulbasaur.maxHp = 50;
			bulbasaur.hp = 50;
			bulbasaur.picture = Resources.LoadAll<Sprite> ("Sprites/pokemon_battle")[0];
			bulbasaur.attacks = new Attack[4];
			bulbasaur.attacks [0] = tackle;
			bulbasaur.attacks [1] = tackle2;
			bulbasaur.attacks [2] = tackle3;
			bulbasaur.attacks [3] = tackle4;

			Pokemon bulba2 = bulbasaur;

			InterSceneData.main.battle_opponent = bulbasaur;
			InterSceneData.main.battle_friendly = bulba2;
		}
	}
}
