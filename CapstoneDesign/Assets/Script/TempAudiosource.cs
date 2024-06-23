using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAudiosource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
        Destroy(gameObject, 1f);
    }
}
