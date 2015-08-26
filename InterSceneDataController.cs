using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InterSceneDataController : MonoBehaviour {

	bool sceneIsArea = false;
	bool firstupdate = true;
	GameObject player;

	// Use this for initialization
	void Start () {
		if (GameObject.Find ("AreaMarker") != null) {
			sceneIsArea = true;
			player = GameObject.FindGameObjectWithTag ("Player");

			if (InterSceneData.main.destinatedSpawn == "LAST") {
				player.transform.position = new Vector2 (InterSceneData.main.posX, InterSceneData.main.posY);
				InterSceneData.main.destinatedSpawn = "DEFAULT";
			} else if (InterSceneData.main.destinatedSpawn != "DEFAULT") {
				GameObject spawn = GameObject.Find (InterSceneData.main.destinatedSpawn);
				player.transform.position = spawn.transform.position;
				InterSceneData.main.destinatedSpawn = "DEFAULT";
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (firstupdate) {
			InterSceneData.main.lastScene = Application.loadedLevelName;
			if (sceneIsArea) {
				InterSceneData.main.lastArea = Application.loadedLevelName;
			}
			firstupdate = false;
		}

		if (sceneIsArea) {
			if (player == null) {
				ReloadPlayerObject ();
			}

			InterSceneData.main.posX = player.transform.position.x;
			InterSceneData.main.posY = player.transform.position.y;

			InterSceneData.main.heading = player.GetComponent<PlayerMovementController> ().heading;
		}
		InterSceneData.main.minutesPlayed += Time.deltaTime / 60;
	}

	void onDisable () {
		InterSceneData.main.lastScene = Application.loadedLevelName;
		if (sceneIsArea) {
			InterSceneData.main.lastArea = Application.loadedLevelName;
		}
	}

	void ReloadPlayerObject () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	public void Save () {
		SaveData dat = new SaveData ();

		dat.posX = InterSceneData.main.posX;
		dat.posY = InterSceneData.main.posY;
		dat.headingX = InterSceneData.main.heading.x;
		dat.headingY = InterSceneData.main.heading.y;
		dat.lastScene = InterSceneData.main.lastScene;
		dat.lastArea = InterSceneData.main.lastArea;
		dat.destinatedSpawn = InterSceneData.main.destinatedSpawn;
		dat.playerName = InterSceneData.main.playerName;
		dat.minutesPlayed = InterSceneData.main.minutesPlayed;
		dat.money = InterSceneData.main.money;
		dat.badges = InterSceneData.main.badges;

		BinaryFormatter form = new BinaryFormatter ();
		FileStream of = File.Create (Application.persistentDataPath + "/save.dat");
		form.Serialize (of, dat);
		of.Close ();
	}

	public bool Load () {
		if (File.Exists (Application.persistentDataPath + "/save.dat")) {
			BinaryFormatter form = new BinaryFormatter ();
			FileStream inf = File.OpenRead (Application.persistentDataPath + "/save.dat");
			SaveData dat = (SaveData)form.Deserialize (inf);
			inf.Close ();

			InterSceneData.main.posX = dat.posX;
			InterSceneData.main.posY = dat.posY;
			InterSceneData.main.heading.y = dat.headingY;
			InterSceneData.main.heading.x = dat.headingX;
			InterSceneData.main.lastScene = dat.lastScene;
			InterSceneData.main.lastArea = dat.lastArea;
			InterSceneData.main.destinatedSpawn = dat.destinatedSpawn;
			InterSceneData.main.playerName = dat.playerName;
			InterSceneData.main.minutesPlayed = dat.minutesPlayed;
			InterSceneData.main.money = dat.money;
			InterSceneData.main.badges = dat.badges;

			return true;
		} else {
			return false;
		}
	}
}

[System.Serializable]
class SaveData {

	// This is basically a non-MonoBehaviour copy of InterSceneData

	public float posX;
	public float posY;
	public float headingX;
	public float headingY;
	
	public string lastScene;
	public string lastArea;
	public string destinatedSpawn;
	

	public string playerName;
	public float minutesPlayed;
	public int money;
	public int badges;
}