using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Player mPlayer;
    Vector3 offset = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - mPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mPlayer.transform.position + offset;
    }
}
