using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public static bool isFirstTime = true;

    private void Awake()
    {
        if(!isFirstTime)
        {
            Destroy(gameObject);
            return;
        }
        isFirstTime = false;
    }
}
