using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
	[SerializeField]
	GameObject[] objects;
	[SerializeField]
	GameObject CountDownText;
	float countDown;

	void Start () {
		for(int i = 0; i < objects.Length; i++){
			objects[i].SetActive(false);
		}		
		countDown = 3.5f;
	}
	
	void Update () {
		countDown -= Time.deltaTime;
		Text CDText = CountDownText.GetComponent<Text>();
		CDText.text = countDown.ToString("F0");

		if(countDown < -0.5f){
			CDText.color = new Color((72f / 255f) , (158f / 255f), (206f / 255f), 0f);
			for(int i = 0; i < objects.Length; i++){
					objects[i].SetActive(true);
			}
			Destroy(gameObject);
		}else if(countDown < 0.5f){
			CDText.text = "スタート!";
		}
	}
}