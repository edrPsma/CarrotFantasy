using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestory : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
