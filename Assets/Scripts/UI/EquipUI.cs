using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipUI : MonoBehaviour
{
    public static EquipUI Instance;
    [SerializeField]
    public Image[] mImage;
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
        for (int i = 0; i < mImage.Length; i++)
        {
            mImage[i].sprite = Resources.Load<Sprite>("Empty");
        }
    } 
}
