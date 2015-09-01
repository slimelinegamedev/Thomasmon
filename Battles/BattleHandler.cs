using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleHandler : MonoBehaviour {

	public Text op_kp_txt;
	public Text fl_kp_txt;

	public Text attack_1;
	public Text attack_2;
	public Text attack_3;
	public Text attack_4;

	public Text item_1;
	public Text item_2;

	public GameObject attackOverlay;
	public GameObject itemOverlay;
	public GameObject separator;

	public GameObject msgBox;
	public GameObject msgBoxText;

	public bool firstupdate = true;
	public bool battleEnd = false;
	public bool isTrainer = false;

	public bool eventTriggered = false;
	public bool itemChosen = false;
	public Attack usersChoice;
	public Item.Category usersItem;
	public int eventNo;

	void Update () {
		if (firstupdate) {
			if (InterSceneData.main.battle_trainer != 1337)
				isTrainer = true;
			StartCoroutine(battleLoop ());
			firstupdate = false;
		}
	}

	IEnumerator battleLoop () {
		while (!battleEnd) {
			updateStats ();
			yield return StartCoroutine(friendlyAttack ());
			updateStats ();
			yield return StartCoroutine(checkForBattleEnd ());
			yield return StartCoroutine(opponentAttack ());
			updateStats ();
			yield return StartCoroutine(checkForBattleEnd ());
		}

		InterSceneData.main.destinatedSpawn = "LAST";
		Application.LoadLevel (InterSceneData.main.lastArea);
		yield return null;
	}

	IEnumerator checkForBattleEnd () {
		if (InterSceneData.main.battle_friendly.hp <= 0) {
			battleEnd = true;
			yield return StartCoroutine (showMessage (InterSceneData.main.battle_friendly.name + " wurde besiegt", 3f));
			InterSceneData.main.destinatedSpawn = "LAST";
			Application.LoadLevel (InterSceneData.main.lastArea);
		} else if (InterSceneData.main.battle_opponent.hp <= 0 && isTrainer) {
			battleEnd = true;
			yield return StartCoroutine (showMessage (InterSceneData.main.battle_opponent.name + " wurde besiegt", 3f));
			InterSceneData.main.destinatedSpawn = "TRAINER";
			Application.LoadLevel (InterSceneData.main.lastArea);
		} else if (InterSceneData.main.battle_opponent.hp <= 0 && !isTrainer){
			battleEnd = true;
			yield return StartCoroutine (showMessage (InterSceneData.main.battle_opponent.name + " wurde besiegt", 3f));
			InterSceneData.main.destinatedSpawn = "LAST";
			Application.LoadLevel (InterSceneData.main.lastArea);
		}
	}

	IEnumerator friendlyAttack () {
		usersChoice = new Attack ();
		do {
			Debug.Log ("Waiting for user input");
			yield return StartCoroutine (waitForUserChoice ());
		} while (!choiceIsValid ());

		itemOverlay.SetActive (false);
		attackOverlay.SetActive (false);
		separator.SetActive (false);

		if (!itemChosen) {
			InterSceneData.main.battle_friendly.attacks [eventNo].ap -= 1;

			yield return StartCoroutine (showMessage (InterSceneData.main.battle_friendly.name + " setzt " + InterSceneData.main.battle_friendly.attacks [eventNo].aname + " ein", 1.5f));

			Debug.Log ("Calculating damage");
			int damage = calculateDamage (InterSceneData.main.battle_friendly, InterSceneData.main.battle_opponent, usersChoice);

			if (damage >= InterSceneData.main.battle_opponent.hp) {
				InterSceneData.main.battle_opponent.hp = 0;
			} else {
				InterSceneData.main.battle_opponent.hp -= (uint)damage;
			}
		} else {
			if (usersItem == Item.Category.Ball) {
				if (isTrainer) {
					yield return StartCoroutine (showMessage ("Man darf nur wilde Pokemon fangen", 2f));
				} else {
					itemOverlay.SetActive (false);
					attackOverlay.SetActive (false);
					separator.SetActive (false);
					
					yield return StartCoroutine (showMessage (InterSceneData.main.playerName + " wirft Pokeball", 2f));
					removeItem (usersItem);

					float success = 0.5f;
					if (Random.value > success) {
						InterSceneData.main.pokemons.Add (InterSceneData.main.battle_opponent);
						yield return StartCoroutine (showMessage (InterSceneData.main.battle_opponent.name + " wurde gefangen!", 2f));
						battleEnd = true;
						InterSceneData.main.destinatedSpawn = "LAST";
						Application.LoadLevel (InterSceneData.main.lastArea);
					} else {
						yield return StartCoroutine (showMessage (InterSceneData.main.battle_opponent.name + " hat sich befreit!", 2f));
					}
				}
			} else {
				itemOverlay.SetActive (false);
				attackOverlay.SetActive (false);
				separator.SetActive (false);

				yield return StartCoroutine (showMessage (InterSceneData.main.playerName + " benutzt Trank", 2f));

				InterSceneData.main.battle_friendly.hp += 20;
				if (InterSceneData.main.battle_friendly.hp > InterSceneData.main.battle_friendly.maxHp)
					InterSceneData.main.battle_friendly.hp = InterSceneData.main.battle_friendly.maxHp;

				removeItem (usersItem);
			}
		}
	}

	IEnumerator waitForUserChoice () {
		while (!eventTriggered) {
			yield return new WaitForEndOfFrame ();
		}

		eventTriggered = false;
		yield return null;
	}

	bool choiceIsValid () {
		uint medicine = 0;
		uint balls = 0;
		
		foreach (Object obj in InterSceneData.main.inventory.GetAll ().ToArray ()) {
			Item itm = obj as Item;
			if (itm.category == Item.Category.Medicine) {
				medicine++;
			} else if (itm.category == Item.Category.Ball) {
				balls++;
			}
		}

		if (itemChosen) {
			if (usersItem == Item.Category.Medicine) {
				if (medicine == 0)
					return false;
				else
					return true;
			} else {
				if (balls == 0)
					return false;
				else
					return true;
			}
		} else {
			if (usersChoice.ap <= 0)
				return false;
			else
				return true;
		}
	}

	IEnumerator opponentAttack () {
		setControlLock (true);

		Attack chosen = new Attack ();
		do {
			int attackIndex = Random.Range (0, 3);
			Debug.Log ("Chosen attack " + attackIndex.ToString ());
			chosen = InterSceneData.main.battle_opponent.attacks[attackIndex];
			Debug.Log ("Before: " + InterSceneData.main.battle_opponent.attacks[attackIndex].ap.ToString ());
			InterSceneData.main.battle_opponent.attacks[attackIndex].ap -= 1;
			Debug.Log ("After: " + InterSceneData.main.battle_opponent.attacks[attackIndex].ap.ToString ());
		} while (chosen.ap <= 0);

		yield return StartCoroutine (showMessage (InterSceneData.main.battle_opponent.name + " setzt " + InterSceneData.main.battle_opponent.attacks[eventNo].aname + " ein", 1.5f));

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

	void updateStats () {
		displayItems ();
		displayAP ();
		displayKP ();
	}

	void displayItems () {
		uint medicine = 0;
		uint balls = 0;
		
		foreach (Object obj in InterSceneData.main.inventory.GetAll ().ToArray ()) {
			Item itm = obj as Item;
			if (itm.category == Item.Category.Medicine) {
				medicine++;
			} else if (itm.category == Item.Category.Ball) {
				balls++;
			}
		}
		
		item_1.text = "Trank\n" + medicine.ToString () + "x";
		item_2.text = "Pokeball\n" + balls.ToString () + "x";
	}

	void displayAP () {
		attack_1.text = InterSceneData.main.battle_friendly.attacks [0].aname + "\n AP: " + Mathf.Max (0, InterSceneData.main.battle_friendly.attacks [0].ap).ToString () + " / " + InterSceneData.main.battle_friendly.attacks [0].maxAp.ToString ();
		attack_2.text = InterSceneData.main.battle_friendly.attacks [1].aname + "\n AP: " + Mathf.Max (0, InterSceneData.main.battle_friendly.attacks [1].ap).ToString () + " / " + InterSceneData.main.battle_friendly.attacks [1].maxAp.ToString ();
		attack_3.text = InterSceneData.main.battle_friendly.attacks [2].aname + "\n AP: " + Mathf.Max (0, InterSceneData.main.battle_friendly.attacks [2].ap).ToString () + " / " + InterSceneData.main.battle_friendly.attacks [2].maxAp.ToString ();
		attack_4.text = InterSceneData.main.battle_friendly.attacks [3].aname + "\n AP: " + Mathf.Max (0, InterSceneData.main.battle_friendly.attacks [3].ap).ToString () + " / " + InterSceneData.main.battle_friendly.attacks [3].maxAp.ToString ();
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
			op_kp_txt.color = Color.magenta;
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

	void removeItem (Item.Category category) {
		Item remove = new Item ();

		foreach (Object obj in InterSceneData.main.inventory.GetAll ().ToArray ()) {
			Item itm = obj as Item;
			if (itm.category == Item.Category.Medicine && category == Item.Category.Medicine) {
				remove = itm;
			} else if (category == Item.Category.Ball && category == Item.Category.Ball) {
				remove = itm;
			}
		}

		InterSceneData.main.inventory.Remove (remove);
	}

	IEnumerator showMessage (string text, float duration) {
		msgBoxText.GetComponent<Text> ().text = text;
		msgBox.SetActive (true);
		yield return new WaitForSeconds (duration);
		msgBox.SetActive (false);
	}

	public void attack1Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[0];
		eventNo = 0;
		itemChosen = false;
	}

	public void attack2Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[1];
		eventNo = 1;
		itemChosen = false;
	}

	public void attack3Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[2];
		eventNo = 2;
		itemChosen = false;
	}

	public void attack4Pressed (BaseEventData data) {
		eventTriggered = true;
		usersChoice = InterSceneData.main.battle_friendly.attacks[3];
		eventNo = 3;
		itemChosen = false;
	}

	public void item1Pressed (BaseEventData data) {
		eventTriggered = true;
		usersItem = Item.Category.Medicine;
		itemChosen = true;
	}

	public void item2Pressed (BaseEventData data) {
		eventTriggered = true;
		usersItem = Item.Category.Ball;
		itemChosen = true;
	}
}
