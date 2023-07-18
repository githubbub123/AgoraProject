using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToDream1 : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bed"))
        {
            FindObjectOfType<LevelChanger>().FadeToNextLevel();
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
