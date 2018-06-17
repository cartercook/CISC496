using System;
using Resources = TypeSafety.Resources;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))] // for trigger-trigger collisions
public class GhostDestination : MonoBehaviour {
	// The group this destination belongs to
	Guid group;
	
	// ghost this group belongs to (and its path)
	Ghost ghost;
	GhostPath ghostPath;
	
	// Called by ghost
	public static GhostDestination Instantiate(Ghost ghost)
	{
		// position
		Vector3 startPos = ghost.transform.position;
		startPos.y = 0;
		
		// instantiate
		GhostDestination ghostDest = Resources.Ghost.GhostDestination.Instantiate(startPos).GetComponent<GhostDestination>();
		
		// setup
		ghostDest.group = Guid.NewGuid();
		ghostDest.ghost = ghost;
		ghostDest.ghostPath = ghost.GetComponent<GhostPath>();
		
		return ghostDest;
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