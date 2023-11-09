using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quests
{

[System.Serializable]
public class QuestMark
{
    [field:SerializeField] public List<QuestTarget> Targets {get; private set;}

    private bool tracking; // This instance tracking something now?

    [Tooltip("Check distance between multiple targets every n seconds")]
    [SerializeField] private float positionChecksInterval = 1;

    // Should we track distance between multiple targets or just track one?
    private bool SingleTarget { get => Targets.Count == 1; }
    private bool initialized = false; // Did we inject this object into QuestTargets?

    
    [SerializeField] private QuestPointerController pointerController; // TODO: get reference in case of multuple players.

    private QuestTarget closest;
    private QuestTarget previousClosest;
    
    private float PreviousDistance
    {
        get
        {
            if (closest == null) { return Mathf.Infinity; }
            return Vector3.Distance(closest.transform.position, pointerController.transform.position);
        }
    }
    


    public void StartTracking()
    {
        if (tracking) { return; }

        if (!initialized)
        {
            foreach (var target in Targets)
            {
                target.Initialize(this);
            }
            initialized = true;
        }

        previousClosest = null; // Reset previous so we can track it in case of disabling/re-enabling tracking
        tracking = true;
        pointerController.StartCoroutine(TrackMultipleTargers());
    }


    private IEnumerator TrackMultipleTargers() // TODO: async/await
    {
        while (tracking) // It'll stops automatically when we stop tracking.
        {
            SetClosestTarget();

            yield return new WaitForSeconds(positionChecksInterval); // Optimization so it won't iterate every frame.
        }
    }


    private void SetClosestTarget()
    {
        if (Targets.Count == 0) { StopTracking(); }
        if (!tracking) { return; }

        QuestTarget currentClosest = FindClosestTarget();

        // If closest changed - change target.
        if (previousClosest != currentClosest)
        {
            pointerController.SetPointerTarget(currentClosest.transform);
            previousClosest = currentClosest;
        }
    }

    private QuestTarget FindClosestTarget()
    {
        if (SingleTarget) { return Targets[0]; } // One target.

        Transform tracker = pointerController.transform;

        for(int i = 0; i < Targets.Count; i++)
        {
            var target = Targets[i];

            float currentDistance = Vector3.Distance(target.transform.position, tracker.position);

            if (currentDistance <= PreviousDistance)
            {
                closest = target;
            }
        }

        return closest;
        
    }


    public void RemoveTarget(QuestTarget target) // This called by target is case of diasbling / destroy / etc..
    {
        Targets.Remove(target);
        if (closest == target) { closest = null; }
        SetClosestTarget();
    }


    public void StopTracking()
    {
        if (!tracking) { return; }

        tracking = false;
        
        if (pointerController != null) pointerController.StopPointer();
    }

}

}
