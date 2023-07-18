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
        if (Input.GetKeyDown(KeyCode.W)) {
            spriteRend.sprite = sprites[0];
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spriteRend.sprite = sprites[1];
        }
    }
}
