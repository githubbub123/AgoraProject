using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Variables
    public Transform moveToTransform;
    public float moveToSpeed = 4f;
    public float collisionCheckSize = 1f;
    public LayerMask unwalkable;

    void Start()
    {
        //StartCoroutine(GameManager.gm.AppearText("eevyyjfthfthdyftdthfytuftuyfytudthdtyhftdftde"));
    }
    // Update is called once per frame
    void Update()
    {
        // Moves the player towards the move point
        transform.position = Vector3.MoveTowards(transform.position, moveToTransform.position, moveToSpeed * Time.deltaTime);

        // Checks if the player wants to move.
        if (Vector3.Distance(transform.position, moveToTransform.position) == 0)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Moving the move point and thus our player will move to it
            if (Mathf.Abs(horizontalInput) == 1f)
            {
                Vector3 horizontalMoveVector = new Vector3(horizontalInput, 0, 0);
                if (!Physics2D.OverlapCircle(moveToTransform.position + horizontalMoveVector, collisionCheckSize, unwalkable))
                {
                    moveToTransform.position += horizontalMoveVector;
                }
            }
            else if (Mathf.Abs(verticalInput) == 1f)
            {
                Vector3 verticalMoveVector = new Vector3(0, verticalInput, 0); ;
                if (!Physics2D.OverlapCircle(moveToTransform.position + verticalMoveVector, collisionCheckSize, unwalkable))
                {
                    moveToTransform.position += verticalMoveVector;
                }
            }
        }
    }
}
