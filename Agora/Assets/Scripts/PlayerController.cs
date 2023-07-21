using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed = 4f;
    private float interactionRange = .5f;
    public ContactFilter2D interactionLayer;

    private string[] textArray = { "fafa fooey", "baba booey" };
    private Rigidbody2D rb;
    private Vector2 moveVector;
    public Vector3 oldMoveVector;

    // Gets the component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Getting an old move vector for detecting interactions
        if (moveVector != new Vector2(0, 0))
        {
            if (GameManager.gm.currentInteractObj != null)
            {
                if (GameManager.gm.currentInteractObj.interactionId == 1)
                {
                    return;
                }
            }

            oldMoveVector = new Vector3(moveVector.x, moveVector.y, 0);
        }

        // If the player is not doing an interaction then proceed
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (GameManager.gm.currentInteractObj == null)
            {

                // Move the player
                rb.velocity = moveVector * moveSpeed;
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
         Vector3 interactVector = new Vector3(interactionRange / 2, interactionRange / 2, 0);
        Gizmos.DrawWireSphere(transform.position + oldMoveVector - interactVector, interactionRange);
    }

    void Update()
    {
        // Interacting with objects
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.gm.currentInteractObj == null && GameManager.gm.publicDebounce == false)
        {
            Collider2D[] interactions = new Collider2D[1];
            Vector3 interactVector = new Vector3(interactionRange / 2, interactionRange / 2, 0);
            Physics2D.OverlapCircle(transform.position + oldMoveVector - interactVector, interactionRange, interactionLayer, interactions);

            if (interactions[0] != null)
            {
                CanInteractWith scr = interactions[0].GetComponent<CanInteractWith>();
                StartCoroutine(GameManager.gm.AppearText(scr));
            }
        }
    }
}