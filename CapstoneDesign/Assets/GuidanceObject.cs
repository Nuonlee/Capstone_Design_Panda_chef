using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceObject : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Player;
    public AudioSource wayThrough;
    public AudioSource warning;

    float initialDistance;
    float distance;
    public float sourceDistance;

    Vector3 goalLocation;
    Vector3 playerLocation;
    Vector3 objectLocation;

    Vector2 vplayer2Goal;
    Vector3 vplayer2Audio;
    Vector3 playerRotation;

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

        vplayer2Goal = new Vector3(Goal.transform.position.x - Player.transform.position.x, 0.0f, Goal.transform.position.z - Player.transform.position.z);
        vplayer2Audio = new Vector3(Mathf.Sin(Player.transform.eulerAngles.x), 0, Mathf.Cos(Player.transform.eulerAngles.x));

        //Debug.Log("g " + vplayer2Goal + " A " + vplayer2Audio + " angle" + Getangle(vplayer2Audio, vplayer2Goal));

        wayThrough.volume = 1.5f - distance / initialDistance;
    }
}
