using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    public bool isClear = false;
    public bool isOver = false;
    private bool warncool = true;
    private bool speakcool = true;

    public float radius = 10f;
    public LayerMask layer;
    public Collider[] colliders;
    public Collider[] Groundcolliders;
    public Collider[] Objectcolliders;

    public bool isWalking;

    public GameObject warning;
    public float warningCooltime = 0f;

    public GameObject Speaking;
    public float speakingCooltime = 0f;

    public AudioSource walking;
    public AudioClip walkingClip;
    public AudioSource gameOver;

    float objDistance;
    Vector3 objLocation;

    private void Start()
    {
        isClear = false;
        isOver = false;
        walking = GetComponent<AudioSource>();
    }
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        Objectcolliders = Physics.OverlapSphere(transform.position, radius / 1.5f, layer);

        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                if (col.tag == "Enemy" && warncool)
                {
                    objDistance = Vector3.Distance(this.transform.position, col.transform.position);
                    objLocation = this.transform.position + (col.transform.position - this.transform.position) / objDistance;
                    Instantiate(warning, objLocation, Quaternion.identity);
                    warncool = false;
                    warningCooltime = 0.5f;
                }


            }
        }
        if(Objectcolliders.Length > 0)
        {
            foreach(Collider objcol in Objectcolliders)
            {
                if (objcol.tag != "Enemy" && objcol.tag != "Goal" && objcol.tag != "Ground" && objcol.tag != "Lake" && objcol.tag != "Road" && speakcool)
                {
                    objDistance = Vector3.Distance(this.transform.position, objcol.transform.position);
                    objLocation = this.transform.position + (objcol.transform.position - this.transform.position) / objDistance;
                    Instantiate(Speaking, objLocation, Quaternion.identity);
                    speakingCooltime = 1f;
                    speakcool = false;
                }
            }
        }


        if (isWalking)
        {
            if (!walking.isPlaying)
            {
                walking.PlayOneShot(walkingClip);
            }
        }
        warningCooltime -= Time.deltaTime;
        speakingCooltime -= Time.deltaTime;
        if(warningCooltime <= 0)
        {
            warncool = true;

        }
        if(speakingCooltime <= 0)
        {
            speakcool = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            isClear = true;
        }
        else if (collision.gameObject.tag == "Enemy" && !isOver)
        {
            isOver = true;
            gameOver.Play();
        }
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Road")
        {
            isWalking = true;
        }
    }
}
