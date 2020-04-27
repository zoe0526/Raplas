using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Equiptment",menuName ="Inventory/Equiptment")]
public class Equipment :Item
{
    public eEquipSlot meEquipSlot;
    public int mArmorModifier;
    public int mDamageModifier;
    public SkinnedMeshRenderer mMesh;
   
    public override void Use()
    {
        base.Use();
        EquipManager.Instance.Equip(this);
        
        RemoveFromInventory();
    } 
}

public enum eEquipSlot
{
    Helmet,
    Accessory,
    Gloves,
    Weapon,
    Sheild,
    Feet
}
