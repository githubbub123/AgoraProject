using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed = 4f;
    public float interactionRange = .5f;
    public ContactFilter2D interactionLayer;

    private string[] textArray = { "fafa fooey", "baba booey" };
    private Rigidbody2D rb;
    private Vector3 oldMoveVector;

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
        if (moveVector != new Vector2(0, 0))
        {
            oldMoveVector = new Vector3(moveVector.x, moveVector.y, 0);
        }

        // If the textbox is not open do some epic things
        if (!GameManager.gm.textBox.activeSelf)
        {
            rb.velocity = moveVector * moveSpeed;

            // Interacting with objects
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Collider2D[] interactions = new Collider2D[1];
                Physics2D.OverlapCircle(transform.position + oldMoveVector, interactionRange, interactionLayer, interactions);

                if (interactions[0] != null)
                {
                    string[] texts = interactions[0].GetComponent<CanInteractWith>().interactionMessage;
                    StartCoroutine(GameManager.gm.AppearText(texts));
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}