using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMainCtrl : MonoBehaviour
{    
    // タイマー表示
    public Text timer;
    Text timertext; 
    // ce = CutEnd = 切れはし
    public Text cutend;
    public static int ceNum = 0;
    public static int ceGet = 0;
    // ゲームスタートフラグ 
    public static bool f_gamestart = false;
    public GameObject gameStartSet;
    // ヒントウィンドウ    
    public GameObject[] hinttext = new GameObject[6];
    public GameObject hintclose;
    // ms = Musical score = 楽譜  // 楽譜ゲットするためのフラグ
    public GameObject ms;
    public GameObject msButton;
    bool f_ms0;
    bool f_ms1;
    bool f_ms2;
    bool f_ms3;
    bool f_ms4;
    bool f_ms5;
    public static bool f_msButton = false;
    // ARマーカーのフラグ
    public static bool f_alice = false;
    public static bool f_card1 = false;
    public static bool f_card4 = false;
    public static bool f_card5 = false;
    public static bool f_card10 = false;
    //public static bool f_butterfly = false;
    public static bool f_Q2 = false;
    public static bool f_Q3 = false;
    public static bool f_Q4 = false;
    public static bool f_Q5 = false;
    public static bool f_Q6 = false;
    //タイムアップcanvas
    public GameObject timeupCanvas;

    void Start ()
    {                       
        timertext = timer.GetComponent<Text>();
        hintclose.SetActive(false);
        for(int i = 0; i < hinttext.Length; i++)
        {
            hinttext[i].SetActive(false);
        }                       
        if(f_gamestart)
        {
            gameStartSet.SetActive(false);
        }               
        CutEndDisplay();
        MSreset();
        ms.SetActive(false);          
        if (!f_msButton)
        {
            msButton.SetActive(false);
        }  

        if(f_alice && f_card1 && f_card4 && f_card5 && f_card10 && f_Q2 && f_Q3 && f_Q4 && f_Q5 && f_Q6)
        {
            timeupCanvas.SetActive(true);
        }
        else
        {
            timeupCanvas.SetActive(false);
        }
    }
    
    void Update ()
    {
        TimeDisplay();                 
    }       

    public void GameStart()
    {
        // ゲームをスタートし、タイマーのカウントを開始。
        gameStartSet.SetActive(false);
        f_gamestart = true;
        TimeCtrl.f_count = true;
    }
    
    // タイマーの表示
    void TimeDisplay()
    {                 
        int minute = (int)TimeCtrl.countTime / 60;
        int second = (int)TimeCtrl.countTime % 60;

        // memo- countTimeの条件文だけできれいに分けられそう。
        if (second < 10)
            timertext.text = minute.ToString("F0") + " : 0" + second.ToString("F0");
        else if (minute < 10)
            timertext.text = "0" + minute.ToString("F0") + " : " + second.ToString("F0");
        else if (second < 10 && minute < 10)
            timertext.text = "0" + minute.ToString("F0") + " : 0" + second.ToString("F0");
        else
            timertext.text = minute.ToString("F0") + " : " + second.ToString("F0");   
    }

    // ページの切れ端の表示
    void CutEndDisplay()
    {
        cutend.GetComponent<Text>().text = ceNum.ToString();
    }

    // ARカメラ
    public void ARcamera()
    {
        SceneManager.LoadScene("ARcamera");
    }

    // ヒントウィンドウの表示
    public void HintWindow(int num)
    {
        // for文 findでいい気がする
        hintclose.SetActive(false);
        for (int i = 0; i < hinttext.Length; i++)
            hinttext[i].SetActive(false);

        switch (num)
        {
            case 0:　//赤                 
                hinttext[0].SetActive(true);                 
                break;

            case 1:　//青                  
                hinttext[1].SetActive(true);
                break;

            case 2:　//紫                
                hinttext[2].SetActive(true);
                break;

            case 3:　//橙                   
                hinttext[3].SetActive(true);
                break;

            case 4:　//黄
                hinttext[4].SetActive(true);
                break;

            case 5:　//緑
                hinttext[5].SetActive(true);
                break;

            default:
                break;
        }
        hintclose.SetActive(true);

        MSflag(num);
    }

    // ヒントウィンドウの非表示
    public void HintCloseButton()
    {
        GameObject hintObj = GameObject.FindGameObjectWithTag("N_HintWindow");
        hintObj.SetActive(false);
        hintclose.SetActive(false);
    }

    void MSflag(int num)
    {
        if (f_ms0 && num == 1)
        {
            f_ms0 = false;
            f_ms1 = true;
        }
        else if (f_ms1 && num == 0)
        {
            f_ms1 = false;
            f_ms2 = true;
        }
        else if (f_ms2 && num == 4)
        {
            f_ms2 = false;
            f_ms3 = true;
        }
        else if (f_ms3 && num == 5)
        {
            f_ms3 = false;
            f_ms4 = true;
        }
        else if (f_ms4 && num == 2)
        {
            f_ms4 = false;
            f_ms5 = true;
        }
        else if (f_ms5 && num == 3)
        {
            f_ms5 = false;
            f_msButton = true;
            msButton.SetActive(true);
            MSon();
        }
        else
        {
            MSreset();
        }
    }

    void MSreset()
    {
        f_ms0 = true;
        f_ms1 = false;
        f_ms2 = false;
        f_ms3 = false;
        f_ms4 = false;
        f_ms5 = false;
    } 
    
    public void MSon()
    {
        ms.SetActive(true);
    }

    public void MScloseButton()
    {
        ms.SetActive(false);
    }

    public void PuzzleButton()
    {
        TimeCtrl.f_timeup = true;
        timeupCanvas.SetActive(false);
        SceneManager.LoadScene("Q8");
    }
}   