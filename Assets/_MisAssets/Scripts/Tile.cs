using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<MaterialID> materials;
    public string tileID;
    public Rigidbody rb;
    public MeshRenderer Col;
    public int dir;
    public int Livespam;
    public MaterialID currentMaterial;

    public CubeManager cubeManager;

    void Awake()
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

        cubeManager?.Die();
    }
    public void Fallen() // Hace despawnear el Tile
    {
        if (transform.position.y < -15)
        {
            Road.i.tiles.Remove(this);
            Destroy(gameObject);
        }
    }
    public void SetColor() //Asigna un color y un material de la lista al azar
    {
        currentMaterial = materials[Random.Range(0, materials.Count)];

        Col.material = currentMaterial.material;
    }



}
