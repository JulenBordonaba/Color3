using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    static public Road i;
    public GameObject tile;
    public List<Tile> Tiles;

    public int lives; //Mover Mas adelante a Una clase GameplayManager o al propio cuvo
    public int dir = 0; // 0-> x negativo|1-> z positivo|2-> x positivo|3-> z negativo
    [Tooltip("Probabilidad de que ocurra un cambio de sentido. 0 no ocurrira y 100 ocurrira siempre")]
    public int DirChangeChance;
   
    private Vector3 pos;

    void Start()
    {
        i = this;
        pos = transform.position;
        //StartCoroutine(StartMove());        
    }
    
    void Update()
    {
        Avanzar();
    }
    
    public void Avanzar() 
    {
        //if (Move)
        {            
            //transform.Translate(5 * Time.deltaTime * (-Mathf.Cos(dir*Mathf.PI/2)), 0, 5 * Time.deltaTime * (Mathf.Sin(dir * Mathf.PI/2)));            
            transform.position = Vector3.Lerp(transform.position,pos,0.2f);
        }
    }

    public void Spawn(int dir) //Genera Un Tile en la posicion actual del Road y la añade a la lista
    {
        GameObject NewTile = Instantiate(tile, transform.position, transform.rotation);
        NewTile.GetComponent<Tile>().dir = dir;
        //NewTile.transform.parent = transform;
        Tiles.Add(NewTile.GetComponent<Tile>());
    }

    /*IEnumerator Go()
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
    }*/

    /*IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Go());
    }*/

    private int RollDir(int dir) //Al azar girara o no y si gira al azar entre izquierda y derecha
    {
        if (Random.Range(0, 100) <= DirChangeChance)
        {
            if (Random.Range(0, 100) <= 50)
            {
                return dir + 1;
            }
            else return dir - 1;
        } else return dir;
    }

    public void Step() //Funcion Que inicia todo el movimiento y la que hay que llamar cuando sepulse el boton correcto
    {
        pos += new Vector3(-Mathf.Cos(dir * Mathf.PI / 2),0, Mathf.Sin(dir * Mathf.PI / 2));
        dir = RollDir(dir);
        foreach (Tile T in Tiles)
        {
            T.Livespam++;
            if (T.Livespam > 7 + lives) T.Fall();
        }
        Spawn(dir);
    }
}
