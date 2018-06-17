using System;
using System.Text.RegularExpressions;
using Resources = TypeSafety.Resources;
using UnityEngine;

/// Destinations remain on the map until the ghost reaches it or touches a stationary ghost in the same group.
/// Once this occurs, the destination is moved to back to its ghost.
/// Two ghosts with intersecting destinations are grouped together.
/// drag-selected ghosts are always grouped.
///
/// If this ghost's destination lands on another stationary ghost:
/// 	This ghost joins the stationary ghost's group.
/// Else:
/// 	A new group is created and assigned to this ghost.

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))] // for trigger-trigger collisions
public class GhostDestination : MonoBehaviour {
	// The group this destination belongs to
	public Guid group;
	
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
		// TODO: set this to only collide with other GhostDestinations
		GhostPath otherPath = other.GetComponent<GhostPath>();
		
		if ((otherPath.isStopped || otherPath.reachedEndOfPath) && ghost.destination.group//TODO) {
			// stop ghost
			ghostPath.SetDestination(ghost.transform.position);
			
		}
	}
}