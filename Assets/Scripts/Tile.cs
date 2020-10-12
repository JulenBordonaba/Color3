using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Material> Colors;
    public Rigidbody rb;
    public MeshRenderer Col;    
    public int Color;
    public int dir;
    public int Livespam;

    void Start()
    {
        SetColor();
    }    
    void Update()
    {
        Fallen();
    }  
    public void Fall() //Hace caer el Tile
    {
        rb.isKinematic = false;
    }   
    public void Fallen() // Hace despawnear el Tile
    {
        if(transform.position.y < -15)
        {
            Road.i.Tiles.Remove(this);
            Destroy(gameObject);
        }
    }
    public void SetColor() //Asigna un color y un material de la lista al azar
    {
        Color = Mathf.FloorToInt(Random.Range(1, 4f));
        Col.material = Colors[Color - 1];        
    }

}
