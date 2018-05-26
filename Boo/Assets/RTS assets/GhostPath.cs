using System.Collections;using System.Collections.Generic;using UnityEngine;using Pathfinding;public class GhostPath : AIPath {    private Transform _target;

    // ---------- PUBLIC ----------

    public void SetDestination(Vector3 destination)    {        this.destination = destination;        _target = null;    }    public void SetTarget(Transform target)    {        _target = target;    }    // ---------- PRIVATE ----------    override protected void OnEnable()    {        base.OnEnable();        // Update the destination right before searching for a path as well.        // This is enough in theory, but this script will also update the destination every        // frame as the destination is used for debugging and may be used for other things by other        // scripts as well. So it makes sense that it is up to date every frame.        onSearchPath += UpdateDestination;    }    override protected void OnDisable()    {        base.OnDisable();        onSearchPath -= UpdateDestination;    }    override protected void Update()
    {
        base.Update();
        UpdateDestination();
    }    void UpdateDestination()    {        if (_target != null)        {            destination = _target.position;        }    }}