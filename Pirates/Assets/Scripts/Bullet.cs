using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
        rigidbody.velocity = transform.up * speed;
        if (OffScreen())
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private bool OffScreen()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        return screenPos.x < -.1 || screenPos.x > 1.1 || screenPos.y < -.1 || screenPos.y > 1.1;
    }
}
