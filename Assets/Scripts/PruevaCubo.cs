using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruevaCubo : MonoBehaviour
{

    public int dir;
    public bool Move;
    public Vector3 pos;
    void Start()
    {
        //StartCoroutine(StartMove());
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Avanzar();
    }

    public void Avanzar()
    {
        //if(Move)transform.Translate(5 * Time.deltaTime * (-Mathf.Cos(dir * Mathf.PI / 2)), 0, 5 * Time.deltaTime * (Mathf.Sin(dir * Mathf.PI / 2)));
        transform.position = Vector3.Lerp(transform.position, pos, 0.2f);
    }

    /*IEnumerator Go()
    {
        Move = true;
        yield return new WaitForSeconds(0.2f);
        Move = false;
        Road.i.Step();
        GetDir();
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Go());
    }*/

    /*IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Go());
    }*/

    private void GetDir()
    {
        foreach(Tile T in Road.i.Tiles)
        {
            if (T.Livespam == 9) dir = T.dir;
        }
        
    }

    public void Step()
    {
        pos += new Vector3(-Mathf.Cos(dir * Mathf.PI / 2), 0, Mathf.Sin(dir * Mathf.PI / 2));
        GetDir();
    }
}
