using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    //variables
    public TextMeshProUGUI UIHealth; //UI text element
    public HealthBar healthBar;
    public AudioSource audioSource;
    public AudioClip pickup;

    // Update is called once per frame
    void Update()
    {
        UIHealth.text = PlayerManager.currentHealth.ToString() + "/" + PlayerManager.maxHealth.ToString(); //display it in the UI text
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if the player touches the health
        if (col.tag == "Health")
        {
            audioSource.clip = pickup;
            audioSource.Play(); 

            int health = Random.Range(5, 15);
            if (PlayerManager.currentHealth >= 100)
            {
                healthBar.SetHealth(PlayerManager.currentHealth);
            }
            else
            {
                PlayerManager.currentHealth += health;
                healthBar.SetHealth(PlayerManager.currentHealth += health);
            }
            Destroy(col.transform.parent.gameObject); //remove the health from the scene
        }
    }
}
