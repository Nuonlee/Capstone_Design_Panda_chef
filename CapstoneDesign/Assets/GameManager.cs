using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject canvas;


    public bool isGameover = false;

    int clear = 0;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<MouseMove>().sensitivity = 0;
        player.GetComponent<CharacterMove>().moveSpeed = 0;
        player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            canvas.GetComponent<MenuController>().activePause(0);
            player.GetComponent<MouseMove>().sensitivity = 500;
            player.GetComponent<CharacterMove>().moveSpeed = 3;
            player.GetComponentInChildren<GuidanceObject>().gamestart = 1;
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 1;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!canvas.GetComponent<MenuController>().blackOn)
            {
                canvas.GetComponent<MenuController>().black.SetActive(true);
                canvas.GetComponent<MenuController>().blackOn = true;
            }
            else
            {
                canvas.GetComponent<MenuController>().black.SetActive(false);
                canvas.GetComponent<MenuController>().blackOn = false;
            }
        }

        if (player.GetComponent<CollisionScript>().isOver)
        {
            player.GetComponent<CharacterMove>().moveSpeed = 0;
            player.GetComponent<MouseMove>().sensitivity = 0;
            canvas.GetComponent<MenuController>().activePause(1);
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 0;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 0;            
        }

        if(player.GetComponent<CollisionScript>().isClear == true)
        {
            canvas.GetComponent<MenuController>().Clear.SetActive(true);
            player.GetComponent<CharacterMove>().moveSpeed = 0;
            player.GetComponent<MouseMove>().sensitivity = 0;
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 0;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 0;
            clear++;

            if(clear == 1 && Input.anyKeyDown)
            {
                SceneManager.LoadScene("Stage2");
            }
            player.GetComponent<CollisionScript>().isClear = false;
        }
    }
}
