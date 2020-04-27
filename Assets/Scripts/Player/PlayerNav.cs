using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNav : MonoBehaviour
{
    public NavMeshAgent mNavagent; 
    public Transform mTarget;//따라갈 타겟 
  
    void Start()
    {
        mNavagent = GetComponent<NavMeshAgent>(); 
    }

    void Update()
    {
        if(mTarget!=null)
        {
            mNavagent.SetDestination(mTarget.position);
            FaceTarget(); 
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        mNavagent.SetDestination(point);
    }

    public void FollowTarget(Interactable target)
    {
        mNavagent.stoppingDistance = target.radius * .8f;// 일정거리 밖에서 멈추게 함
        mNavagent.updateRotation = false;//일정거리 내이면 그쪽 안보는거 방지
        mTarget = target.mInteractPos;
    }

    public void StopFollowingTarget()
    {
        mNavagent.stoppingDistance = 0;
        mNavagent.updateRotation = true;
        mTarget = null;
    }

    public void FaceTarget()
    {
        Vector3 dir = (mTarget.position - transform.position).normalized;//타겟 향한 방향
        Quaternion lookrotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));//방향을 보기위한 로테이션
        Debug.Log(lookrotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 5f);
    }
}
