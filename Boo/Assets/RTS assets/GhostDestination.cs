using System;
using System.Text.RegularExpressions;
using TypeSafety;
using UnityEditor;
using Resources = TypeSafety.Resources;
using UnityEngine;

/// Destinations remain on the map until the ghost reaches it or touches a stationary ghost in the same group.
/// Once this occurs, the destination is moved back to its ghost.
/// Two ghosts with intersecting destinations are grouped together.
/// drag-selected ghosts are always grouped.
///
/// If this ghost's destination lands on another stationary ghost:
/// 	This ghost joins the stationary ghost's group.
/// Else:
/// 	A new group is created and assigned to this ghost.

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))] // for trigger-trigger collisions
public class GhostDestination : MonoBehaviour {
	// The group this destination belongs to
	public Guid group;
	
	// ghost this group belongs to (and its path)
	Ghost ghost;
	GhostPath path;
	
	// components
	new CapsuleCollider collider;
	
	// ---------- PUBLIC ----------
	
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
		ghostDest.path = ghost.GetComponent<GhostPath>();
		
		return ghostDest;
	}
	
	public void Set(Vector3 position)
	{
		// If this destination lands on another stationary ghost, join the stationary ghost's group
		group = checkIntesectionAtPosition(position);
		
		// otherwise create a new group
		if (group == Guid.Empty)
		{
			group = Guid.NewGuid();
		}
		
		path.SetDestination(position);
	}
	
	// ---------- PRIVATE ----------
	
	// Called one frame after instatiate
	void Awake()
	{
		collider = GetComponent<CapsuleCollider>();
	}
	
	// GhostDestination only collide with ghosts
	void OnTriggerEnter(Collider other)
	{
		// don't collide with our own ghost!
		if (other.gameObject == ghost.gameObject)
		{
			return;
		}
		
		// get other's GhostDestination
		GhostDestination otherDest = other.GetComponent<Ghost>().destination;
		
		// TODO
		if (otherDest.path.stationary && otherDest.group == this.group) {
			// we've touched another ghost who has reached our shared destination. Stop our ghost.
			path.SetDestination(ghost.transform.position);
		}
	}
	
	/// <summary>
	/// Get the group of any intersecting GhostDestination
	/// </summary>
	/// <param name="position">Worldspace position to check</param>
	/// <returns>
	/// Group of intersecting GhostDestination.
	/// Returns Guid.Empty if none.
	/// </returns>
	Guid checkIntesectionAtPosition(Vector3 position)
	{
		// capsule is made up of two connected sphere
		Vector3 VectorToTopSphere = transform.up * (collider.height / 2 - collider.radius);
		Vector3 TopSpherePos = collider.center + VectorToTopSphere;
		Vector3 BottomSpherePos = collider.center - VectorToTopSphere;
		
		// check for interectiong GhostDestinations at position
		Collider[] intersections = Physics.OverlapCapsule(
			TopSpherePos,
			BottomSpherePos,
			collider.radius,
			Layers.GhostDestination.mask, // only collider with other GhostDestinations
			QueryTriggerInteraction.Collide // GhostDestinations use triggers, so we'd better hit then
		);

		if (intersections.Length > 0)
		{
			return intersections[0].GetComponent<GhostDestination>().group;
		}
		else
		{
			return Guid.Empty;
		}
	}
}