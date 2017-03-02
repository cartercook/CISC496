﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlashlightCollider : MonoBehaviour {

	HashSet<Ghost> stunnedGhosts = new HashSet<Ghost>(); //add colliding ghosts to this hashset

	// Not sure if the ghosts can rotate? The flashlight just always hits their back right now, so unless they can rotate, we can just
	// remove all the code after the first if statement and just have the stunning conditions be: 
	// hit by flashlight and is ghost.

	void OnTriggerEnter (Collider collider) {
		// The light component in the parent flashlight is only enabled when the button is pressed and there is enough power.
		Debug.Log(collider.name+" enter/stay!");

		Ghost ghost = collider.GetComponentInParent<Ghost>();

		// Determine whether the ghost is looking into the light (0 degrees) or away from the light (> 190 degrees)
		// float angle = Vector3.Angle(light.transform.position - ghost.transform.position, ghost.transform.forward);

		if (true /*angle > 190*/) {  // Front or back is hit
			ghost.stun(); // stun the ghost immediately
			stunnedGhosts.Add(ghost);
		} else {
			ghost.unstun();
			stunnedGhosts.Remove(ghost);
		}

	}

	void OnTriggerExit (Collider collider) {
		Debug.Log("Trigger exit!");
		Ghost ghost = collider.GetComponent<Ghost>();
		ghost.unstun();
		stunnedGhosts.Remove(ghost);
	}


	// --------------- PUBLIC FUNCTIONS ---------------

	public void turnOn() {
		gameObject.SetActive(true);
	}

	public void turnOff() {
		gameObject.SetActive(false);

		foreach (Ghost ghost in stunnedGhosts) {
			ghost.unstun();
		}
		stunnedGhosts.Clear();
	}
}
