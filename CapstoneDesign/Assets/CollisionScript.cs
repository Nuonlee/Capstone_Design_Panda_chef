using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    public bool isClear = false;
    public bool isOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision active");
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            isClear = true;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Crushed");
            isOver = true;
        }
    }
}
