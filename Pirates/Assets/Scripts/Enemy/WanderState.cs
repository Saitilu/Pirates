using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WanderState : AIState
{
    Vector3 startRot;
    float goalAngle;
    public override void EnterState(EnemyAI parent)
    {

        this.parent = parent;
        forward = true;
        startRot = parent.transform.up;
        goalAngle = Random.Range(10f, 90f) * (Random.Range(0, 2) == 1 ? -1 : 1);
    }
    public override AIState Update()
    {

        float amountTurned = Quaternion.FromToRotation(startRot, parent.transform.up).eulerAngles.z;
        amountTurned = ((amountTurned + 180) % 360) - 180;
        bool notDone = Mathf.Abs(amountTurned) < Mathf.Abs(goalAngle);

        if (notDone)
        {
            horizontal = -Mathf.Sign(goalAngle);
            return null;
        }

        return parent.travelState;
    }
}
