using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour {
	[SerializeField]
	GameObject TimeText;
	[SerializeField]
	GameObject SecondText;
	float GameTime;
	public int catCount;
	[SerializeField]
	GameObject catCountText;
	[SerializeField]
	GameObject RestartButton;
	[SerializeField]
	GameObject ExitButton;
	[SerializeField]
	GameObject catgene;
	[SerializeField]
	GameObject CountDownText;
	GameObject cat;

	void Start () {
		GameTime = 60.0f;
		catCount = 3;
		RestartButton.SetActive(false);
		ExitButton.SetActive(false);
	}
	
	void FixedUpdate () {
		cat = GameObject.FindWithTag("m_cat");
		GameTime = Mathf.Clamp(GameTime, 0.0f, 60.0f);
		Text time = TimeText.GetComponent<Text>();
		time.text = GameTime.ToString("F0");
		GameTime -= Time.deltaTime;
		catCountText.GetComponent<Text>().text = "残り " + catCount.ToString() + "回";
		Text cdText = CountDownText.GetComponent<Text>();

		if(GameTime < 0.5f){
			cdText.color = new Color((72f / 255f) , (158f / 255f), (206f / 255f), 255f);
			cdText.text = "ゲームオーバー";
			Destroy(cat);
			Destroy(catgene);
			Destroy(SecondText);
			time.text = "";
			RestartButton.SetActive(true);
			ExitButton.SetActive(true);
		}

		if(catCount == 0){
			Destroy(cat);
			time.text = "捕まえた!";
		}
	}

	public void GetCat(){
		catCount--;
		if(catCount == 0){
			CatClear();
		}
		AudioSource AS = GetComponent<AudioSource>();
		AS.Play();
	}

	void CatClear(){
		Text cdText = CountDownText.GetComponent<Text>();
		cdText.color = new Color((72f / 255f) , (158f / 255f), (206f / 255f), 255f);
		cdText.text = "ゲームクリア!";
		Destroy(cat);
		Destroy(catgene);
		Destroy(SecondText);
		GameMainCtrl.ceGet += 2;
		GameMainCtrl.f_Q3 = true;
		Invoke("Clear", 1.0f);
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Exit(){
		SceneManager.LoadScene("Q3");
	}

	void Clear(){
		SceneManager.LoadScene("CutEnd");
	}
}