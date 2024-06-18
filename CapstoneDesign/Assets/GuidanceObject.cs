using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceObject : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Player;
    Vector3 goalLocation;
    Vector3 playerLocation;
    Vector3 objectLocation;
    public AudioSource wayThrough;
    public AudioSource warning;

    float initialDistance;
    float distance;
    public float sourceDistance;
    Vector2 vplayer2Goal;
    Vector3 vplayer2Audio;
    Vector3 playerRotation;

    public int gamestart = 0;

    private float Getangle(Vector3 v1, Vector3 v2)
    {
        return (Vector3.Dot(v1, v2) / (Vector3.Magnitude(v1) * Vector3.Magnitude(v2)));
    }
    void Start()
    {
        goalLocation = Goal.transform.position;
        initialDistance = Vector3.Magnitude(Player.transform.position - goalLocation);
    }

    void Update()
    {
        playerLocation = Player.transform.position;
        playerRotation = Player.transform.eulerAngles;
        distance = Vector3.Distance(goalLocation, playerLocation);
        objectLocation = playerLocation + sourceDistance * (goalLocation - playerLocation) / distance;
        this.transform.position = objectLocation;

        vplayer2Goal = new Vector3(Goal.transform.position.x - Player.transform.position.x, Goal.transform.position.z - Player.transform.position.z, 0);
        vplayer2Audio = new Vector3(Mathf.Sin(Player.transform.eulerAngles.y), Mathf.Cos(Player.transform.eulerAngles.y), 0);
        //        Debug.Log( "euler + " + playerRotation + "P2A " + vplayer2Audio.magnitude + "Angle " + Getangle(vplayer2Audio, vplayer2Goal));
        wayThrough.volume = (1.5f - distance / initialDistance) * gamestart;
    }
}
