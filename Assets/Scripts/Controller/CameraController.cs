using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{ 
    private Transform target;
    [SerializeField]
    public float mDistance=5f;//z
    [SerializeField]
    public float mHeight = 8f;//y
    [SerializeField]
    public float mSmoothangle=1f;
    private Transform mCamera;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        mCamera = GetComponent<Transform>();
    }

    void Update()
    {
        float angle = Mathf.LerpAngle(mCamera.eulerAngles.y, target.eulerAngles.y, mSmoothangle * Time.deltaTime);
        Quaternion rotate = Quaternion.Euler(0, angle, 0);
        mCamera.position = target.position - (rotate * Vector3.forward * mDistance) + (Vector3.up * mHeight);
    }
}
