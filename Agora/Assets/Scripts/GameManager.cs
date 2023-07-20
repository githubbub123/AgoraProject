using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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

    private string[] texts;
    private int textArrayIndex;
    private int choiceIndex;
    private bool textDisplayed = false;
    private bool appearing = false;
    private string[] choiceTexts;
    private AudioSource textScrollSFX;
    private Vector3 oldPlayerPos;
    private int playerElevated;

    public CanInteractWith currentInteractObj;
    private bool publicDebounce = false;

    public GameObject sleepingMask;

    void Start()
    {
        gm = this;
        textScrollSFX = GetComponent<AudioSource>();
        textBox.SetActive(false);
        choiceBox.SetActive(false);
    }

    public IEnumerator AppearText(CanInteractWith scr)
    {
        if (publicDebounce)
        {
           yield return null;
        }
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
        if (publicDebounce)
        {
            return;
        }

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
            textScrollSFX.PlayOneShot(textScrollSFX.clip, 1);
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

                        if (choiceBox.activeSelf)
                        {
                           bool dontDIsappear = CallInteractionFunction(currentInteractObj.interactionId, choiceIndex);
                            if (dontDIsappear)
                            {
                                return;
                            }
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

    private void DisplayInteractionDialoge(string interactionName)
    {
        CanInteractWith scr = GameObject.Find(interactionName).GetComponent<CanInteractWith>();
        StartCoroutine(AppearText(scr));
    }
    // Interaction functions
    private bool CallInteractionFunction(int interactionId, int choiceChosen)
    {
        // Finds the object's interaction id, then executes the functions connected to it
        // For choice chosen, 0 is top option and 1 is bottom option

        if (interactionId == 1)
        {
            print(choiceChosen);
            if (choiceChosen == 0)
            {
                // Pushing the chair
                Vector3 moveDirection = playerScript.oldMoveVector;
                StartCoroutine(Push(moveDirection));
            }
            else if (choiceChosen == 1)
            {
                // Climbing the chair
                StartCoroutine(ClimbChair(true, currentInteractObj));
                DisplayInteractionDialoge("InteractionId1");

            }
        }
        else if (interactionId == 2)
        {
            if (choiceChosen == 0)
            {
                // Gets the player off the chair
                StartCoroutine(ClimbChair(false, currentInteractObj));
            }
        }
        else if (interactionId == 3)
        {
            if (choiceChosen == 0)
            {
                // Changse the level
                FindObjectOfType<LevelChanger>().FadeToLevel();
            }
        }
        else if (interactionId == 4)
        {
            if (choiceChosen == 0)
            {
                // Putting on the sleeping mask
                Destroy(sleepingMask);
                FindObjectOfType<SwitchSprite>().PutMaskOn();
            }
        }
        else if (interactionId == 5)
        {
            if (choiceChosen == 0)
            {
                // Acquiring tony
                print(playerElevated);
                if (playerElevated > 0)
                {
                    currentInteractObj.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    currentInteractObj.gameObject.transform.SetParent(playerScript.gameObject.transform);
                    currentInteractObj.gameObject.transform.position = playerScript.gameObject.transform.position + new Vector3(-.25f, .6f, 0);

                    string[] texts = new string[] { "Congratulations. You have acquired my son.", "Keep him for now, he is quite the handful.", "(Glurrggg)", "Oh my, I am quite hungry.", "Go retrieve my food." };
                    string[] choices = new string[] { "Yes", "Yes" };

                    ChangeWaddlesQuest(texts, choices);
                }
                else
                {
                    DisplayInteractionDialoge("InteractionId5");
                    return true;
                }
            }
        }
        else if (interactionId == 6)
        {
            if (choiceChosen == 0)
            {
                // Resetting chair position
                GameObject chair = GameObject.Find("Chair");
                chair.transform.position = new Vector3(9.75f, -1.65f, 1);
            }
        }
        else if (interactionId == 7)
        {
            // Accepting Waddles offer to get his food
            DisplayInteractionDialoge("InteractionId7");
            return true;
        }
        return false;
    }

    private IEnumerator Push(Vector3 moveDirection)
    {
        // Pushes an object in a direction
        Rigidbody2D rb = currentInteractObj.gameObject.GetComponent<Rigidbody2D>();
        Vector2 newMoveDirection = new Vector2(moveDirection.x, moveDirection.y);
        rb.velocity += newMoveDirection * 10;
        //rb.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(1);
        //rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private IEnumerator ClimbChair(bool climbing, CanInteractWith myInteractObj)
    {
        // Getting the player and chair transforms then lerping the player to be ontop of the player
        float speed = 10f;

        Transform chairTransform = myInteractObj.gameObject.GetComponent<Transform>();
        Transform playerTransform = playerScript.gameObject.GetComponent<Transform>();

        playerScript.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        myInteractObj.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Vector3 destination;

        publicDebounce = true;

        if (climbing)
        {
            destination = chairTransform.position + new Vector3(0, 1f, 0);
            oldPlayerPos = playerTransform.position;
        }
        else
        {
            destination = oldPlayerPos;
        }

        while (Vector3.Distance(playerTransform.position, destination) > 0)
        {
            print("gon");
            float lerpSpeed = speed * Time.deltaTime;
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, destination, lerpSpeed);
            yield return new WaitForSeconds(0);
        }

        print("finsihed");
        if (climbing)
        {
            myInteractObj.interactionMessage = new string[] {"(Do you want to climb off the chair?)"};
            myInteractObj.choices = new string[] { "Yes", "No"};
            myInteractObj.interactionId = 2;
            playerElevated += 1;
        }
        else
        {
            myInteractObj.interactionMessage = new string[] { "What a convinent chair!"};
            myInteractObj.choices = new string[] { "Push", "Climb"};
            myInteractObj.interactionId = 1;
            playerElevated -= 1;

            playerScript.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            myInteractObj.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
        }

        publicDebounce = false;
    }

    private void ChangeWaddlesQuest(string[] texts, string[] choices)
    {
        GameObject Waddles = GameObject.Find("Waddles");
        CanInteractWith scr = Waddles.GetComponent<CanInteractWith>();
        scr.interactionMessage = texts;
        scr.choices = choices;
        scr.interactionId = 7;
    }
}
