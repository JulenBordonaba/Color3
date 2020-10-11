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
            foreach (Tile T in Tiles)
            {
                if (!T.clear)
                {
                    T.transform.Translate(5 * Time.deltaTime, 0, 0);
                    //T.transform.position = Vector3.Lerp(T.transform.position, new Vector3(T.transform.position.x + 1, T.transform.position.y, T.transform.position.z), 0.2f);
                    if (T.transform.position.x > 10 + lives) T.Fall();
                }
                
            }
        }
    }

    public void Spawn()
    {
        GameObject NewTile = Instantiate(tile, transform.position, transform.rotation);
        NewTile.transform.parent = transform;
        Tiles.Add(NewTile.GetComponent<Tile>());
    }

    IEnumerator Go()
    {
        Move = true;
        yield return new WaitForSeconds(0.2f);
        Move = false;
        Spawn();
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(Go());
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Go());
    }
}
