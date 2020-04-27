using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class MonsterController : MonoBehaviour
{
    public float mAttackRadius = 3f;
    private Transform mTarget;
    private NavMeshAgent mNavMesh;
    private Animator mAnim;
    private MonsterStats mMonsterStat;
    private Player mPlayer; 
    private float mAttackCoolDown = 1f;
    private float mDelaySec = .6f; 

    private EffectPool mEffectPool;
    private DamagePool mDamagePool;
    private PlayerStats mPlayerstat;

    void Awake()
    { 
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); 
    }

    void Start()
    { 
        mTarget = mPlayer.transform;
        mNavMesh = GetComponent<NavMeshAgent>();
        mEffectPool = GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectPool>();
        mDamagePool = GameObject.FindGameObjectWithTag("Effect").GetComponent<DamagePool>();
        mMonsterStat = GetComponent<MonsterStats>();
         
        mAnim = GetComponent<Animator>(); 
    }

    public void MonsterAttack(CharacStat targetstat)
    {
        if (mAttackCoolDown <= 0)
        {
            StartCoroutine(MonsterAttackRoutine(targetstat, mDelaySec)); 
            mAttackCoolDown = 1f / mMonsterStat.MonsterAttackSpeed(); 
        } 
    }

    IEnumerator MonsterAttackRoutine(CharacStat stat, float delay)
    {
        yield return new WaitForSeconds(delay);

        SoundController.Instance.PlayEffect((int)eEffectsound.Damage);
        stat.TakeDamage(mMonsterStat.mDamage.GetValue());
        int damaged = mMonsterStat.mDamage.GetValue() - stat.mArmor.GetValue();
        damaged = Mathf.Clamp(damaged, 0, int.MaxValue);

        if(damaged>0)
        {
            DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
            val.mDamagevalue.fontSize = 30;
            val.mDamagevalue.color = new Color(255, 0, 0);
            val.mDamagevalue.text = (-damaged).ToString();
            val.transform.position = mPlayer.GetComponentInChildren<Canvas>().transform.position;
        }

        Effect effect = mEffectPool.GetFromPool((int)eEffectType.Damage);
        effect.transform.position = mPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!mPlayer.GetComponent<PlayerStats>().IsDead)
        {
            AttackPlayer(); 
        }
        mAttackCoolDown -= Time.deltaTime; 
    }

    private void AttackPlayer()
    {
        float playerdistance = Vector3.Distance(mTarget.position, transform.position);
        if (playerdistance <= mAttackRadius)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            mNavMesh.SetDestination(mTarget.position);
            if (playerdistance <= mNavMesh.stoppingDistance)
            { 
                CharacStat targetstat = mTarget.GetComponent<CharacStat>();
                if (targetstat != null)
                {
                  //  AnimationController.Instance.mAnimator.SetBool(ePlayerAnimation.IsWalk.ToString(), false);
                    Vector3 dir = (transform.position-targetstat.transform.position).normalized;
                    Quaternion lookat = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                    targetstat.transform.rotation = Quaternion.Slerp(targetstat.transform.rotation, lookat, Time.deltaTime * 2f);


                    MonsterAttack(targetstat); 
                    mAnim.SetTrigger(eMonsterState.IsAttack.ToString()); 
                }
                FaceTarget(); 
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 dir = (mTarget.position - transform.position).normalized;
        Quaternion lookat = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookat,Time.deltaTime*5f); 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mAttackRadius);//몬스터 공격범위 시각화
    } 
}
