using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonVehicle : MonoBehaviour
{
    public float x, y, z;
    public float speed;
    public float distance;

    Vector3 startpoint;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = new Vector3(x, y, z);
        this.transform.position = startpoint;
    }

    // Update is called once per frame
    void Update()
    {

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
