using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemElem : MonoBehaviour
{
    public Item mItem; 
    public Text mNameText, mDescription, mSellCost, mAttackValue; 
    public Image mIcon;
    
    public void AddItem(Item item)
    {
        mItem = item;
        mIcon.sprite = item.icon;
        mIcon.enabled = true;
    }

    public void ClearSlot()
    {
        mItem = null;
        mIcon.sprite = null;
        mIcon.enabled = false;
    }
  
    public void UseItem()
    {
        if(mItem!=null)
            mItem.Use();
    } 
}
