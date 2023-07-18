using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Variables
    public float moveSpeed = 4f;

    private string[] textArray = { "fafa fooey", "baba booey" };
    private Rigidbody2D rb;

    // Gets the component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Moves the player if the textbox is not open
        if (!GameManager.gm.textBox.activeSelf)
        {
            Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = moveVector * moveSpeed;
        }
    }
}