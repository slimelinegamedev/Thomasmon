using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleHandler : MonoBehaviour {

	public Text op_kp_txt;
	public Text fl_kp_txt;

	public bool firstupdate = true;
	public bool battleEnd = false;
	public bool eventTriggered = false;

	public Attack usersChoice;

	void Update () {
		if (firstupdate) {
			StartCoroutine(battleLoop ());
			// battleLoop ();
			//displayKP ();
			//friendlyAttack ();
			//opponentAttack ();
			//displayKP ();
			//firstupdate = false;
		}
	}

	IEnumerator battleLoop () {
		//while (!battleEnd) {
			displayKP ();
			friendlyAttack ();
			displayKP ();
			opponentAttack ();
			displayKP ();
			battleEnd = true;
		//}
		yield return null;
	}

	void friendlyAttack () {
		usersChoice = new Attack ();
		//do {
		Debug.Log ("Waiting");
			waitForUserChoice ();
		//} while (usersChoice.ap <= 0);
		Debug.Log ("Calculating");
		int damage = calculateDamage (InterSceneData.main.battle_friendly, InterSceneData.main.battle_opponent, usersChoice);

		if (damage >= InterSceneData.main.battle_opponent.hp) {
			InterSceneData.main.battle_opponent.hp = 0;
		} else {
			InterSceneData.main.battle_opponent.hp -= (uint)damage;
		}
	}

	IEnumerator waitForUserChoice () {
		while (!eventTriggered) {
			yield return new WaitForEndOfFrame ();
		}

		eventTriggered = false;
		yield return null;
	}

	void opponentAttack () {
		setControlLock (true);

		Attack chosen = new Attack ();
		do {
			int attackIndex = Random.Range (0, 3);
			Debug.Log ("Chosen attack " + attackIndex.ToString ());
			chosen = InterSceneData.main.battle_opponent.attacks[attackIndex];
		} while (chosen.ap <= 0);

		int damage = calculateDamage (InterSceneData.main.battle_opponent, InterSceneData.main.battle_friendly, chosen);

		if (damage >= InterSceneData.main.battle_friendly.hp) {
			InterSceneData.main.battle_friendly.hp = 0;
		} else {
			InterSceneData.main.battle_friendly.hp -= (uint)damage;
		}

		setControlLock (false);
	}

	int calculateDamage (Pokemon attacker, Pokemon defender, Attack attack) {
		float rawdamage = 0;
		if (attack.type == Attack.Type.physical) {
			rawdamage = (attacker.attack / defender.defense) * attack.strenght + 2;
		} else if (attack.type == Attack.Type.special) {
			rawdamage = (attacker.specialAttack / defender.specialDefense) * attack.strenght + 2;
		} else {
			return 0;
		}

		Debug.Log ("Rawdamage: " + rawdamage.ToString ());
		return (int)(rawdamage * Random.Range (0.95f, 1.0f));
	}

	void setControlLock (bool set) {
		MenuOverlayHandler handler = gameObject.GetComponent<MenuOverlayHandler> ();
		if (set) {
			handler.controlsLocked = true;
		} else {
			handler.controlsLocked = false;
		}
	}

	void displayKP () {
		uint op_maxKP = InterSceneData.main.battle_opponent.maxHp;
		uint op_kp = InterSceneData.main.battle_opponent.hp;

		uint fl_maxKP = InterSceneData.main.battle_friendly.maxHp;
		uint fl_kp = InterSceneData.main.battle_friendly.hp;

		float op_percent = (float)op_kp / (float)op_maxKP * 100;
		float fl_percent = (float)fl_kp / (float)fl_maxKP * 100;

		op_kp_txt.text = "KP: " + op_kp.ToString () + " / " + op_maxKP.ToString ();

		if (op_percent > 35) {
			op_kp_txt.color = Color.black;
		} else if (op_percent > 15) {
			op_kp_txt.color = Color.yellow;
		} else {
			op_kp_txt.color = Color.red;
		}

		fl_kp_txt.text = "KP: " + fl_kp.ToString () + " / " + fl_maxKP.ToString ();
		
		if (fl_percent > 35) {
			fl_kp_txt.color = Color.black;
		} else if (fl_percent > 15) {
			fl_kp_txt.color = Color.yellow;
		} else {
			fl_kp_txt.color = Color.red;
		}
	}

	public void attack1Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[0];
	}

	public void attack2Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[1];
	}

	public void attack3Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[2];
	}

	public void attack4Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[3];
	}
}
