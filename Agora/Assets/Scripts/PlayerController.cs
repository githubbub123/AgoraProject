using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Variables
    public float moveSpeed = 4f;

    private string[] textArray = { "fafa fooey", "baba booey" };
    private Rigidbody2D rb;

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(.1f);
        StartCoroutine(GameManager.gm.AppearText(textArray));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = moveVector * moveSpeed;
       
    }
}