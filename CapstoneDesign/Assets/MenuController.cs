using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject black;
    public GameObject start;
    public GameObject Clear;

    public bool blackOn = false;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.GetChild(2).gameObject;
        Clear = transform.GetChild(4).gameObject;
        black = transform.GetChild(3).gameObject;
        black.SetActive(blackOn);
    }

    public void activePause(int i)
    {
        if(i == 1)
        {
            start.SetActive(true);
        }
        else if(i == 0)
        {
            start.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong input : " + i);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
