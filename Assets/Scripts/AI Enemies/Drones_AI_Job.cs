using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drones_AI_Job : MonoBehaviour
{
    private GameObject player;
    private Vector3 movement;
    float speed = 20f;
    GameObject explosion;
    [SerializeField] private GameObject siren;
    // Start is called before the first frame update
    void Start()
    {
        player = Resources.Load("Player", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(player.transform.position);
        if (player != null)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            float randomAngle = Random.Range(-45f, 45f);
            Quaternion randomRotation = Quaternion.Euler(0f, randomAngle, 0f);
            Vector3 randomDirection = randomRotation * directionToPlayer;
            transform.Translate(randomDirection * speed * Time.deltaTime);

            float strikeDistance = Random.Range(3f, 7f);
            Vector3 strikePosition = player.transform.position + (directionToPlayer * strikeDistance);
            //Quaternion rotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            //transform.rotation = rotation;

            transform.rotation = Quaternion.LookRotation(directionToPlayer);

            Vector3 moveToPlayer = player.transform.position - transform.position;
            //transform.Translate(moveToPlayer * 5f * Time.deltaTime);
        }
        StartCoroutine(DroneSiren());
    }

    IEnumerator DroneSiren()
    {
        yield return new WaitForSeconds(.3f);
        siren.SetActive(true);

        yield return new WaitForSeconds(.3f);
        siren.SetActive(false);
        StopAllCoroutines();
    }
    void OnCollisionEnter(Collision other)

    {

        if (other.gameObject.name == "Player(Clone)")
        {
            Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
            gameObject.SetActive(false);
            ColorfullExplosion();

        }
        if (other.gameObject.name == "Bullet(Clone)")
        {
            //AudioManager.Instance.PlayOneShot(audioClips);
            //// int clipsIndex = Random.Range(0, audioClips.Length);
            //if (audioManager != null)
            //{
            //    audioManager.PlayOneShot(audioClips);
            //}
        }
    }

    void ColorfullExplosion()
    {
        explosion = DestructiblePoolManager.Instance.GetExplosionPieces();
        explosion.transform.position = transform.position;

    }
}
