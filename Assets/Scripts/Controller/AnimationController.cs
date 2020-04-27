using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerAnimation
{
    IsIdle,
    IsWalk,
    IsRun,
    IsJump,
    IsDamage,
    IsAttack,
    IsDead
}


public enum eMonsterState
{
    IsIdle,
    IsAttack,
    IsRun,
    IsDamage,
    IsDie
}

public enum eBossState
{
    IsWalk,
    IsAttack1,
    IsAttack2,
    IsAttack3,
    IsBlock,
    IsDamage,
    IsReady,
    IsDodge,
    IsDie
}

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    public Animator mAnimator;
    [SerializeField]
    public Player mPlayer;

    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

      mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
         mAnimator = mPlayer.GetComponent<Animator>();
    }

    public void SkillAttack(CharacStat target, int id) //스킬공격
    {
        if (mPlayer.GetComponent<PlayerStats>().mDamage.GetValue()!=0)
        {
            target.TakeDamage(SkillButton.Instance.mSkillDataArr[id].AttackValue);
            
            ActivateSkill(id);
        }
    }

    private IEnumerator SetCoolTime(int id)
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        SkillButton.Instance.mSkillDataArr[id].CooltimeCurr = SkillButton.Instance.mSkillDataArr[id].Cooltime;
        SkillButton.Instance.ShowSkillCoolTime(SkillButton.Instance.mSkillDataArr[id].ID, true);

        while (SkillButton.Instance.mSkillDataArr[id].CooltimeCurr > 0)
        {
            SkillButton.Instance.mSkillDataArr[id].CooltimeCurr -= Time.deltaTime;

            SkillButton.Instance.ShowCoolTime(SkillButton.Instance.mSkillDataArr[id].ID, SkillButton.Instance.mSkillDataArr[id].Cooltime, SkillButton.Instance.mSkillDataArr[id].CooltimeCurr);
            SkillButton.Instance.mSkillButtonArr[id].interactable = false;
            yield return fixedUpdate;
        }

        SkillButton.Instance.mSkillButtonArr[id].interactable = true;
        SkillButton.Instance.ShowSkillCoolTime(SkillButton.Instance.mSkillDataArr[id].ID, false);
    }
    public void ActivateSkill(int id)
    {
        StartCoroutine(SetCoolTime(SkillButton.Instance.mSkillDataArr[id].ID));
    }

    float _checkTime = 1;
    public void AttackAnim(string param)
    {
        if (Time.time - _checkTime > 1)
        {
            _checkTime = Time.time;
            mAnimator.SetTrigger(param);
        }
    }
}
