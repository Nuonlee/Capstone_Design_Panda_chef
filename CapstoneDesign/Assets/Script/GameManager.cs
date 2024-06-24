using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject goal;
    public GameObject canvas;

    public AudioClip gamestart;
    public AudioClip stageclear;
    public AudioClip gameclear;
    public AudioClip restart;

    private AudioSource audio;
    private AudioSource audioclear;
    private AudioSource audiorestart;
    private bool clearheard = false;
    Scene scene;

    public Vector3 playerStartposition;

    public bool isGameover = false;

    public bool clear = false;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<MouseMove>().sensitivity = 0;
        player.GetComponent<CharacterMove>().moveSpeed = 0;
        player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 0;
        playerStartposition = player.transform.position;
        player.GetComponent<CollisionScript>().isOver = false;
        player.GetComponent<CollisionScript>().isWalking = false;
        player.GetComponent<CollisionScript>().walking.volume = 0.6f;
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(gamestart);
        audioclear = GetComponent<AudioSource>();
        audiorestart = GetComponent<AudioSource>();
        clearheard = false;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        if (Input.anyKeyDown && !player.GetComponent<CollisionScript>().isOver && !player.GetComponent<CollisionScript>().isClear)
        {
            canvas.GetComponent<MenuController>().activePause(0);
            player.GetComponent<MouseMove>().sensitivity = 500;
            player.GetComponent<CharacterMove>().moveSpeed = 5;
            player.GetComponentInChildren<GuidanceObject>().gamestart = 1;
            player.GetComponentInChildren<GuidanceObject>().wayThrough.volume = 1;
            player.GetComponent<CollisionScript>().warning.GetComponent<AudioSource>().volume = 0.5f;
            player.GetComponent<CollisionScript>().Speaking.GetComponent<AudioSource>().volume = 1;
            player.GetComponent<CollisionScript>().isWalking = true;
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
            player.GetComponent<CollisionScript>().isWalking = false;
            if (!audiorestart.isPlaying && !clearheard)
            {
                audiorestart.PlayOneShot(restart);
                clearheard = true;
            }
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
            player.GetComponent<CollisionScript>().isWalking = false;

            if (scene.name == "Stage1")
            {
                canvas.GetComponent<MenuController>().stage1Clear.SetActive(true);
                if (!audioclear.isPlaying && !clearheard)
                {
                    audioclear.PlayOneShot(stageclear);
                    clearheard = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.GetComponent<CollisionScript>().isClear = false;
                    player.GetComponent<CollisionScript>().isOver = false;
                    SceneManager.LoadScene("Stage2");
                }
            }

            if(scene.name == "Stage2")
            {
                if (!audioclear.isPlaying && !clearheard)
                {
                    audioclear.PlayOneShot(gameclear);
                    clearheard = true;
                }
                canvas.GetComponent<MenuController>().Clear.SetActive(true);
            }
        }

    }
}
