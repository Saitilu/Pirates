using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelState : AIState
{
    float time;
    public override void EnterState(EnemyAI parent)
    {
        Debug.Log("making Travel");
        this.parent = parent;
        forward = true;
        horizontal = 0;
        time = Random.Range(1, 2);
    }
    public override AIState Update()
    {
        Debug.Log("Update Travel");
        time -= Time.fixedDeltaTime;
        if (time <= 0)
            return parent.wanderState;
        return null;
    }
}
