using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MakeTextAppear());
    }

    private IEnumerator MakeTextAppear() {
        yield return new WaitForSeconds(1f);
        string[] text = { "bavaf}" };
        StartCoroutine(GameManager.gm.AppearBackground(text));
    }


    // Update is called once per frame
    void Update()
    {
        //FindObjectOfType<GameManager>().AppearBackground(choiceBox));
    }
}
