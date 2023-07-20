using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    private bool saidText = false;
    // Start is called before the first frame update
    public CanInteractWith scr;

    void Start()
    {
        StartCoroutine(DisplayTheText());
    }

    private IEnumerator DisplayTheText() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameManager.gm.AppearText(scr));

    }
    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<SwitchSprite>().sprites == FindObjectOfType<SwitchSprite>().spritesWithMask && saidText == false) {
            scr.interactionMessage = new string[] {"(Help Agora find her bed to go to sleep.)", "(She cannot see with her sleeping mask on.)"};
            StartCoroutine(DisplayTheText());
            saidText = true;
        }
    }
}
