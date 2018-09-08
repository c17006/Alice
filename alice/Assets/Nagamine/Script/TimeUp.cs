using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeUp : MonoBehaviour
{             
    public GameObject timeupCanvas;

    void Start ()
    {
		
	}  	
	
	void Update ()
    {
		
	}

    public void PuzzleButton()
    {
        TimeCtrl.f_timeup = true;
        timeupCanvas.SetActive(false);
        SceneManager.LoadScene("Q8");
    }
}
