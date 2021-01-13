using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterChangeSceneAndAfterSeconds : MonoBehaviour
{
    public float seconds;

    private void Awake()
    {
        print("Hola");
        DontDestroyOnLoad(this.gameObject);
        print("Adios");
    }

    public void DestroyObject()
    {
        StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
