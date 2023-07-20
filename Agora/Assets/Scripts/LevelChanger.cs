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
        SceneManager.LoadScene("RealWorld1");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
