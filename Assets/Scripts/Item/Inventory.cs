using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance; 
    private InventoryUI mInventoryUI;
    public List<Item> mItems;
    public delegate void OnItemChanged();  //무슨 메서드던지 트리거되면 다 불러준다.
    public OnItemChanged OnItemChangedCallback;

    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
        mItems = new List<Item>();
        mInventoryUI = GetComponent<InventoryUI>();
    }
    
    public bool Add(Item item)
    {
      if(!item.isDefaaultItem)
        {
            if(mItems.Count>=mInventoryUI.mSlotnum)
            {
                Debug.Log("NO SPACE"); 
                return false; 
            }
            mItems.Add(item);

            if(OnItemChangedCallback!=null)
                OnItemChangedCallback.Invoke(); //이벤트 트리거
        }
        return true;
    }

    public void Remove(Item item)
    {
        mItems.Remove(item);
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke(); //이벤트 트리거
    } 
}
