using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemElem : MonoBehaviour
{
  
    [SerializeField]
    private Image mItemIcon;
    [SerializeField]
    private int mID;
    // Start is called before the first frame update
    void Start()
    {
       //mItemIcon.sprite = Resources.Load<Sprite>("Item/"+ Item.Instance.mItemInfo[i].ItemName);
       
         
        
    }
    public void Init(Sprite icon)
    {
        mItemIcon.sprite = icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
