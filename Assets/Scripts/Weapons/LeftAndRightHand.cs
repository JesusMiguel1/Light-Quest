using object_pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRightHand : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip hitAudioClip;
    //public AudioClip policeAudioClips;
    private AudioManager audioManager;

    public TrailRenderer bulletTrace;

    [SerializeField] private Transform rightHandSpawner;
    [SerializeField] private Transform leftHandSpawner;
    GlobalBool gbool;
    //public Grenade grenadeScript; 

    GameObject explosion;

    private HashSet<string> allowedNames;

    private RaycastHit hit;
    GlobalStrings strings;
    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioManager = GetComponent<AudioManager>();

    }

    void Start()
    {
        strings = new GlobalStrings();
        gbool = new GlobalBool();
        allowedNames = new HashSet<string> {
                strings.slapperClone,
                strings.wanderDroneclone,
                "Powerup",
                strings.EnemyTrigger,
                strings.policeInspectorClone,

            };

       
    }
    public void RightHandShood()
    {
        var trace = Instantiate(bulletTrace, rightHandSpawner.position, Quaternion.identity);
        trace.AddPosition(rightHandSpawner.position);

        if (Physics.Raycast(rightHandSpawner.position, rightHandSpawner.forward, out hit))
        {
            GameObject bullet = BulletsPoolManager.Instance.GetBullet();
            bullet.transform.position = rightHandSpawner.position;
            bullet.transform.rotation = rightHandSpawner.rotation;
            //Debug.DrawRay(rightHandSpawner.position, rightHandSpawner.forward * hit.distance, Color.green) ;
            trace.transform.position = hit.point;

            if (hit.collider.gameObject.name == strings.slapperClone)
            {
                AudioManager.Instance.PlayOneShot(hitAudioClip);

                audioManager = GetComponent<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.PlayOneShot(hitAudioClip);
                }
                //FindObjectOfType<MainAudioManager>().PlaySound("Shoot");
            }

            if (allowedNames.Contains(hit.collider.name))
            {
                hit.collider.gameObject.SetActive(false);
                gbool.inspectorDisabled = true;
                ColorfullExplosion();

            }



            //GameObject grenade = BulletsPoolManager.Instance.GetGrenade();
            //grenade.transform.position = rightHandSpawner.position;
            //grenade.transform.rotation = rightHandSpawner.rotation;

            //grenade.GetComponent<Rigidbody>().AddForce( rightHandSpawner.forward * range, ForceMode.Impulse);


        }
    }

    public void LeftHandShood()
    {
        var trace = Instantiate(bulletTrace, leftHandSpawner.position, Quaternion.identity);
        trace.AddPosition(leftHandSpawner.position);
        if (Physics.Raycast(leftHandSpawner.position, leftHandSpawner.forward, out hit))
        {
            GameObject bullet = BulletsPoolManager.Instance.GetBullet();
            bullet.transform.position = leftHandSpawner.position;
            bullet.transform.rotation = leftHandSpawner.rotation;
            trace.transform.position = hit.point;

            if (hit.collider.gameObject.name == strings.slapperClone)
            {
                AudioManager.Instance.PlayOneShot(hitAudioClip);

                //audioManager = GetComponent<AudioManager>();
                if (audioManager != null)
                {
                    audioManager.PlayOneShot(hitAudioClip);
                }
            }
            if (allowedNames.Contains(hit.collider.name))
            {
                //Debug.Log($"<b>Collision </b> <color=red> <b>{other.gameObject.name}</b> </color>");
                hit.collider.gameObject.SetActive(false);
                ColorfullExplosion();

            }
        }
    }



    #region INSTANTIATING EXPLOSION USING BULLET POOL
    void ColorfullExplosion()
    {
        explosion = DestructiblePoolManager.Instance.GetPieces();
        explosion.transform.position = hit.point;

    }
    #endregion
}
