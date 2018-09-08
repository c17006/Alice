using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutEndCtrl : MonoBehaviour
{
    public Text cutend_num;
	
	void Start ()
    {
        cutend_num.text = GameMainCtrl.ceGet.ToString();
        GameMainCtrl.ceNum += GameMainCtrl.ceGet;

    }  	
	
	void Update ()
    {
		
	}

    public void GameMainBackButton()
    {
        // シーン遷移前に ceGet を初期化
        GameMainCtrl.ceGet = 0;

        SceneManager.LoadScene("GameMain");

    }
}
