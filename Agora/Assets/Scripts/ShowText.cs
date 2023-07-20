using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
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
        
    }
}
