using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject canvas;

    Scene scene;

    public Vector3 playerStartposition;

    public bool isGameover = false;

    public bool clear = false;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        player.GetComponent<MouseMove>().sensitivity = 0;
        player.GetComponent<CharacterMove>().moveSpeed = 0;
        player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
        playerStartposition = player.transform.position;
        player.GetComponent<CollisionScript>().isOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerStartposition);
        if (Input.anyKeyDown && !player.GetComponent<CollisionScript>().isOver && !player.GetComponent<CollisionScript>().isClear)
        {
            canvas.GetComponent<MenuController>().activePause(0);
            player.GetComponent<MouseMove>().sensitivity = 500;
            player.GetComponent<CharacterMove>().moveSpeed = 6;
            player.GetComponentInChildren<GuidanceObject>().gamestart = 1;
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 1;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 1;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 1;

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
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 0;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 0;
            canvas.GetComponent<MenuController>().pause.SetActive(true);
        }

        if (player.GetComponent<CollisionScript>().isOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(scene.name);
        }

        if (player.GetComponent<CollisionScript>().isClear == true)
        {
            player.GetComponent<CharacterMove>().moveSpeed = 0;
            player.GetComponent<MouseMove>().sensitivity = 0;
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 0;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 0;

            if(scene.name == "Stage1")
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.GetComponent<CollisionScript>().isClear = false;
                    player.GetComponent<CollisionScript>().isOver = false;
                    SceneManager.LoadScene("Stage2");
                }
            }

            if(scene.name == "Stage2")
            {
                canvas.GetComponent<MenuController>().Clear.SetActive(true);
            }
        }


    }
}
