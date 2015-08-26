using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class PlayerSoundController : MonoBehaviour {
	void OnCollisionEnter2D () {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
	}
}