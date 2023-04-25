using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Gravity")]
    [SerializeField] float range;
    [SerializeField] float strength;
    float minValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        range *= 2 * transform.localScale.x;
        strength *= 2 * transform.localScale.x;
        minValue *= 2 * transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, range); //find all objects in gravitational field

        foreach (Collider2D collider in objectsInRange)
        {
            float effect;
            if (collider.gameObject.tag == "Player")
                effect = Input.GetKey(KeyCode.R) ? -.5f : 1; //take input for player
            else
                effect = 1;

            if (collider.gameObject.layer == 6) //if effected by gravity
            {
                float distance = Mathf.Max(Vector3.Distance(transform.position, collider.transform.position), minValue);
                Vector3 direction = (transform.position - collider.transform.position).normalized;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(effect * direction * (range / Mathf.Pow(distance, 2)) * strength * Time.fixedDeltaTime);
            }
        }
    }
}
