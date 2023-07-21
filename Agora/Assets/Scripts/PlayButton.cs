using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnClickDo()
    {

        SceneManager.LoadScene("CharlieDevBackup");

    }

    public void StartAnimation()
    {
        ani.Play("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
