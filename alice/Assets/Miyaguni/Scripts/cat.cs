using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour {
	float catLife;
	float catTransparency = 0f;
	float catTransparencySpeed = 1f;

	SpriteRenderer sr;

	void Start(){
		sr = GetComponent<SpriteRenderer>();
		sr.color = new Color(1.0f, 1.0f, 1.0f, 0f);
		//猫の速度判定
		GameObject gameDirector = GameObject.Find("GameDirector");
		int catC = gameDirector.GetComponent<GameDirector>().catCount;
		if(catC==3){
			catLife = 1f;
			catTransparencySpeed = 1f;
		}else if(catC == 2){
			catLife = 0.75f;
			catTransparencySpeed = 1.3f;
		}else{
			catLife = 0.6f;
			catTransparencySpeed = 1.6f;
		}
	}
	
	
	void FixedUpdate () {
		//猫の処理
		catLife -= Time.deltaTime;
		catTransparency += Time.deltaTime * catTransparencySpeed;
		CapsuleCollider2D cc2d = GetComponent<CapsuleCollider2D>();
		if(catTransparency < 0.3f||catTransparency > 0.65f){
			cc2d.enabled = false;
		}else{
			cc2d.enabled = true;
		}

		sr.color = new Color(1.0f,1.0f,1.0f,Mathf.Sin(255 * catTransparency * Mathf.Deg2Rad));
		if(catLife < 0){
			GameObject catgene = GameObject.Find("CatGenerator");
			catgene.GetComponent<CatGenerator>().catgene();
			Destroy(gameObject);
		}
		Debug.Log(catTransparency);

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			//タップ成功の処理
			GameObject catgene = GameObject.Find("CatGenerator");
			catgene.GetComponent<CatGenerator>().catgene();
			GameObject GameDirector = GameObject.Find("GameDirector");
			GameDirector.GetComponent<GameDirector>().GetCat();
			Destroy(gameObject);
		}
	}
}
