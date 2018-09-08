using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARcameraCtrl : MonoBehaviour
{
    public GameObject backButton;
	
	void Start ()
    {
        if (!GameMainCtrl.f_gamestart)
            backButton.SetActive(false);
	}  	
	
	void Update ()
    {
		
	}

    public void GameMainBackButton()
    {
        SceneManager.LoadScene("GameMain");

    }
}
