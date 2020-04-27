using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadController : MonoBehaviour
{
    [SerializeField]
    public JoyStick mJoyStick;
    public static GamePadController Instance;
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
}
