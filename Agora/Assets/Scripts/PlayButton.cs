using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnClickDo()
    {
        SceneManager.LoadScene("CharlieDevBackup");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
