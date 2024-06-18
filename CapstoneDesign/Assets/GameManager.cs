using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject canvas;

    public GameObject gameoverUI;

    public bool isGameover = false;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<MouseMove>().sensitivity = 0;
        player.GetComponent<CharacterMove>().moveSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            canvas.GetComponent<MenuController>().activePause(0);
            player.GetComponent<MouseMove>().sensitivity = 500;
            player.GetComponent<CharacterMove>().moveSpeed = 7;
        }

        if (isGameover == true)
        {
            player.GetComponent<CharacterMove>().moveSpeed = 0;
            player.GetComponent<MouseMove>().sensitivity = 0;
            canvas.GetComponent<MenuController>().activePause(1);
        }

        if(player.GetComponent<CollisionScript>().isClear == true)
        {
            canvas.GetComponent<MenuController>().Clear.SetActive(true);
            player.GetComponent<CharacterMove>().moveSpeed = 0;
            player.GetComponent<MouseMove>().sensitivity = 0;
        }
    }
}
