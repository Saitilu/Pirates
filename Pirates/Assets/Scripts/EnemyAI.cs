using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private AIState currentState;

    //[SerializeField] Animator anim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource dialogueSource;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;

    Rigidbody2D rigidbody;

    [SerializeField] GameObject bulletPrefab;

    [Header("Adjust")]
    [SerializeField] float turnSpeed;
    [SerializeField] float speed;
    [SerializeField] float dragStrength;

    [Header("States")]
    [SerializeField] public TravelState travelState;
    [SerializeField] public WanderState wanderState;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = new TravelState();
        currentState.EnterState(this);
    }

    private void Update()
    {


        //point the ship
        transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), rigidbody.velocity);
        //shoot
        if (Input.GetKeyDown("space"))
        {
            Transform shooter = transform.Find("Shooter");
            Instantiate(bulletPrefab, shooter.position, transform.rotation * Quaternion.Euler(0, 0, 180));
        }
    }

    private void FixedUpdate()
    {
        AIState newState = currentState.Update();
        //Debug.Log(newState);
        if (newState != null)
        {
            Debug.Log("///////////////////////////////////////////////////////////////////////////////////////////////////");
            currentState = newState;
            currentState.EnterState(this);
        }
        //turn
        rigidbody.velocity = Quaternion.Euler(0, 0, turnSpeed * -currentState.horizontal * Time.fixedDeltaTime) * rigidbody.velocity;
        //add drag
        rigidbody.AddForce(-rigidbody.velocity.normalized * rigidbody.velocity.sqrMagnitude * dragStrength);

        //movement force
        float force = (currentState.forward ? 1 : 0) * Time.fixedDeltaTime * speed;
        rigidbody.AddForce(transform.up * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //if the collision is with a bullet
        {
            Destroy(collision.gameObject);
            Die();
        }
    }

    void Die()
    {
        //play death audio
        audioSource.clip = death;
        audioSource.Play();
        //Die
        Destroy(gameObject);
    }
    //public void TakeEnemyDamage(int damage)
    //{
    //    //play hit audio
    //    audioSource.clip = hit;
    //    audioSource.Play();

    //    //play hit animation
    //    //anim.SetTrigger("IsHit");
    //    currentHealth -= damage; //damage is taken from health

    //    healthBar.SetHealth(currentHealth); //set the healthbar health to the current health

    //    if (currentHealth <= 0) //if health becomes 0
    //    {
    //        Die(); //call the die function
    //    }
    //}
}
