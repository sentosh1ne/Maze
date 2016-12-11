using UnityEngine;
using System.Collections;


/// <summary>
/// User input for the player
/// </summary>
public class PlayerControls : MonoBehaviour,IFlippable
{

    public float playerSpeed = 2.0f;

    private Animator animator;
    private Rigidbody2D rgBody;

    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Flip();
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        rgBody.velocity = move * playerSpeed;
        if (isMoving())
        {
        
            animator.SetTrigger("Moved");
        }
    }

    bool isMoving()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down")
            || Input.GetKeyDown("right") || Input.GetKeyDown("left"))
        {
            return true;
        }

        return false;
    }

    // Change facing depending on movement direction
    public void Flip()
    {
        Debug.Log(rgBody.velocity.x);
        float relativePosition = rgBody.velocity.x + transform.position.x;

        if (relativePosition < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
    }
}
