using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPos : MonoBehaviour
{ 
    private Player mPlayer;
    private Camera mCamera;
    private Transform mStartPos; 

    void Awake()
    { 
        mStartPos = GetComponent<Transform>();
        mStartPos = gameObject.transform;
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        mPlayer.gameObject.transform.position = mStartPos.position;
        mPlayer.gameObject.transform.rotation = mStartPos.rotation;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        mPlayer.mCam = mCamera;
    }
}
