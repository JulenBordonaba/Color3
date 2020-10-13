using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [Tooltip("Put here all the materials with their IDs")]
    public List<MaterialID> materials = new List<MaterialID>();

    [Tooltip("The time in seconds that the cube can move again afte it's last move")]
    public float movementCooldown = 1f;


    [Header("Components")]
    public Animator animator;
    public Rigidbody rb;
    public new Renderer renderer;

    
    private bool canMove = true;

    //código temporal
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            CheckTile("Red");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CheckTile("Green");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CheckTile("Blue");
        }

    }


    /// <summary>
    /// This functions sets the material that matches with the given color ID, if there are not matches it logs an error.
    /// </summary>
    /// <param name="colorID">The id of the given color</param>
    public void SetColor(string colorID)
    {
        foreach(MaterialID materialID in materials)
        {
            if(materialID.id == colorID)
            {
                renderer.material = materialID.material;
                return;
            }
        }

        Debug.LogError("The given ID doesn't match with any material.");
    }

    /// <summary>
    /// This function checks if the colorID given matches with the next tiles colorID
    /// </summary>
    /// <param name="colorID">The id of the given color</param>
    public void CheckTile(string colorID)
    {
        if(Road.Instance.Tiles[0].currentMaterial.id == colorID)
        {
            if(canMove)
            {
                SetColor(colorID);
                Move();
            }
        }
    }

    /// <summary>
    /// This function moves the cube to it's next position
    /// </summary>
    public void Move()
    {
        canMove = false;

        animator.SetTrigger("Move");

        Road.Instance.Step();

        StartCoroutine(AllowMovement(movementCooldown));
    }


    /// <summary>
    /// This coroutine reactivates the movement of the cube afte some time
    /// </summary>
    /// <param name="_cooldown">The time to wait to reactivate the movement</param>
    /// <returns></returns>
    public IEnumerator AllowMovement(float _cooldown)
    {
        yield return new WaitForSeconds(_cooldown);
        canMove = true;
    }
    
}
