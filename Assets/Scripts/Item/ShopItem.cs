using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItem : MonoBehaviour
{ 
    public Item mPurchaseItem;
    [SerializeField]
    public TextMeshProUGUI mCost, mName;
    [SerializeField]
    public Image mImage; 
    
    [SerializeField]
    public Button mPurchaseButton;
    
   public void Set(Item Item)
    {
        mPurchaseItem = Item;
        
        mCost.text = mPurchaseItem.Cost.ToString();
        mName.text = mPurchaseItem.name;
        mImage.sprite = mPurchaseItem.icon; 
    }
    
    public void ButtonClicked()
    { 
        ShopUI.Instance.PurchaseClick(this); 
    }  
}
