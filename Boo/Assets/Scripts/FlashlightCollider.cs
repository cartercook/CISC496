﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FlashlightCollider : MonoBehaviour {

	// Not sure if the ghosts can rotate? The flashlight just always hits their back right now, so unless they can rotate, we can just
	// remove all the code after the first if statement and just have the stunning conditions be: 
	// hit by flashlight and is ghost.

	void OnTriggerEnter (Collider collider) {
		// The light component in the parent flashlight is only enabled when the button is pressed and there is enough power.
		if ((transform.parent.GetComponent<Light> ().enabled == true) && (collider.tag == "Unit")) {
			/* // Determine whether the front or back of the ghost entered the trigger.
			Vector3 delta = gameObject.transform.InverseTransformPoint (collider.gameObject.transform.position);
			float yAbs = Mathf.Abs (delta.y);
			float zAbs = Mathf.Abs (delta.z);
			if (zAbs > yAbs) {	// Front or back is hit
				if (delta.z > 0) {	// Back is hit
					Debug.Log ("nothing happened...");
				} else {	// Front is hit
					Debug.Log ("ghost is stunned!");
					// STUN GHOST HERE
				}
			} */
			collider.gameObject.GetComponent<NavMeshAgent> ().Stop ();	// Stun the ghost immediately.
			collider.gameObject.GetComponent<AICharacterControl> ().enabled = false;	// Prevent RTS player from controlling the ghost.
			collider.gameObject.GetComponent<ThirdPersonCharacter> ().enabled = false;
			collider.gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().material.SetColor ("_OutlineColor", new Color32(144, 66, 244, 82));	// Changes ghost outline to purple = stunned.
		}
	}

	void OnTriggerStay (Collider collider) {
		if ((transform.parent.GetComponent<Light> ().enabled == true) && (collider.tag == "Unit")) {
			/* Vector3 delta = gameObject.transform.InverseTransformPoint (collider.gameObject.transform.position);
			float yAbs = Mathf.Abs (delta.y);
			float zAbs = Mathf.Abs (delta.z);
			if (zAbs > yAbs) {	
				if (delta.z > 0) {	
					Debug.Log ("nothing happened...");
				} else {
					Debug.Log ("ghost is stunned!");
					// STUN GHOST HERE
				}
			} */
			collider.gameObject.GetComponent<NavMeshAgent> ().Stop ();
			collider.gameObject.GetComponent<AICharacterControl> ().enabled = false;
			collider.gameObject.GetComponent<ThirdPersonCharacter> ().enabled = false;
			collider.gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().material.SetColor ("_OutlineColor", new Color32(144, 66, 244, 82));
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.tag == "Unit") {
			collider.gameObject.GetComponent<NavMeshAgent> ().Resume ();
			collider.gameObject.GetComponent<AICharacterControl> ().enabled = true;
			collider.gameObject.GetComponent<ThirdPersonCharacter> ().enabled = true;
			collider.gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ().material.SetColor ("_OutlineColor", new Color32(0, 154, 255, 82));
		}
	}
}
