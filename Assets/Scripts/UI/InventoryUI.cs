using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory mInventory;
    [SerializeField]
    public int mSlotnum;
    [SerializeField]
    public ItemElem mItemPrefab;

    [SerializeField]
    public Transform mScrollTargetPos; 
    public List<ItemElem> mSlots;
 
    void Start()
    {
        CreateLayout();
        mInventory = Inventory.Instance;
        mInventory.OnItemChangedCallback += UpdateUI; 
    }
     
    void UpdateUI()
    {
        for(int i=0; i<mSlotnum;i++)
        {
            if(i<mInventory.mItems.Count)
            {
                mSlots[i].AddItem(mInventory.mItems[i]);
            }
            else
            {
                mSlots[i].ClearSlot();
            }
        }
    }

    private void CreateLayout()
    {
        for (int i = 0; i < mSlotnum; i++)
        { 
            ItemElem elem = Instantiate(mItemPrefab, mScrollTargetPos); 
            mSlots.Add(elem);  
        } 
    }

}
