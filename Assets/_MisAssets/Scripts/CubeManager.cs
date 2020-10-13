using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour
{
    [Tooltip("Put here all the materials with their IDs")]
    public List<MaterialID> materials = new List<MaterialID>();

    [Tooltip("The time in seconds that the cube can move again afte it's last move")]
    public float movementCooldown = 1f;

    [Tooltip("The time the cube lasts to reach the next position")]
    public float movementTime = 0.5f;

    [Header("Components")]
    public Animator animator;
    public Rigidbody rb;
    public new Renderer renderer;
    public GameObject model;

    protected Tile currentTile;

    protected Vector3 nextPos;

    protected bool canMove = true;
    

    /// <summary>
    /// we initialize the component on the start
    /// </summary>
    public void Start()
    {
        currentTile = Road.Instance.tiles[0];
        SetColor(currentTile.currentMaterial.id);
        currentTile.cubeManager = this;
        model.transform.localPosition = Vector3.zero;
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
    /// This function sets the current tiles color on the cube
    /// </summary>
    public void SetCurrentColor()
    {
        Debug.Log("CurrentColor");
        foreach (MaterialID materialID in materials)
        {
            if (materialID.id == currentTile.currentMaterial.id)
            {
                renderer.material = materialID.material;
                return;
            }
        }
    }

    
    /// <summary>
    /// This function checks if the colorID given matches with the next tiles colorID
    /// </summary>
    /// <param name="colorID">The id of the given color</param>
    public void CheckTile(string colorID)
    {

        int nextTileIndex = NextTileIndex;

        if(Road.Instance.tiles[nextTileIndex].currentMaterial.id == colorID)
        {
            if(canMove)
            {
                Move(nextTileIndex);
            }
        }
        else
        {
            //sacamos el menú de muerte
            Die();
        }
    }

    /// <summary>
    /// This function moves the cube to it's next position
    /// </summary>
    public void Move(int nextTileIndex)
    {
        //we restrict the movement
        canMove = false;
        

        //we generate a new Tile
        Road.Instance.Step();

        //we get the next cube position
        nextPos = Road.Instance.tiles[nextTileIndex].transform.position;
        nextPos.y = transform.position.y;

        //we rotate the cube toward the next tile
        model.transform.forward = nextPos - transform.position;
        
        //we deassign the currentTile's cubeManager
        currentTile.cubeManager = null;

        //we assign the next tile
        currentTile = Road.Instance.tiles[nextTileIndex];

        //we assign the new current tile's cubeManager
        currentTile.cubeManager = this;

        //we update the animator
        animator.SetTrigger("Move");
    }

    /// <summary>
    /// This function calls a coroutine to reactivate the movement
    /// </summary>
    public void AllowMovement()
    {
        StartCoroutine(AllowMovementCoroutine(movementCooldown));
    }
    
    /// <summary>
    /// This function starts the movement of the cube  
    /// </summary>
    public void StartMovement()
    {
        //we move the cube to the next position
        Tween myTween = transform.DOMove(nextPos, movementTime);

        //when the movement is finished we allow it again
        myTween.OnComplete(AllowMovement);
    }

    /// <summary>
    /// This coroutine reactivates the movement of the cube afte some time
    /// </summary>
    /// <param name="_cooldown">The time to wait to reactivate the movement</param>
    /// <returns></returns>
    public IEnumerator AllowMovementCoroutine(float _cooldown)
    {
        yield return new WaitForSeconds(_cooldown);
        canMove = true;
    }

    /// <summary>
    /// This property returns the index of the next tile the cube will go to
    /// </summary>
    public int NextTileIndex
    {
        get
        {

            for (int i = 0; i < Road.Instance.tiles.Count; i++)
            {
                if(Road.Instance.tiles[i].tileID==currentTile.tileID)
                {
                    return i + 1;
                }
            }
            Debug.LogError("Not Tile Matched");
            return -1;
        }
    }

    /// <summary>
    /// This function is called when the player dies
    /// </summary>
    public void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
