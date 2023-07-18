using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //forward facing
        if (spriteRend.sprite == sprites[2] && Input.GetKeyDown(KeyCode.W)) {
            spriteRend.sprite = sprites[3];
        }
        if (spriteRend.sprite == sprites[1] && Input.GetKeyDown(KeyCode.W))
        {
            spriteRend.sprite = sprites[0];
        }

        //backward facing
        if (spriteRend.sprite == sprites[0] && Input.GetKeyDown(KeyCode.S))
        {
            spriteRend.sprite = sprites[1];
        }
        if (spriteRend.sprite == sprites[3] && Input.GetKeyDown(KeyCode.S))
        {
            spriteRend.sprite = sprites[2];
        }

        //left
        if (spriteRend.sprite == sprites[0] && Input.GetKeyDown(KeyCode.A))
        {
            spriteRend.sprite = sprites[3];
        }
        if (spriteRend.sprite == sprites[1] && Input.GetKeyDown(KeyCode.A))
        {
            spriteRend.sprite = sprites[2];
        }

        //right
        if (spriteRend.sprite == sprites[3] && Input.GetKeyDown(KeyCode.D))
        {
            spriteRend.sprite = sprites[0];
        }
        if (spriteRend.sprite == sprites[2] && Input.GetKeyDown(KeyCode.D))
        {
            spriteRend.sprite = sprites[1];
        }

    }
}
