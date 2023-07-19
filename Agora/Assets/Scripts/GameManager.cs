using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager gm;
    public GameObject textBox;
    public GameObject choiceBox;
    public TextMeshProUGUI textObject;
    public float textSpeed = .1f;
    public float textBoxAppearSpeed = .005f;
    public string[] texts;

    private int textArrayIndex;
    private bool textDisplayed = false;
    private bool appearing = false;

    void Start()
    {
        gm = this;
        textBox.SetActive(false);
        choiceBox.SetActive(false);
    }

    public IEnumerator AppearText(string[] newTextArray)
    {
        // Replacing the text array and making the first line appear
        appearing = true;
        textArrayIndex = 0;
        texts = newTextArray;
        StartCoroutine(SubText(newTextArray[textArrayIndex]));

        // Tweening the text box to appear
        StartCoroutine(AppearBackground(textBox));

        yield return new WaitForSeconds(.1f);
        appearing = false;
    }

    //public IEnumerator AppearChoice()
    //{

    //}

    public IEnumerator SubText(string text)
    {
        // Subbing text so it flows across the screen
        textDisplayed = false;
        for (int i = 0; i-1 < text.Length; i++)
        {
            if (textDisplayed == true)
            {
                break;
            }
            textObject.text = text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
        textObject.text = text;
        textDisplayed = true;
    }

    public IEnumerator AppearBackground(GameObject object1)
    {
        // Tweening the text box to appear
        if (!object1.activeSelf)
        {
            Transform objectTransform = object1.GetComponent<RectTransform>();
            object1.SetActive(true);

            objectTransform.localScale = new Vector3(1, 0, 1);
            for (float i = 0; i < 1.1; i += .1f)
            {
                objectTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }
        }
    }

    public IEnumerator DisappearBackground(GameObject object1)
    {
        // Tweening the box to disappear
        if (object1.activeSelf)
        {
            Transform objectTransform = object1.GetComponent<RectTransform>();
            objectTransform.localScale = new Vector3(1, 1, 1);

            for (float i = 1; i > 0; i -= .1f)
            {
                objectTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }
            object1.SetActive(false);
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
                bool hasntDisplayedAllText = texts.Length > textArrayIndex+1;

                // If there is another text to display, display it. Otherwise, disappear the text box
                if (appearing == false)
                {
                    if (textDisplayed == false)
                    {
                        textDisplayed = true;
                        return;
                    }

                    if (hasntDisplayedAllText)
                    {
                        textArrayIndex += 1;
                        StartCoroutine(SubText(texts[textArrayIndex]));
                    }
                    else
                    {
                        StartCoroutine(DisappearBackground(textBox));
                        StartCoroutine(DisappearBackground(choiceBox));
                    }
                }
            }
        }
    }
}
