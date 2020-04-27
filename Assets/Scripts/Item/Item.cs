using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum eItemType
{
    Weapon,     //무기
    Armor,   //갑옷
    Shoes,      //신발
    Gloves,     //글로브
    Pants,      //바지
    Accessory,  //액세서리
    Potion      //포션

}
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaaultItem = false;
    public float Cost;
    public int ID;

    public virtual void Use()//각아이템마다 사용방법이 다르기때문
    {
        Debug.Log("used Item" + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    } 
} 

