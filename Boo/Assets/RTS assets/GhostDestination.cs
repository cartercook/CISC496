using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TypeSafety;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))] // for trigger-trigger collisions
public class GhostDestination : MonoBehaviour {
	// The group this destination belongs to
	public Guid group;
	
	Ghost ghost;
	GhostPath ghostPath;
	
	// Use this for initialization
	void Start ()
	{
		group = Guid.NewGuid();
		ghost = transform.parent.GetComponent<Ghost>();
		ghostPath = ghost.GetComponent<GhostPath>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		// TODO: set proximity detector to only collide with Unit
		GhostPath otherPath = other.GetComponent<GhostPath>();
		
		if ((otherPath.isStopped || otherPath.reachedEndOfPath) && Vector3.Distance(ghostPath.destination, otherPath.destination) < 1) {
			// 
			ghostPath.SetDestination(ghost.transform.position);
			
		}
	}
}
