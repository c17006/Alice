using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCtrl : MonoBehaviour
{
    AudioSource SE;
                   
    void Start ()
    {
        SE = GetComponent<AudioSource>();
    } 	
	
	void Update ()
    {
		
	}

    public void ARcamera()
    {
        SE.PlayOneShot(SE.clip);
        Invoke("ARcameraSceneLoad", 1.0f);        
    }

    void ARcameraSceneLoad()
    {
        SceneManager.LoadScene("ARcamera");
    }
}
