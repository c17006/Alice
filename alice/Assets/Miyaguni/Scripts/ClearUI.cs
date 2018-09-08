using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearUI : MonoBehaviour {
	Text sr;
	[SerializeField]
	float a;
	void Start(){
		//yield return new WaitForSeconds(1.0f);
		sr = gameObject.GetComponent<Text>();
	}

	void FixedUpdate(){
		//float num = Mathf.Sin(180 * catTransparency);
		float num = Mathf.PingPong(Time.time * a, 1);
		Mathf.Clamp01(num);
		sr.text = num.ToString();
		sr.color = new Color(0f, 0f, 0f, num*255f);
	}
}