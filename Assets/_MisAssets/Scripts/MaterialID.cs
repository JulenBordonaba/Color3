using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[SerializeField]
public class MaterialID 
{
    [Tooltip("ID del color, en cada objeto puede corresponder a un matrial distinto")]
    public string id;

    [Tooltip("Material del objeto, corresponde a la id")]
    public Material material;
}
