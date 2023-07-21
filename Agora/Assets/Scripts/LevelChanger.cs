using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(OnFadeComplete());

    }

    IEnumerator OnFadeComplete()
    {
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CharlieDevBackup"))
        {
            SceneManager.LoadScene("DreamWorldByChar");
            animator.SetTrigger("Fade_In");
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("DreamWorldByChar"))
        {
            SceneManager.LoadScene("TheFinalScene");
            animator.SetTrigger("Fade_In");
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TheFinalScene"))
        {
            SceneManager.LoadScene("Credits");
            animator.SetTrigger("Fade_In");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
