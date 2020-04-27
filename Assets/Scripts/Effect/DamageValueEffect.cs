using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageValueEffect : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI mDamagevalue;
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
