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

    public bool isWalking;

    public GameObject warning;
    public float warningCooltime = 0f;

    public GameObject Speaking;
    public float speakingCooltime = 0f;

    public AudioSource walking;
    public AudioSource gameOver;

    float objDistance;
    Vector3 objLocation;

    private void Start()
    {
        isClear = false;
        isOver = false;
    }
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layer);

        if(colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                if (col.tag == "Enemy" && warncool)
                {
                    Debug.Log(col.name + "Enemy detected");
                    objDistance = Vector3.Distance(this.transform.position, col.transform.position);
                    objLocation = this.transform.position + (col.transform.position - this.transform.position) / objDistance;
                    Instantiate(warning, objLocation, Quaternion.identity);
                    warncool = false;
                    warningCooltime = 0.5f;
                }
                else if(col.tag != "Enemy" && col.tag != "Goal" && col.tag != "Ground" && col.tag != "Lake" && col.tag != "Road" && speakcool)
                {
                    objDistance = Vector3.Distance(this.transform.position, col.transform.position);
                    objLocation = this.transform.position + (col.transform.position - this.transform.position) / objDistance;
                    Instantiate(Speaking, objLocation, Quaternion.identity);
                    speakingCooltime = 1f;
                    speakcool = false;
                }
            }
        }

        if (isWalking && !walking.isPlaying)
        {
            Debug.Log("walk");
            walking.Play();
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            isWalking = true;
        }
        else if (collision.gameObject.CompareTag("Grounnd"))
        {
            isWalking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road"))
        {
            isWalking = false;
        }
        else if (collision.gameObject.CompareTag("Grounnd"))
        {
            isWalking = false;
        }
    }

}
