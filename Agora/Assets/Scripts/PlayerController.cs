using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed = 4f;
    public float interactionRange = .2f;
    public ContactFilter2D interactionLayer;

    private string[] textArray = { "fafa fooey", "baba booey" };
    private Rigidbody2D rb;
    public Vector3 oldMoveVector;

    // Gets the component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Moves the player if the textbox is not open
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Getting an old move vector for detecting interactions

        // If the textbox is not open do some epic things
        if (GameManager.gm.currentInteractObj == null)
        {
            if (moveVector != new Vector2(0, 0))
            {
                oldMoveVector = new Vector3(moveVector.x, moveVector.y, 0);
            }

            rb.velocity = moveVector * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Update()
    {
        // Interacting with objects
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.gm.currentInteractObj == null)
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