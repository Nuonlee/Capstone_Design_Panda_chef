using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonVehicle : MonoBehaviour
{
    public float x, y, z;
    public float speed;
    public float distance;

    public Collider[] colliders;
    public float radius = 10f;
    public LayerMask layer;
    private AudioSource audiosource;

    Vector3 startpoint;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = new Vector3(x, y, z);
        this.transform.position = startpoint;
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, radius, layer);
        if (colliders.Length > 0)
        {
            foreach(Collider col in colliders)
            {
                if(col.tag == "Player")
                {
                    audiosource.PlayOneShot(audiosource.clip);
                }
            }
        }
        this.transform.position += new Vector3(0, 0, speed);
        if(speed < 0)
        {
            if(this.transform.position.z <= 100)
            {
                this.transform.position = startpoint;
            }
        }
        else
        {
            if(this.transform.position.z >= 350)
            {
                this.transform.position = startpoint;
            }
        }
    }
}
