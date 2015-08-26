using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleInitialisation : MonoBehaviour {

	public Text op_name;
	public Text fl_name;

	public Text op_kp;
	public Text fl_kp;

	public Text op_lvl;
	public Text fl_lvl;

	public Text attack_1;
	public Text attack_2;
	public Text attack_3;
	public Text attack_4;

	public Pokemon opponent;
	public Pokemon friendly;

	public Image op_sprite;

	// Use this for initialization
	void Start () {
		opponent = InterSceneData.main.battle_opponent;
		friendly = InterSceneData.main.battle_friendly;

		op_name.text = opponent.name;
		fl_name.text = friendly.name;

		op_kp.text = "KP: " + opponent.hp.ToString () + " / " + opponent.maxHp.ToString ();
		fl_kp.text = "KP: " + friendly.hp.ToString () + " / " + friendly.maxHp.ToString ();

		op_lvl.text = "Level: " + opponent.level.ToString ();
		fl_lvl.text = "Level: " + friendly.level.ToString ();

		attack_1.text = friendly.attacks [0].aname + "\n AP: " + friendly.attacks [0].ap.ToString () + " / " + friendly.attacks [0].maxAp.ToString ();
		attack_2.text = friendly.attacks [1].aname + "\n AP: " + friendly.attacks [1].ap.ToString () + " / " + friendly.attacks [1].maxAp.ToString ();
		attack_3.text = friendly.attacks [2].aname + "\n AP: " + friendly.attacks [2].ap.ToString () + " / " + friendly.attacks [2].maxAp.ToString ();
		attack_4.text = friendly.attacks [3].aname + "\n AP: " + friendly.attacks [3].ap.ToString () + " / " + friendly.attacks [3].maxAp.ToString ();

		op_sprite.sprite = opponent.picture;
	}
}
