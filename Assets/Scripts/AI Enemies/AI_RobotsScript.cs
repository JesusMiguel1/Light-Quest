using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_RobotsScript : MonoBehaviour
{
    public float moveSpeed;
    Transform player; // Reference to the player's transform
    GameObject explosion;

    private bool isMoving = false;

    public AudioClip audioClips;

    private AudioManager audioManager;

    void Start()
    {
        // Find the player object in the scene if not assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (!isMoving)
        {
            // Move towards the player if not already moving
            MoveTowardsPlayer();
            //transform.LookAt(player.transform.position);
        }
    }

    void MoveTowardsPlayer()
    {
        isMoving = true;

        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move towards the player
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Check if the unit has reached close to the player
        if (Vector3.Distance(transform.position, player.position) > 1f)
        {
            // Stop moving
            isMoving = false;
        }
    }
    void OnCollisionEnter(Collision other)

    {
        if (other.gameObject.name == "Player")
        {
            //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
            gameObject.SetActive(false);
            ColorfullExplosion();

        }
        if (other.gameObject.name == "Bullet(Clone)")
        {
            AudioManager.Instance.PlayOneShot(audioClips);
           
        }
    }

    void ColorfullExplosion()
    {
        explosion = DestructiblePoolManager.Instance.GetPieces();
        explosion.transform.position = transform.position;

    }
}
