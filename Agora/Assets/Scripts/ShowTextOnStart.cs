using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextOnStart : MonoBehaviour
{
    // Shows text on start
    public CanInteractWith scr;
    void Start()
    {
        StartCoroutine(DisplayTheText());
    }

    private IEnumerator DisplayTheText()
    {
        yield return new WaitForSeconds(0f);
        StartCoroutine(GameManager.gm.AppearText(scr));

    }
}
