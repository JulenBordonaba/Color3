using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Singleton<Road>
{
    static public Road i;
    public GameObject tile;
    public List<Tile> tiles;

    public AnimationCurve fallTimeProgression;
    public float fallStartTime = 2f;

    public float distanceBetweenTiles = 1.5f;

    public int lives; //Mover Mas adelante a Una clase GameplayManager o al propio cuvo
    public int dir = 0; // 0-> x negativo|1-> z positivo|2-> x positivo|3-> z negativo
    [Tooltip("Probabilidad de que ocurra un cambio de sentido. 0 no ocurrira y 100 ocurrira siempre")]
    public int DirChangeChance;
   
    private Vector3 pos;

    private int numTiles = 0;

    private float time;

    public override void Awake()
    {
        base.Awake();
        foreach (Tile t in tiles)
        {
            t.tileID = numTiles.ToString();
            numTiles += 1;
        }

        for (int i = 0; i < 10; i++)
        {
            
            Step();

            transform.position = Vector3.Lerp(transform.position, pos, 1f);
        }

        int livespan = 9;

        foreach (Tile t in tiles)
        {
            t.Livespam = livespan;
            if(livespan>0)
            {
                livespan -= 1;
            }
        }
    }

    void Start()
    {
        i = this;
        pos = transform.position;
        


        StartCoroutine(StepWaitTime());        
    }
    
    void Update()
    {

        time += Time.deltaTime;
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
        Tile NewTile = Instantiate(tile, transform.position, transform.rotation).GetComponent<Tile>();
        NewTile.tileID = numTiles.ToString();
        numTiles += 1;
        NewTile.dir = dir;
        //NewTile.transform.parent = transform;
        tiles.Add(NewTile);
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

    public void TileFall()
    {
        foreach (Tile T in tiles)
        {
            T.Livespam++;
            if (T.Livespam > 7 + lives) T.Fall();
        }
    }



    public IEnumerator StepWaitTime()
    {
        yield return new WaitForSeconds(fallStartTime);


        while(true)
        {

            TileFall();

            yield return new WaitForSeconds(fallTimeProgression.Evaluate(time));
        }
    }

    public void Step() //Funcion Que inicia todo el movimiento y la que hay que llamar cuando sepulse el boton correcto
    {


        pos += new Vector3(-Mathf.Cos(dir * Mathf.PI / 2),0, Mathf.Sin(dir * Mathf.PI / 2)) * distanceBetweenTiles;
        dir = RollDir(dir);
        
        Spawn(dir);
    }
}
