using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.badEnding == true)
        {
            CanInteractWith scr = GameObject.Find("DisplayText").GetComponent<CanInteractWith>();
            scr.interactionMessage = new string[] {"I finished packing...", "I don't think I'm going to leave.", "(Agora is stressed.)" };

            CanInteractWith scr2 = GameObject.Find("Door").GetComponent<CanInteractWith>();
            scr2.interactionMessage = new string[] { "I've made up my mind.", "I'm not leaving.", "I'm going back to bed." };
            scr2.choices = new string[] { };

            CanInteractWith scr3 = GameObject.Find("Bed").GetComponent<CanInteractWith>();
            scr3.interactionMessage = new string[] { "(Do you want to go to bed?)" };
            scr3.choices = new string[] {"Yes", "No"};
            scr3.interactionId = 3;

            BoxCollider2D col = GameObject.Find("Bed").GetComponent<BoxCollider2D>();
            col.offset = new Vector2(-0.3092572f, 0.02713943f);
            col.size = new Vector2(1.392323f, 1.054279f);
            Destroy(GameObject.Find("Waddles"));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
