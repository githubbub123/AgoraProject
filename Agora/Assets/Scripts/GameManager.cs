using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager gm;
    public GameObject textBox;
    public TextMeshProUGUI textObject;
    public float textSpeed = .1f;

    private RectTransform textBoxTransform;

    void Start()
    {
        gm = this;
        textBoxTransform = textBox.GetComponent<RectTransform>();
        textBox.SetActive(false);
    }

    public IEnumerator AppearText(string text)
    {
        StartCoroutine(SubText(text));

        // Tweening the text box to appear
        if (!textBox.activeSelf)
        {
            textBox.SetActive(true);
            textBoxTransform.localScale = new Vector3(1, 0, 1);
            for (float i = 0; i < 1.1; i += .1f)
            {
                print(i);
                textBoxTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(.005f);
            }
        }
    }

    public IEnumerator SubText(string text)
    {
        // Subbing in the text over time
        for (int i = 0; i < text.Length; i++)
        {
            textObject.text = text.Substring(1, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public IEnumerator DisappearText()
    {
        // Tweening the box to disappear
        if (textBox.activeSelf)
        {
            print("disap");
            textBoxTransform.localScale = new Vector3(1, 1, 1);
            for (float i = 1; i > 0; i -= .1f)
            {
                textBoxTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(.005f);
            }
            textBox.SetActive(false);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // StartCoroutine(DisappearText());
        }
    }
}
