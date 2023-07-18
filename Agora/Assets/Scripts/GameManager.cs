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
    public float textBoxAppearSpeed = .005f;
    public string[] texts;

    private int textArrayIndex;
    private RectTransform textBoxTransform;

    void Start()
    {
        gm = this;
        textBoxTransform = textBox.GetComponent<RectTransform>();
        textBox.SetActive(false);
    }

    public IEnumerator AppearText(string[] newTextArray)
    {
        // Replacing the text array and making the first line appear
        texts = newTextArray;
        StartCoroutine(SubText(newTextArray[0]));

        // Tweening the text box to appear
        if (!textBox.activeSelf)
        {
            textBox.SetActive(true);
            textBoxTransform.localScale = new Vector3(1, 0, 1);
            for (float i = 0; i < 1.1; i += .1f)
            {
                textBoxTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }
        }
    }

    public IEnumerator SubText(string text)
    {
        // Subbing text so it flows across the screen
        for (int i = 0; i-1 < text.Length; i++)
        {
            textObject.text = text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public IEnumerator DisappearText()
    {
        // Tweening the box to disappear
        if (textBox.activeSelf)
        {
            textArrayIndex = 0;
            textBoxTransform.localScale = new Vector3(1, 1, 1);
            for (float i = 1; i > 0; i -= .1f)
            {
                textBoxTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }
            textBox.SetActive(false);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Text box interaciton
            if (texts != null)
            {
                // Getting the current text in the text that we need to display array.
                textArrayIndex += 1;

                // If there is another text to display, display it. Otherwise, disappear the text box
                if (texts.Length >= textArrayIndex + 1)
                {
                    StartCoroutine(SubText(texts[textArrayIndex]));
                }
                else
                {
                    StartCoroutine(DisappearText());
                }
            }

            // Interacting with objects
        }
    }
}
