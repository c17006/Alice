using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCtrl : MonoBehaviour
{
    AudioSource SE;
    
	void Start ()
    {
        SE = GetComponent<AudioSource>();        
	}       

	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SE.PlayOneShot(SE.clip);
            Invoke("MainSceneLoad", 1.0f);            
        } 		
	}

    void MainSceneLoad()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
