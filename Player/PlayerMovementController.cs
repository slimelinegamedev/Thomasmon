using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour, IEventSystemHandler {

	Rigidbody2D body;
	Animator animator;

	public float WalkingSpeed;
	public float RunningSpeed;

	public GameObject MobileControls;

	public bool DebugMode;
	public bool isAllowedToMove;

	public float button_movement_x = 0;
	public float button_movement_y = 0;
	public bool  button_running = false;

	public Vector2 movement;
	public Vector2 heading;

	string lastMover = "x";
	string direction = "x";

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			MobileControls.SetActive (true);
		} else if (DebugMode) {
			MobileControls.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (isAllowedToMove) {
			float movement_x = Input.GetAxisRaw ("Horizontal") + button_movement_x;
			float movement_y = Input.GetAxisRaw ("Vertical") + button_movement_y;
		
			float multiplicator = WalkingSpeed;

			if (Input.GetKey (KeyCode.LeftShift) || button_running) {
				multiplicator = RunningSpeed;
			}

			movement = new Vector2 (0, 0);

			if (movement_x != 0 || movement_y != 0) {
				if (movement_x != 0 && movement_y != 0) {
					if (lastMover == "y") {
						movement = new Vector2 (movement_x, 0);
						direction = "x";
					} else {
						movement = new Vector2 (0, movement_y);
						direction = "y";
					}
				} else if (movement_x == -1 || movement_x == 1) {
					movement = new Vector2 (movement_x, 0);
					lastMover = "x";
					direction = "x";
				} else if (movement_y == -1 || movement_y == 1) {
					movement = new Vector2 (0, movement_y);
					lastMover = "y";
					direction = "y";
				}
				animator.SetBool ("isIdle", false);

				if (direction == "x") {
					animator.SetFloat ("inp_x", movement.x);
					animator.SetFloat ("inp_y", 0);
				} else {
					animator.SetFloat ("inp_y", movement.y);
					animator.SetFloat ("inp_x", 0);
				}

			} else {
				animator.SetBool ("isIdle", true);
			}

			body.MovePosition (body.position + movement * multiplicator * Time.deltaTime);

			if (movement != Vector2.zero) {
				heading = movement;
			}
		}
	}

	public void inp_left_on (BaseEventData EventData) {
		button_movement_x = -1;
	}

	public void inp_left_off (BaseEventData EventData) {
		button_movement_x = 0;
	}

	public void inp_right_on (BaseEventData EventData) {
		button_movement_x = 1;
	}

	public void inp_right_off (BaseEventData EventData) {
		button_movement_x = 0;
	}

	public void inp_up_on (BaseEventData EventData) {
		button_movement_y = 1;
	}

	public void inp_up_off (BaseEventData EventData) {
		button_movement_y = 0;
	}

	public void inp_down_on (BaseEventData EventData) {
		button_movement_y = -1;
	}

	public void inp_down_off (BaseEventData EventData) {
		button_movement_y = 0;
	}

	public void inp_run_on (BaseEventData EventData) {
		button_running = true;
		Debug.Log ("Now running");
	}
	
	public void inp_run_off (BaseEventData EventData) {
		button_running = false;
		Debug.Log ("Now walking");
	}
	
}
