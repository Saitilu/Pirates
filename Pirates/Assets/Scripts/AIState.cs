using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIState : MonoBehaviour
{
    public bool forward { get; protected set; }
    public float horizontal { get; protected set; }
    abstract public void EnterState(EnemyAI parent);
    abstract public AIState Update();

    protected EnemyAI parent;
}


