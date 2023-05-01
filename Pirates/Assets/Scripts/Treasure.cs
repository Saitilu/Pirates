using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    float time = 0;
    Rigidbody2D rigidbody;
    [SerializeField] GameObject push;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        //add drag
        rigidbody.AddForce(-rigidbody.velocity.normalized * rigidbody.velocity.sqrMagnitude * .15f);
        if (time > .3f)
        {
            Destroy(push);
            this.enabled = false;
        }
    }
}
