using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(Time()); 
    }

    private IEnumerator Time()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
