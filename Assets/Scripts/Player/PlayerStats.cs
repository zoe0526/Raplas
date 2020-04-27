using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerStats : CharacStat
{
    private Player mPlayer;
    public bool IsDead;
    private float mCoundown = 7.0f;
    private EffectPool mEffectPool;

    void Awake()
    {
        mEffectPool = GameObject.FindGameObjectWithTag("Effect").GetComponent<EffectPool>();
        mCoundown = 7.0f;
        IsDead = false;
    }

    void Start()
    {
        EquipManager.Instance.onEquipmentChaged += OnEquipmentChanged;
        mMaxHealth = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].MaxLife;

        mPlayer =GetComponent<Player>();
        mCurrHealth = mMaxHealth;
    }
    
    void OnEquipmentChanged(Equipment newitem,Equipment olditem)
    {
        if(newitem!=null)
        { 
            mArmor.AddModifer(newitem.mArmorModifier);
            mDamage.AddModifer(newitem.mDamageModifier);
        }
        if(olditem!=null)
        { 
            mArmor.RemoveModifier(olditem.mArmorModifier);
            mDamage.RemoveModifier(olditem.mDamageModifier);
        }
    }

    void ShowPlayerStat()
    {
        PlayerUI.Instance.mAttackstat.text = mDamage.GetValue().ToString();
        PlayerUI.Instance.mSheildstat.text = mArmor.GetValue().ToString();
        PlayerUI.Instance.mHPstat.text = string.Format("{0}/{1}", mCurrHealth.ToString(), PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].MaxLife.ToString());
    }
    
    public override void Die()
    {
        base.Die();
        IsDead = true; 
    }

    void Update()
    {
        mMaxHealth = PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].MaxLife;
        
        if(PlayerManager.Instance.mLevelup==true)
        {
            mCurrHealth = mMaxHealth;
            Effect effect = mEffectPool.GetFromPool((int)eEffectType.LevelUp);
            effect.transform.position = mPlayer.transform.position;
            SoundController.Instance.PlayEffect((int)eEffectsound.LevelUp);
            Debug.Log(mPlayer.transform.position);
            PlayerManager.Instance.mLevelup = false;
        }

        PlayerManager.Instance.EXPCalculate();
        ShowPlayerStat();
        PlayerUI.Instance.ShowPlayerHPBar(mCurrHealth, mMaxHealth);

        if (IsDead)
        {
            mCoundown -= Time.deltaTime;
            PlayerUI.Instance.mPreventClick.SetActive(true);
            AnimationController.Instance.mAnimator.SetBool(ePlayerAnimation.IsDead.ToString(), true);
            
            if(mCoundown<=5f)
            {
                mPlayer.GetComponent<NavMeshAgent>().enabled = false;
                PlayerUI.Instance.mLosePopup.SetActive(true);
                PlayerUI.Instance.mCountdown.text = string.Format("마을로 돌아갑니다..{0}", mCoundown.ToString("0"));

            }
           
            if (mCoundown <= 0)
            {
                PlayerUI.Instance.mLosePopup.SetActive(false);
                PlayerUI.Instance.mPreventClick.SetActive(false);
                LoadSceneController.Instance.LoadSceneAsync("GamePad", LoadSceneController.eScene.Game);
                AnimationController.Instance.mAnimator.SetBool(ePlayerAnimation.IsDead.ToString(), false);
                SoundController.Instance.PlayBGM((int)eBGMsound.Game);
                mCurrHealth = mMaxHealth;
                IsDead = false;
                mCoundown = 7.0f; 
            } 
        } 
    }
}

