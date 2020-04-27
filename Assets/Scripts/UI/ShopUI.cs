using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance; 
    public List<ShopItem> mItemList;
    [SerializeField]
    public ShopItem mPurchaseItemPrefab;
    [SerializeField]
    public Transform mScrollTarget;
    [SerializeField]
    public GameObject mPopUp;
    [SerializeField]
    public TextMeshProUGUI mPopupText;
    [SerializeField]
    public Button mPurchaseButton;
    [SerializeField]
    public Item[] mPurchaseItems;
    public int mCurrItemNum;
    public bool mPurchaseButtonClicked;
    public bool mCanBuy;
   void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    void Start()
    {
       for(int i=0; i<mPurchaseItems.Length;i++)
        {
            ShopItem item = Instantiate(mPurchaseItemPrefab, mScrollTarget);
            mItemList.Add(item); 
        }

       for(int i=0; i<mPurchaseItems.Length;i++)
        {
            mItemList[i].Set(mPurchaseItems[i]); 
        }
    }

    public int PurchaseNum(Item item)
    {
        for (int i = 0; i < mPurchaseItems.Length; i++)
        {
            if (mPurchaseItems[i] == item)
            {
                mCurrItemNum = i;
            }
        }
        return mCurrItemNum;
    } 
    public void PurchaseClick(ShopItem mShopItem)
    {
        if(mShopItem.mPurchaseItem.Cost<= PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold )
        {
            mPopupText.text = string.Format("{0}를 구매하시겠습니까? {1}골드가 차감됩니다!", mShopItem.mPurchaseItem.name.ToString(), mShopItem.mPurchaseItem.Cost);
            mPurchaseButton.gameObject.SetActive(true);
            mPopUp.gameObject.SetActive(true);
            mCanBuy = true;
            PurchaseNum(mShopItem.mPurchaseItem);
          
        }
        else
        {
            mPopupText.text = string.Format("보유 골드가 부족합니다!");
            mPurchaseButton.gameObject.SetActive(false);
            mPopUp.gameObject.SetActive(true);
        }

    }
    public void BUttonC()
    {
        if (mCanBuy)
        { 
            SoundController.Instance.PlayEffect((int)eEffectsound.Bought);
            PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold -= mItemList[mCurrItemNum].mPurchaseItem.Cost;
            Inventory.Instance.Add(mItemList[mCurrItemNum].mPurchaseItem);
            Destroy(mItemList[mCurrItemNum].gameObject);
             
            mPopUp.gameObject.SetActive(false); 
        }

        mCanBuy = false;
    }
  
    /*
    public void PurchaseClick(Item item, int Itemnum)
    { 
        if(item.Cost<= PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold)
        { 
            mPopupText.text = string.Format("{0}를 구매하시겠습니까? {1}골드가 차감됩니다!", item.name.ToString(), item.Cost);
            mPurchaseButton.gameObject.SetActive(true);
            mPopUp.gameObject.SetActive(true);
            mPurchaseButton.onClick.AddListener(() => {
                Bought = true;
                SoundController.Instance.PlayEffect((int)eEffectsound.Bought); 
                PlayerManager.Instance.mPlayerInfo[PlayerManager.Instance.CurrID].Gold -= item.Cost;
                //Inventory.Instance.Add(item);
                Debug.Log(mPurchaseItems[Itemnum]);
                  
                   Destroy(mItemList[mCurrItemNum].gameObject);
               
                
                mPopUp.gameObject.SetActive(false);
                //Bought = false;
            });
        }
        else
        {
            mPopupText.text = string.Format("보유 골드가 부족합니다!");
            mPurchaseButton.gameObject.SetActive(false);
            mPopUp.gameObject.SetActive(true);
        } 
    }  
    */
}

                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
                                                                                                  
             