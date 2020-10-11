using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    static public Road i;
    public List<Tile> Tiles;
    public int lives;
    public GameObject tile;
    private bool Move=false;
    public int dir = 0; // 0-> x negativo|1-> z positivo|2-> x positivo|3-> z negativo

    void Start()
    {
        i = this;
        StartCoroutine(StartMove());        
    }
    
    void Update()
    {
        Avanzar();
    }
    //Vector3.Lerp(T.transform.position, new Vector3(T.transform.position.x + 1, T.transform.position.y, T.transform.position.z),0.2f)
    public void Avanzar()
    {
        if (Move)
        {
            /*foreach (Tile T in Tiles)
            {                               
                    T.transform.Translate(5 * Time.deltaTime, 0, 0);
                    //T.transform.position = Vector3.Lerp(T.transform.position, new Vector3(T.transform.position.x + 1, T.transform.position.y, T.transform.position.z), 0.2f);
                    if (T.transform.position.x > 10 + lives) T.Fall();                                
            }*/
            transform.Translate(5 * Time.deltaTime * (-Mathf.Cos(dir*Mathf.PI/2)), 0, 5 * Time.deltaTime * (Mathf.Sin(dir * Mathf.PI/2)));
            
        }
    }

    public void Spawn(int dir)
    {
        GameObject NewTile = Instantiate(tile, transform.position, transform.rotation);
        NewTile.GetComponent<Tile>().dir = dir;
        //NewTile.transform.parent = transform;
        Tiles.Add(NewTile.GetComponent<Tile>());
    }

    IEnumerator Go()
    {
        Move = true;
        yield return new WaitForSeconds(0.2f);
        Move = false;        
        dir = RollDir(dir);
        foreach (Tile T in Tiles)
        {
            T.Livespam++;
            if (T.Livespam > 8 + lives) T.Fall();
        }
        Spawn(dir);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Go());
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Go());
    }

    private int RollDir(int dir)
    {
        if (Random.Range(0, 100) <= 10)
        {
            if (Random.Range(0, 100) <= 50)
            {
                return dir + 1;
            }
            else return dir - 1;
        } else return dir;
    }
}
