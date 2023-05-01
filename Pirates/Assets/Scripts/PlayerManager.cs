using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int maxHealth = 100;
    public static int currentHealth;
    //[SerializeField] Animator anim;
    [SerializeField] HealthBar healthBar; //reference to the health bar script
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource dialogueSource;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip death;
    [SerializeField] ScoreManager scoreManager;

    Rigidbody2D rigidbody;

    [SerializeField] GameObject bulletPrefab;

    [Header("Adjust")]
    [SerializeField] float turnSpeed;
    [SerializeField] float speed;
    [SerializeField] float dragStrength;
    [SerializeField] float shotCooldown;
    float rightShotCooldownTimer = 0;
    float leftShotCooldownTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth; //set the current health the max health
        healthBar.SetMaxHealth(maxHealth); //set the health bars max health through the SetMaxHealth method
    }

    private void Update()
    {
        //point the ship
        transform.rotation = Quaternion.FromToRotation(new Vector3(0, 1, 0), rigidbody.velocity);
        //shoot
        rightShotCooldownTimer -= rightShotCooldownTimer > 0 ? Time.deltaTime : 0;
        leftShotCooldownTimer -= leftShotCooldownTimer > 0 ? Time.deltaTime : 0;
        if (Input.GetKey(KeyCode.X) && rightShotCooldownTimer <= 0)
        {
            rightShotCooldownTimer = shotCooldown;
            Transform shooter = transform.Find("Right Shooter");
            Rigidbody2D bullet = Instantiate(bulletPrefab, shooter.position, transform.rotation).GetComponent<Rigidbody2D>();
            bullet.velocity = rigidbody.velocity;
            bullet.AddForce(transform.rotation * new Vector3(50, 150, 0));
        }
        if (Input.GetKey(KeyCode.Z) && leftShotCooldownTimer <= 0)
        {
            leftShotCooldownTimer = shotCooldown;
            Transform shooter = transform.Find("Left Shooter");
            Rigidbody2D bullet = Instantiate(bulletPrefab, shooter.position, transform.rotation).GetComponent<Rigidbody2D>();
            bullet.velocity = rigidbody.velocity;
            bullet.AddForce(transform.rotation * new Vector3(-50, 150, 0));
        }
    }

    private void FixedUpdate()
    {
        //turn
        rigidbody.velocity = Quaternion.Euler(0, 0, turnSpeed * -Input.GetAxis("Horizontal") * Time.fixedDeltaTime) * rigidbody.velocity;
        //add drag
        rigidbody.AddForce(-rigidbody.velocity.normalized * rigidbody.velocity.sqrMagnitude * dragStrength);

        //movement force
        float force = Mathf.Max(0, Input.GetAxis("Vertical")) * Time.fixedDeltaTime * speed;
        rigidbody.AddForce(transform.up * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet") //if the collision is with a bullet
        {
            Destroy(collision.gameObject);
            TakeEnemyDamage(5);
        }
        if (collision.gameObject.tag == "Dangerous Object")
            TakeEnemyDamage(34);
        if (collision.gameObject.tag == "Treasure")
        {
            Destroy(collision.gameObject);
            scoreManager.CollectTreasure();
        }
    }

    public void TakeEnemyDamage(int damage)
    {
        //play hit audio
        //audioSource.clip = hit;
        //audioSource.Play();

        //play hit animation
        //anim.SetTrigger("IsHit");
        currentHealth -= damage; //damage is taken from health

        healthBar.SetHealth(currentHealth); //set the healthbar health to the current health

        if (currentHealth <= 0) //if health becomes 0
        {
            Die(); //call the die function
        }
    }

    void Die()
    {
        //play death audio
        audioSource.clip = death;
        audioSource.Play();

        //load game over scene
        SceneManager.LoadScene("Game Over");
    }
}
