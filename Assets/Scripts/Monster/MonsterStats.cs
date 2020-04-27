using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : CharacStat
{ 
    [SerializeField]
    public int mID;
    private MonsterUI mMonsterUI;
    private MonsterGenerator mMonsterGenerator;
    public Animator mAnimator;
    public bool IsDead;

    void Awake()
    {
        mMonsterUI = GetComponent<MonsterUI>(); 
        mMonsterGenerator = MonsterGenerator.Instance;
        mAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        mMaxHealth = mMonsterGenerator.mMonsterInfo[mID].Life;
        mCurrHealth = mMaxHealth;
        mDamage.AddModifer((int)mMonsterGenerator.mMonsterInfo[mID].AttackValue);
        IsDead = false; 
    }

    public float MonsterAttackSpeed()
    {
        float value = mMonsterGenerator.mMonsterInfo[mID].Speed;
        return value;
    }
    public override void Die()
    {
        base.Die();
        IsDead = true;
        
        PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold += mMonsterGenerator.mMonsterInfo[mID].DropGold;
        PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].CurrEXP += mMonsterGenerator.mMonsterInfo[mID].EXP;
        PlayerUI.Instance.ShowGold(PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold); 
    }

    void Update()
    { 
        mMonsterUI.ShowMonsterHPBar(mCurrHealth, mMaxHealth); 
        if(IsDead)
        {
            Destroy(gameObject);
        } 
    } 
}

[System.Serializable]
public class MonsterInfo
{ 
    public int ID;
    public float DropGold;  //떨구는 골드량
    public float AttackValue;   //공격력
    public float EXP;   //주는 경험치
    public float Life;  //생명력
    public float Speed;
    public GameObject Prefab; 
}
