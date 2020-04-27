using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacStat : MonoBehaviour
{ 
    public float mMaxHealth;
    public float mCurrHealth; 
    public Stat mDamage;
    public Stat mArmor; 

    void Awake()
    { 
        mCurrHealth = mMaxHealth; 
    }

    public virtual void Resetstat()
    {
        mCurrHealth = mMaxHealth;
    }
  
    public void TakeDamage(int damage)
    {
        damage -= mArmor.GetValue();//방어력만큼 데미지 감소
        damage = Mathf.Clamp(damage, 0, int.MaxValue);//데미지값이 -가 안되도록

        mCurrHealth -= damage; 
        if (mCurrHealth<=0)
        { 
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("died");
    } 
} 

[System.Serializable]
public class Stat
{
    [SerializeField]
    public int BaseValue;
    private List<int> Modifiers=new List<int>();

    public int GetValue()
    {
        int FinalValue = BaseValue;
        Modifiers.ForEach(x => FinalValue += x);//모든 value들 값의 합을 반환해줌
        return FinalValue;
    }

    public void AddModifer(int modifier)
    {
        if (modifier != 0)
            Modifiers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            Modifiers.Remove(modifier);
    }
}
