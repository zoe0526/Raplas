using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : DataLoader
{

    public static Item Instance;
    [SerializeField]
    public ItemElem mItemPrefab;
    [SerializeField]
    public Transform mScrollTargetPos;
    [SerializeField]
    public ItemInfo[] mItemInfo;
    public ItemInfo[] ItemInfos { get { return mItemInfo; } }
    private List<ItemElem> mItemElemList;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadJsonData(out mItemInfo, "JsonFiles/Item");



        mItemElemList = new List<ItemElem>();
        for (int i = 0; i < mItemInfo.Length; i++)
        {
            ItemElem elem = Instantiate(mItemPrefab, mScrollTargetPos);
            elem.Init(Resources.Load<Sprite>("Item/" + mItemInfo[i].ItemName));
            mItemElemList.Add(elem);

        }
    }
    
}

[System.Serializable]
public class ItemInfo
{
    
    public int ItemID;
    public string ItemName;
    public eItemType ItemType;
    private int mStockValue;        //겹칠수 있는 아이템들(포션 등)
    public void Init(int id, string name, eItemType type, int stockvalue)
    {
        ItemID = id;
        ItemName = name;

        ItemType = type;
        mStockValue = stockvalue;
    }

}
public enum eItemType
{
    Weapon,     //무기
    Coustume,   //갑옷
    Shoes,      //신발
    Gloves,     //글로브
    Pants,      //바지
    Potion      //포션

}