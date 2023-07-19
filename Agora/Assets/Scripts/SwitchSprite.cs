using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] spritesWithMask;
    private SpriteRenderer spriteRend;


    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void PutMaskOn()
    {
        sprites = spritesWithMask;
        spriteRend.sprite = sprites[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.textBox.activeSelf) {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            
            if (verticalInput == 1)
            {
                // Backward
                spriteRend.sprite = sprites[0];
            }
             if (verticalInput == -1 || horizontalInput == 1)
            {
                // Forward right
                spriteRend.sprite = sprites[1];
            }
             if (horizontalInput == -1)
            {
                // Left
                spriteRend.sprite = sprites[2];
            }
            
        }

    }
}
