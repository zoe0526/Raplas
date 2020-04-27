using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Road : MonoBehaviour
{
    [SerializeField]
    public GameObject mRoad;
    private Player mPlayer;
    void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
     
    void Update()
    {
        if(mPlayer.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            mRoad.SetActive(true);
        }
        if(!mPlayer.gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            mRoad.SetActive(false);
        }
    }
}
