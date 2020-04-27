using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public float mAttackRadius = 3f;
    private Transform mTarget;
    private NavMeshAgent mNavMesh;
    private Animator mAnim;
    private Boss mBossStat;
    private Player mPlayer;
     
    private float mAttackCoolDown = 1f;
    private float mDelaySec = .6f; 
    private EffectPool mEffectPool;
    private DamagePool mDamagePool;

    void Awake()
    {
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mEffectPool = GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectPool>();
        mDamagePool = GameObject.FindGameObjectWithTag("Effect").GetComponent<DamagePool>();
    }

    void Start()
    { 
        mTarget = mPlayer.transform;
        mNavMesh = GetComponent<NavMeshAgent>(); 
        mBossStat = GetComponent<Boss>();
        mAnim = GetComponent<Animator>(); 
    }
     
    public void BossAttack(CharacStat targetstat)
    {
        if (mAttackCoolDown <= 0)
        {

            if (mBossStat.mCurrHealth >= 40)
                StartCoroutine(BossAttackRoutine(targetstat, 0.6f));

            else if (mBossStat.mCurrHealth < 40 && mBossStat.mCurrHealth >= 15)
                StartCoroutine(BossAttackRoutine(targetstat, 0.5f));
            else if (mBossStat.mCurrHealth < 15 && mBossStat.mCurrHealth > 0)
                StartCoroutine(BossAttackRoutine(targetstat, 0.4f));

            // StartCoroutine(BossAttackRoutine(targetstat, mDelaySec)); 
            mAttackCoolDown = 1f / mBossStat.BossAttackSpeed(); 
        } 
    }

    IEnumerator BossAttackRoutine(CharacStat stat, float delay)
    {
        yield return new WaitForSeconds(delay); 
         
        if (mBossStat.mCurrHealth >= 40)
        {
            stat.TakeDamage(mBossStat.mDamage.GetValue());
            int damaged = mBossStat.mDamage.GetValue() - stat.mArmor.GetValue();
            damaged = Mathf.Clamp(damaged, 0, int.MaxValue);
            if(damaged>0)
            {
                DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                val.mDamagevalue.fontSize = 30;
                val.mDamagevalue.color = new Color(255, 0, 0);
                val.mDamagevalue.text = (-damaged).ToString();
                val.transform.position = mPlayer.GetComponentInChildren<Canvas>().transform.position;
            } 

            mAnim.SetTrigger(eBossState.IsAttack1.ToString());
            SoundController.Instance.PlayEffect((int)eEffectsound.BossAttack1);
            Effect effect = mEffectPool.GetFromPool((int)eEffectType.BossDamage);
            effect.transform.position =mPlayer.transform.position;
        }

        else if (mBossStat.mCurrHealth < 40 && mBossStat.mCurrHealth >= 15)
        {
            stat.TakeDamage(mBossStat.mDamage.GetValue()+2);
            int damaged = mBossStat.mDamage.GetValue()+2 - stat.mArmor.GetValue();
            damaged = Mathf.Clamp(damaged, 0, int.MaxValue);

            if(damaged>0)
            {
                DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                val.mDamagevalue.fontSize = 30;
                val.mDamagevalue.color = new Color(255, 0, 0);
                val.mDamagevalue.text = (-damaged).ToString();
                val.transform.position = mPlayer.GetComponentInChildren<Canvas>().transform.position;
            }

            mAnim.SetTrigger(eBossState.IsAttack2.ToString()); 
            SoundController.Instance.PlayEffect((int)eEffectsound.BossAttack2);
            Effect effect = mEffectPool.GetFromPool((int)eEffectType.BossDamage);
            effect.transform.position =mPlayer.transform.position;
        }

        else if (mBossStat.mCurrHealth < 15 && mBossStat.mCurrHealth > 0)
        {
            stat.TakeDamage(mBossStat.mDamage.GetValue()+3);
            int damaged = mBossStat.mDamage.GetValue()+3 - stat.mArmor.GetValue();
            damaged = Mathf.Clamp(damaged, 0, int.MaxValue);

            if(damaged>0)
            {
                DamageValueEffect val = mDamagePool.GetFromPool((int)eDamageEffect.Damageval);
                val.mDamagevalue.fontSize = 30;
                val.mDamagevalue.color = new Color(255, 0, 0);
                val.mDamagevalue.text = (-damaged).ToString();
                val.transform.position = mPlayer.GetComponentInChildren<Canvas>().transform.position;
            }

            mAnim.SetTrigger(eBossState.IsAttack3.ToString()); 
            SoundController.Instance.PlayEffect((int)eEffectsound.BossAttack3);
            Effect effect = mEffectPool.GetFromPool((int)eEffectType.BossDamage);
            effect.transform.position =mPlayer.transform.position;
        }
    }
    
    void Update()
    { 
        if (!mPlayer.GetComponent<PlayerStats>().IsDead)
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
            mNavMesh.SetDestination(mTarget.position);

            if (playerdistance <= mNavMesh.stoppingDistance)
            { 
                CharacStat targetstat = mTarget.GetComponent<CharacStat>();
                if (targetstat != null)
                { 
                    mAnim.SetBool(eBossState.IsWalk.ToString(), true);
                    BossAttack(targetstat); 
                }
                FaceTarget(); 
            }
        }
        else
        {
            mAnim.SetBool(eBossState.IsWalk.ToString(), false);
        }
    }
    private void FaceTarget()
    {
        Vector3 dir = (mTarget.position - transform.position).normalized;
        Quaternion lookat = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookat, Time.deltaTime * 5f); 
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mAttackRadius);//몬스터 공격범위 시각화
    }
}
