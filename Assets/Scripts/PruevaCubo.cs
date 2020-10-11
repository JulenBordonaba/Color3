using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruevaCubo : MonoBehaviour
{

    public int dir;
    public bool Move;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove());
    }

    // Update is called once per frame
    void Update()
    {
        Avanzar();
    }

    public void Avanzar()
    {
        if(Move)transform.Translate(5 * Time.deltaTime * (-Mathf.Cos(dir * Mathf.PI / 2)), 0, 5 * Time.deltaTime * (Mathf.Sin(dir * Mathf.PI / 2)));
    }

    IEnumerator Go()
    {
        Move = true;
        yield return new WaitForSeconds(0.2f);
        Move = false;
        GetDir();
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Go());
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Go());
    }

    private void GetDir()
    {
        foreach(Tile T in Road.i.Tiles)
        {
            if (T.Livespam == 8) dir = T.dir;
        }
        
    }
}
