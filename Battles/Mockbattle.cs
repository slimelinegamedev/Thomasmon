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
			tackle.strenght = 10;
			tackle.type = Attack.Type.physical;

			tackle2.aname = "Yet another Tackle";
			tackle2.ap = 20;
			tackle2.maxAp = 30;
			tackle2.precise = 100;
			tackle2.strenght = 10;
			tackle2.type = Attack.Type.physical;

			tackle3.aname = "Even more Tackle";
			tackle3.ap = 10;
			tackle3.maxAp = 30;
			tackle3.precise = 100;
			tackle3.strenght = 10;
			tackle3.type = Attack.Type.physical;

			tackle4.aname = "Much Tackle";
			tackle4.ap = 0;
			tackle4.maxAp = 30;
			tackle4.precise = 100;
			tackle4.strenght = 10;
			tackle4.type = Attack.Type.physical;

			bulbasaur.name = "Nikuh";
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
			bulbasaur.picture = Resources.LoadAll<Sprite> ("Sprites/pokemon_battle")[245];
			bulbasaur.attacks = new Attack[4];
			bulbasaur.attacks [0] = tackle;
			bulbasaur.attacks [1] = tackle2;
			bulbasaur.attacks [2] = tackle3;
			bulbasaur.attacks [3] = tackle4;

			Attack tackle5 = new Attack ();
			Attack tackle6 = new Attack ();
			Attack tackle7 = new Attack ();
			Attack tackle8 = new Attack ();
			
			Pokemon bulba2 = new Pokemon ();
			
			tackle5.aname = "Tackle";
			tackle5.ap = 30;
			tackle5.maxAp = 30;
			tackle5.precise = 100;
			tackle5.strenght = 40;
			tackle5.type = Attack.Type.physical;
			
			tackle6.aname = "Yet another Tackle";
			tackle6.ap = 20;
			tackle6.maxAp = 30;
			tackle6.precise = 100;
			tackle6.strenght = 40;
			tackle6.type = Attack.Type.physical;
			
			tackle7.aname = "Even more Tackle";
			tackle7.ap = 10;
			tackle7.maxAp = 30;
			tackle7.precise = 100;
			tackle7.strenght = 40;
			tackle7.type = Attack.Type.physical;
			
			tackle8.aname = "Much Tackle";
			tackle8.ap = 0;
			tackle8.maxAp = 30;
			tackle8.precise = 100;
			tackle8.strenght = 40;
			tackle8.type = Attack.Type.physical;
			
			bulba2.name = "Bisasam";
			bulba2.attack = 20;
			bulba2.defense = 15;
			bulba2.specialAttack = 13;
			bulba2.specialDefense = 8;
			bulba2.experience = 300;
			bulba2.level = 6;
			bulba2.id = 0;
			bulba2.pokedexId = 1;
			bulba2.trainer = InterSceneData.main.playerName;
			bulba2.types = new Pokemon.Type[10];
			bulba2.types [0] = Pokemon.Type.grass;
			bulba2.maxHp = 50;
			bulba2.hp = 50;
			bulba2.picture = Resources.LoadAll<Sprite> ("Sprites/pokemon_battle")[0];
			bulba2.attacks = new Attack[4];
			bulba2.attacks [0] = tackle;
			bulba2.attacks [1] = tackle6;
			bulba2.attacks [2] = tackle7;
			bulba2.attacks [3] = tackle8;

			InterSceneData.main.battle_opponent = bulbasaur;
			InterSceneData.main.battle_friendly = bulba2;
		}
	}
}
