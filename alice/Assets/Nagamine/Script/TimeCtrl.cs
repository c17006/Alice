using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class TimeCtrl : MonoBehaviour
{
    public static float countTime = 1200;     
    public static bool f_count = false;
    public static bool f_timeup = false;
    public GameObject timeupCanvas;

    void Start ()
    {             
        DontDestroyOnLoad(this.gameObject);         
        timeupCanvas.SetActive(false);
    }   
	
	void Update ()
    {
        if (!f_timeup)
        {
            CountTime();  
        }              
    }

    void CountTime()
    {
        if (f_count && countTime > 0)
        {
            Timer();
        }

        if (f_count && countTime <= 0)
        {
            timeupCanvas.SetActive(true);
        }
    }

    void Timer()
    {
        countTime -= Time.deltaTime;        
    }     
    
    public void PuzzleButton()
    {               
        f_timeup = true;
        timeupCanvas.SetActive(false);
        SceneManager.LoadScene("Q8");
    }
}
