using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Variables
    public static GameManager gm;
    public GameObject textBox;
    public GameObject choiceBox;
    public TextMeshProUGUI[] choices;
    public TextMeshProUGUI textObject;
    public float textSpeed = .025f;
    public float textBoxAppearSpeed = .005f;
    public PlayerController playerScript;
    public bool isInteracting;

    private string[] texts;
    private int textArrayIndex;
    private int choiceIndex;
    private bool textDisplayed = false;
    private bool appearing = false;
    private string[] choiceTexts;
    public CanInteractWith currentInteractObj;

    void Start()
    {
        gm = this;
        textBox.SetActive(false);
        choiceBox.SetActive(false);
    }

    public IEnumerator AppearText(CanInteractWith scr)
    {
        // Replacing the text array and making the first line appear
        appearing = true;
        currentInteractObj = scr;
        textArrayIndex = 0;
        texts = scr.interactionMessage;
        StartCoroutine(SubText(scr.interactionMessage[textArrayIndex]));

        // Tweening the text box to appear
        StartCoroutine(AppearBackground(textBox));

        yield return new WaitForSeconds(.1f);
        appearing = false;
        StartCoroutine(DisappearBackground(choiceBox));
    }

    public void SelectChoice(float verticalInput)
    {
        int num = Convert.ToInt32(Mathf.Round(verticalInput));
        choiceIndex += num;

        // Looping choices
        if (choiceIndex > 1)
        {
            choiceIndex = 0;
        }
        else if (choiceIndex < 0)
        {
            choiceIndex = 1;
        }

        choices[0].text = choiceTexts[0];
        choices[1].text = choiceTexts[1];
        choices[choiceIndex].text = choiceTexts[choiceIndex] + " <";
    }

    public void AppearChoice(string[] selectedChoiceTexts)
    {
        // Making the choice box appear and auto selecting a choice
        StartCoroutine(AppearBackground(choiceBox));
        choiceTexts = selectedChoiceTexts;
        choiceIndex = -1;
        SelectChoice(1);
    }

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
       // if (!object1.activeSelf)
        //{
            Transform objectTransform = object1.GetComponent<RectTransform>();
            object1.SetActive(true);

            objectTransform.localScale = new Vector3(1, 0, 1);
            for (float i = 0; i < 1.1; i += .1f)
            {
                objectTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }
        //}
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
                if (appearing == true)
                {
                    print("stopped");
                    break;
                }
                objectTransform.localScale = new Vector3(1, i, 1);
                yield return new WaitForSeconds(textBoxAppearSpeed);
            }

            if (appearing == false)
            {
                object1.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (currentInteractObj != null)
        {
            // Text boxes
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Getting the current text in the text that we need to display array.
                bool hasntDisplayedAllText = texts.Length > textArrayIndex + 1;

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
                    else if (currentInteractObj.choices.Length > 0 && !choiceBox.activeSelf)
                    {
                        AppearChoice(currentInteractObj.choices);
                    }
                    else
                    {

                        if (choiceBox.activeSelf && choiceIndex == 0)
                        {
                            CallInteractionFunction(currentInteractObj.interactionId, choiceIndex);
                        }

                        StartCoroutine(DisappearBackground(textBox));
                        StartCoroutine(DisappearBackground(choiceBox));
                        currentInteractObj = null;
                    }
                }
            }

            // Choices
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SelectChoice(-1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SelectChoice(1);
            }
        }
    }

    // Interaction functions
    private void CallInteractionFunction(int interactionId, int choiceChosen)
    {
        // Finds the object's interaction id, then executes the functions connected to it
        // For choice chosen, 0 is top option and 1 is bottom option

        if (interactionId == 1)
        {
            if (choiceChosen == 0)
            {
                Vector3 moveDirection = playerScript.oldMoveVector;
                StartCoroutine(Push(moveDirection));
            }
        }
    }

    private IEnumerator Push(Vector3 moveDirection)
    {
        // Pushes an object in a direction
        Rigidbody2D rb = currentInteractObj.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection;
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector2(0, 0);
    }
}
