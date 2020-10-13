using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoplayBorrar : MonoBehaviour
{
    public Road A;
    public PruevaCubo B;

    void Start()
    {
        StartCoroutine(Go());
    }
    
    void Update()
    {
        
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(1f);
        A.Step();
        B.Step();
        StartCoroutine(Go());
    }
}
