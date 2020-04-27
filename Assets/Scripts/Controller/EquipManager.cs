 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance;

    private Equipment[] mDefaultEquip;
    private Equipment[] mCurrEquipment;
    private int mEquipSlotNum;//총 슬롯
    private int mSlotIndex;//각 슬롯 넘버
    private SkinnedMeshRenderer[] mCurrMesh;
    public SkinnedMeshRenderer mSwordTargetMesh;
    public SkinnedMeshRenderer mHelmetTargetMesh;
     
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
    }

    public delegate void OnEquipmentChanged(Equipment newitem, Equipment olditem);
    public OnEquipmentChanged onEquipmentChaged;

    void Start()
    {
        mEquipSlotNum = System.Enum.GetNames(typeof(eEquipSlot)).Length;
        mCurrEquipment = new Equipment[mEquipSlotNum];
        mCurrMesh = new SkinnedMeshRenderer[mEquipSlotNum];
    }

    public void Equip(Equipment newitem)
    {
        mSlotIndex = (int)newitem.meEquipSlot;//어느부위인지 분별
        Equipment OldItem = null;
        Debug.Log("Equip");
        if(mCurrEquipment[mSlotIndex]!=null)
        {
            OldItem = mCurrEquipment[mSlotIndex];
      
            Inventory.Instance.Add(OldItem);
            if (mCurrMesh[mSlotIndex] != null)
            {
                Destroy(mCurrMesh[mSlotIndex].gameObject);
            }
        }
        if(onEquipmentChaged!=null)
        {
            onEquipmentChaged.Invoke(newitem, OldItem);
        }

        mCurrEquipment[mSlotIndex] = newitem;
        EquipUI.Instance.mImage[mSlotIndex].sprite = newitem.icon;
        switch(mSlotIndex)
        {
            case (int)eEquipSlot.Weapon:
                SkinnedMeshRenderer newSwordMesh = Instantiate<SkinnedMeshRenderer>(newitem.mMesh);//
                newSwordMesh.transform.parent = mSwordTargetMesh.transform;//부모
                newSwordMesh.bones = mSwordTargetMesh.bones;
                newSwordMesh.rootBone = mSwordTargetMesh.rootBone;    //부모 뼈에 맞게
                mCurrMesh[mSlotIndex] = newSwordMesh;
                break;
            case (int)eEquipSlot.Gloves:
                
                break;
            case (int)eEquipSlot.Accessory:
                break;
            case (int)eEquipSlot.Feet:
                break;
            case (int)eEquipSlot.Helmet:
                SkinnedMeshRenderer newhelmetMesh = Instantiate<SkinnedMeshRenderer>(newitem.mMesh);
                newhelmetMesh.transform.parent = mHelmetTargetMesh.transform;
                newhelmetMesh.bones = mHelmetTargetMesh.bones;
                newhelmetMesh.rootBone = mHelmetTargetMesh.rootBone;
                mCurrMesh[mSlotIndex] = newhelmetMesh;
                break;
            case (int)eEquipSlot.Sheild:
                break;
        }
    }

    public void DefaultEquipItems()
    {
        foreach(Equipment item in mDefaultEquip)
        {
            Equip(item);
        }
    }

    public void UnEquip(int slotindex)
    {
        if(mCurrEquipment[slotindex]!=null)
        {
            if(mCurrMesh[slotindex]!=null)
            {
                Destroy(mCurrMesh[slotindex].gameObject);
            }
            Equipment OldItem = mCurrEquipment[slotindex];
            Inventory.Instance.Add(OldItem);
            Debug.Log("UnEquip");
            mCurrEquipment[slotindex] = null;
            if (onEquipmentChaged != null)
            {
                onEquipmentChaged.Invoke(null, OldItem);
            }
            
        }
    }//장비 하나 해제

    public void UnEquipAll()
    {
        for(int i=0; i<mEquipSlotNum;i++)
        {
            UnEquip(i);
        }
    }//장비 전부 해제

}
