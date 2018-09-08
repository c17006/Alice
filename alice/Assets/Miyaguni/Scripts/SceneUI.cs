using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneUI : MonoBehaviour {
	public void GameStart(){
		//AudioClip audioClip = GetComponent<AudioClip>();
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.Play();
		Invoke("SceneMove", 1f);
	}

	void SceneMove(){
		SceneManager.LoadScene("Q3_game");
	}
}