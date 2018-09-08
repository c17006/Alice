using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour {

	public GameObject cameraObject;
	public GameObject seconds;
	public GameObject retryButton;
	public GameObject retireButton;
	public GameObject startButton;
	public GameObject coment1;
	public GameObject coment2;

	public BoxCollider[] maruTrueCol;
	public BoxCollider[] maruFalseCol;
	public SpriteRenderer[] maruTrueSprite;
	public SpriteRenderer[] maruFalseSprite;
	public Text timeText;
	public Text mistakeCountText;

	public int timeLimit = 60;

	Vector3 cameraStartPos;
	Vector3 touchStartPos;
	Vector3 targetPos;
	Vector3 movingCenter;

	Vector2 pinchCenter;

	int mistakeCount = 5;
	float Dist;
	float changingDist;

	bool cameraMoving = false;
	bool gameActive = false;

	Camera Camera;
	BoxCollider touchCol;

	void Start(){
		mistakeCountText.text = "あと" + mistakeCount + "こ";
		timeText.text = "" + timeLimit;
		cameraStartPos = cameraObject.transform.position;
		Camera = cameraObject.GetComponent<Camera>();
		touchCol = gameObject.GetComponent<BoxCollider>();
	}

	void FixedUpdate(){
		if(!gameActive){return;}
		//タッチ・スワイプ
		if(Input.touchCount == 1){
			Touch t = Input.GetTouch(0);
			Vector3 touchingPos = Input.touches[0].position;
			if(t.phase == TouchPhase.Began){
				touchStartPos = Input.touches[0].position;
				movingCenter = cameraObject.transform.position;
				cameraMoving = true;
			}
			if(t.phase == TouchPhase.Ended){
				Dist = Vector2.Distance(touchingPos, touchStartPos);
				if(Dist <= 10f){
					TouchScreen(Camera.ScreenToWorldPoint(new Vector3(touchingPos.x, touchingPos.y, 0 - cameraObject.transform.position.z)));
				}
				cameraMoving = false;
			}
			Vector3 v = touchingPos - touchStartPos;
			if(cameraMoving){
				Vector3 position = movingCenter;
				position.x += -v.x * (cameraObject.transform.position.z / cameraStartPos.z) / 40;
				position.y += -v.y * (cameraObject.transform.position.z / cameraStartPos.z) / 40;
				cameraObject.transform.position = LimitingPos(position);
			}
		}
		//ピンチイン・アウト
		if(Input.touchCount == 2){
			Touch t1 = Input.GetTouch(0);
			Touch t2 = Input.GetTouch(1);
			Vector2 touchPos1 = Input.touches[0].position;
			Vector2 touchPos2 = Input.touches[1].position;
			changingDist = Vector2.Distance(touchPos1, touchPos2);
			if(t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began){
				pinchCenter = (touchPos1 + touchPos2) / 2;
				movingCenter = cameraObject.transform.position;
				Dist = changingDist;
				targetPos = Camera.ScreenToWorldPoint(new Vector3(pinchCenter.x, pinchCenter.y, -cameraObject.transform.position.z));
				cameraMoving = true;
			}
			if(t1.phase == TouchPhase.Ended || t2.phase == TouchPhase.Ended){
				cameraMoving = false;
			}
			if(cameraMoving){
				Vector3 position = movingCenter;
				position.x += (targetPos.x - cameraStartPos.x) * ((changingDist - Dist) / 250);
				position.y += (targetPos.y - cameraStartPos.y) * ((changingDist - Dist) / 250);
				position.z += (targetPos.z - cameraStartPos.z) * ((changingDist - Dist) / 250);
				cameraObject.transform.position = LimitingPos(position);
			}
		}
	}

	void TouchScreen(Vector3 pos){
		touchCol.center = pos;
	}

	void OnTriggerEnter(Collider other){
		int i = int.Parse(other.gameObject.name);
		maruTrueCol[i].enabled = false;
		maruFalseCol[i].enabled = false;
		maruTrueSprite[i].enabled = true;
		maruFalseSprite[i].enabled = true;
		mistakeCount--;
		mistakeCountText.text = "あと" + mistakeCount + "こ";
		if(mistakeCount == 0){
			GameMainCtrl.ceGet = 2;
			GameMainCtrl.f_Q2 = true;
			SceneManager.LoadScene("CutEnd");
		}
	}

	public void OnStartButton(){
		gameActive = true;
		mistakeCountText.gameObject.SetActive(true);
		timeText.gameObject.SetActive(true);
		seconds.SetActive(true);
		startButton.SetActive(false);
		coment1.SetActive(false);
		StartCoroutine(CountDown());
	}

	public void OnRetryButton(){
		mistakeCount = 5;
		timeLimit = 60;
		timeText.text = timeLimit.ToString();
		retryButton.SetActive(false);
		retireButton.SetActive(false);
		coment2.SetActive(false);
		timeText.gameObject.SetActive(true);
		seconds.gameObject.SetActive(true);
		mistakeCountText.gameObject.SetActive(true);
		for(int i = 0; i < maruTrueCol.Length; i++){
			maruFalseCol[i].enabled = true;
			maruTrueCol[i].enabled = true;
			maruFalseSprite[i].enabled = false;
			maruTrueSprite[i].enabled = false;
		}
		StartCoroutine(CountDown());
	}

	public void OnRetireButton(){
		SceneManager.LoadScene("GameMain");
	}

	IEnumerator CountDown(){
		for(int i = timeLimit; i >= 0; i--){
			yield return new WaitForSeconds(1.0f);
			timeLimit--;
			timeText.text = timeLimit.ToString();
		}
		gameActive = false;
		retryButton.SetActive(true);
		retireButton.SetActive(true);
		coment2.SetActive(true);
		seconds.SetActive(false);
		timeText.gameObject.SetActive(false);
		mistakeCountText.gameObject.SetActive(false);
	}

	Vector3 LimitingPos(Vector3 pos){
		pos.z = Mathf.Clamp(pos.z, cameraStartPos.z, -5);
		pos.x = Mathf.Clamp(pos.x, -(22f / -cameraStartPos.z * pos.z + 22f), 22f / -cameraStartPos.z * pos.z + 22f);
		pos.y = Mathf.Clamp(pos.y, -(15f / -cameraStartPos.z * pos.z + 15f), 15f / -cameraStartPos.z * pos.z + 15f);
		return pos;
	}
}
