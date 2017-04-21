﻿using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {

	private Timer spawnDelay;
	private Timer startDelay;
	Vector3 center;

	AudioSource sfx;

	// Use this for initialization
	void Start () {
		sfx = GetComponent<AudioSource>();

		startDelay = new Timer (25.0f);
		spawnDelay = new Timer (15.0f);
		center = transform.position;
		spawnDelay.StartTimer ();
	}

	// Update is called once per frame
	void Update () {
		if (!startDelay.IsRunning ()) {
			if (spawnDelay.IsRunning ()) {
				spawnDelay.UpdateTimer ();
			} else {
				Vector3 pos = RandomCircle (center, 6.0f);
				Quaternion rot = Quaternion.FromToRotation (Vector3.forward, center - pos);
				Instantiate (Resources.Load("Bomb Ball"), pos, rot);
				sfx.Play ();
				spawnDelay.ResetTimer ();
			}
		}
	}

	Vector3 RandomCircle (Vector3 c, float r) {
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = c.x + r * Mathf.Sin (ang * Mathf.Deg2Rad);
		pos.y = c.y;
		pos.z = c.z + r * Mathf.Sin (ang * Mathf.Deg2Rad);
		return pos;
	}

}