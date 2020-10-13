using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<MaterialID> materials;
    public Rigidbody rb;
    public MeshRenderer Col;    
    public int dir;
    public int Livespam;
    public MaterialID currentMaterial;

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
        currentMaterial = materials[Random.Range(0, materials.Count)];
        
        Col.material = currentMaterial.material;        
    }

}
