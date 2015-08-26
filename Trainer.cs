using UnityEngine;
using System.Collections;

public class Trainer : MonoBehaviour {

	public bool isDefeated;

	public uint trainer_id;

	public string[] beforeBattleMessage;
	public string[] afterBattleMessage;

	public int reward;

	void OnTriggerEnter2D (Collider2D col) {
		if (InterSceneData.main.defeatedTrainers.IndexOf (";" + trainer_id.ToString () + ";") != -1)
			isDefeated = true;
		if (!isDefeated) {
			StartCoroutine (handleBattleRequest ());
		}
	}

	IEnumerator handleBattleRequest () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		TalkController tcontroller = player.GetComponent<TalkController> ();

		InterSceneData.main.battle_trainer = trainer_id;

		foreach (string msg in beforeBattleMessage) {
			yield return StartCoroutine (tcontroller.showMessage (msg, 2f));
		}

		InterSceneData.main.battle_friendly = InterSceneData.main.pokemons.GetAll ().ToArray () [0] as Pokemon;
		InterSceneData.main.battle_opponent = player.GetComponent<TrainerPokemonDatabase> ().getPokemonForID (trainer_id);

		Application.LoadLevel ("BattleScene");
	}

	public IEnumerator defeated () {

		InterSceneData.main.defeatedTrainers += trainer_id.ToString () + ";";

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		TalkController tcontroller = player.GetComponent<TalkController> ();

		foreach (string msg in afterBattleMessage) {
			yield return StartCoroutine (tcontroller.showMessage (msg, 2f));
		}
		
		InterSceneData.main.money += reward;
		yield return StartCoroutine(tcontroller.showMessage (InterSceneData.main.playerName + " hat " + reward.ToString () + "$ erhalten!", 2f));
	}
}
