using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARtouch : MonoBehaviour
{
    int ceGet_num = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTag();            
        }          
    }

    void ProcessTag()
    {
        // 獲得できるページの切れ端を初期化
        ceGet_num = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 1000))
        {
            switch (hit.collider.tag)
            {
                case "N_Alice":                       
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // aliceをタッチした時の処理 
                    if (!GameMainCtrl.f_alice)
                    {
                        GameMainCtrl.f_alice = true;
                        ceGet_num += 1;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                        //SceneManager.LoadScene("GameMain");
                    }                                
                    break;

                case "N_Card1":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // 切れ端をgetした時の処理 
                    if(!GameMainCtrl.f_card1)
                    {
                        GameMainCtrl.f_card1 = true;                          
                        ceGet_num += 1;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                    }                                 
                    break;

                case "N_Card4":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // 切れ端をgetした時の処理 
                    if (!GameMainCtrl.f_card4)
                    {
                        GameMainCtrl.f_card4 = true;
                        ceGet_num += 1;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                    }                      
                    break;

                case "N_Card5":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // 切れ端をgetした時の処理 
                    if (!GameMainCtrl.f_card5)
                    {
                        GameMainCtrl.f_card5 = true;
                        ceGet_num += 1;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                    }                              
                    break;

                case "N_Card10":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // 切れ端をgetした時の処理 
                    if (!GameMainCtrl.f_card10)
                    {
                        GameMainCtrl.f_card10 = true;
                        ceGet_num += 1;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                    }                              
                    break;

                case "N_Butterfly":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // 切れ端をgetした時の処理 
                    if (!GameMainCtrl.f_Q6)
                    {
                        GameMainCtrl.f_Q6 = true;
                        ceGet_num += 4;
                        GameMainCtrl.ceGet += ceGet_num;
                        SceneManager.LoadScene("CutEnd");
                    }                      
                    break;

                case "N_Twins":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // twinsをタッチした時の処理 
                    if (!GameMainCtrl.f_Q2)
                    {
                        SceneManager.LoadScene("Q2");
                    }                            
                    break;

                case "N_Cat":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // catをタッチした時の処理
                    if(!GameMainCtrl.f_Q3)
                    {
                        SceneManager.LoadScene("Q3");
                    }                           
                    break;

                case "N_Alice_golf":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // alice_golfをタッチした時の処理
                    if (!GameMainCtrl.f_Q4)
                    {
                        SceneManager.LoadScene("Q4");
                    }                            
                    break;

                case "N_WhiteRabbit":
                    Debug.LogFormat("rayが {0} に当たった。", hit.collider.tag);
                    // whiterabbitをタッチした時の処理
                    if (!GameMainCtrl.f_Q5)
                    {
                        SceneManager.LoadScene("Q5");
                    }                             
                    break;

                default:
                    break;
            }
        }   
    }
}
