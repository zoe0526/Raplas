using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int mSlotX, mSlotY;
    [SerializeField]
    public ItemElem mItemPrefab;
    [SerializeField]
    public Transform mScrollTargetPos;
    private List<ItemElem> mItemElemList;
    void Awake()
    {
       
        mSlotX = 4;
        mSlotY = 5;
        mItemElemList = new List<ItemElem>();
    }
    void Start()
    {
       

        for (int i = 0; i < mSlotX * mSlotY; i++)
        {
            ItemElem elem = Instantiate(mItemPrefab, mScrollTargetPos);
            elem.Init(Resources.Load<Sprite>("Item/Empty"));
            mItemElemList.Add(elem);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
