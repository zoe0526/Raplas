using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss : CharacStat
{ 
    public Animator mAnimator;
    public BossInfo mBossInfo; 
    private Player mPlayer;
    
    public bool IsDead;
    private float mCountdownTime=5.0f;
    void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
   
    void Start()
    {
        BossUI.Instance.mHPBar.gameObject.SetActive(false);
        mMaxHealth = 50;
        mCurrHealth = mMaxHealth;
      
        mBossInfo.AttackValue = 6f;
        mBossInfo.DropGold = 100;
        mBossInfo.EXP = 50;
        mBossInfo.Life = mMaxHealth;
        mBossInfo.Speed = 0.4f;
        mDamage.AddModifer((int)mBossInfo.AttackValue);
        IsDead = false; 
    }

    private void SetActiveHPBar()
    {
        if (Vector3.Distance(mPlayer.transform.position,transform.position) <= gameObject.GetComponent<NavMeshAgent>().stoppingDistance
            && !mPlayer.GetComponent<PlayerStats>().IsDead)
        {
            BossUI.Instance.mHPBar.gameObject.SetActive(true);
        }
    }
    public float BossAttackSpeed()
    {
        float value = mBossInfo.Speed;
        return value;
    } 

    public override void Die()
    { 
        base.Die();
        IsDead = true;
        mAnimator.SetTrigger(eBossState.IsDie.ToString());
        SoundController.Instance.PlayEffect((int)eEffectsound.BossDead);
        PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold += mBossInfo.DropGold;
        PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].CurrEXP += mBossInfo.EXP;
        
        PlayerUI.Instance.ShowGold(PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold);
        
        BossUI.Instance.mWinPopup.gameObject.SetActive(true);
        BossUI.Instance.ShowValues();
       
        Inventory.Instance.Add(BossUI.Instance.mDropItem[BossUI.Instance.rand]); 
    }
 
    void Update()
    {
        SetActiveHPBar();
        BossUI.Instance.ShowBossHPBar(mCurrHealth, mMaxHealth);
     
        if (IsDead)
        {
            mCountdownTime -= Time.deltaTime;
            BossUI.Instance.mPreventClick.gameObject.SetActive(true);
            BossUI.Instance.mWinText.text = string.Format("승리!! {0}초 후 마을로 돌아갑니다.", 
                                                            mCountdownTime.ToString("0"));
            if (mCountdownTime <= 2)
                BossUI.Instance.mHPBar.gameObject.SetActive(false);
             
            if (mCountdownTime <= 0)
            {
                Destroy(gameObject);
                mPlayer.gameObject.GetComponent<NavMeshAgent>().enabled = false;

                BossUI.Instance.mPreventClick.gameObject.SetActive(false);
                LoadSceneController.Instance.LoadSceneAsync("GamePad", LoadSceneController.eScene.Game);
                
                SoundController.Instance.PlayBGM((int)eBGMsound.Game);
                IsDead = false;
                mCountdownTime = 5.0f;
                mPlayer.GetComponent<PlayerStats>().mCurrHealth = 
                        PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].MaxLife;
                 
            }
        }
    } 
}

[System.Serializable]
public class BossInfo
{
    public float DropGold;  //떨구는 골드량
    public float AttackValue;   //공격력
    public float EXP;   //주는 경험치
    public float Life;  //생명력
    public float Speed; 
}
