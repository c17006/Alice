using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puzzle0 : MonoBehaviour {

	public GameObject Camera;
	public GameObject Piece_easy;
	public GameObject Piece_nomal;
	public GameObject Piece_hard;
	public GameObject PutPos_easy;
	public GameObject PutPos_nomal;
	public GameObject PutPos_hard;
	public GameObject Panel_easy;
	public GameObject Panel_nomal;
	public GameObject Panel_hard;

	public Text timeLimitText;

	public int timeLimit = 300;

	int count;
	bool touching = false;
	bool puzzleActive = true;

	Vector3 startPos;

	Camera cam;
	GameObject PutPos;
	GameObject MovingPiece;
	BoxCollider MovingPieceCol;
	Renderer MovingPieceRen;

	void Start () {
		StartCoroutine(countDown());
		cam = Camera.GetComponent<Camera>();
		switch(Mathf.FloorToInt(GameMainCtrl.ceNum / 8)){
			case 0:
				Piece_easy.SetActive(true);
				PutPos_easy.SetActive(true);
				Panel_easy.SetActive(true);
				count = 20;
				break;
			case 1:
				Piece_nomal.SetActive(true);
				PutPos_nomal.SetActive(true);
				Panel_nomal.SetActive(true);
				count = 35;
				break;
			default:
				Piece_hard.SetActive(true);
				PutPos_hard.SetActive(true);
				Panel_hard.SetActive(true);
				count = 56;
				break;
		}
	}
	
	void FixedUpdate () {
		if(!puzzleActive){return;}
		if(Input.touchCount != 1){return;}
		Touch t = Input.GetTouch(0);
		transform.position = cam.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, -Camera.transform.position.z));
		if(t.phase == TouchPhase.Began){
			touching = true;
		}
		if(MovingPiece != null){
			MovingPiece.transform.position = cam.ScreenToWorldPoint(new Vector3(t.position.x, t.position.y, -Camera.transform.position.z));
			if(t.phase != TouchPhase.Ended){return;}
			if(PutPos != null && PutPos.name.Substring(0) == MovingPiece.name.Substring(9)){
				MovingPiece.transform.position = PutPos.transform.position;
				count--;
				if(count == 0){
					SceneManager.LoadScene("GameClear");
				}
			}else{
				MovingPiece.transform.position = startPos;
				MovingPieceCol.enabled = true;
			}
			MovingPieceRen.sortingOrder--;
			MovingPiece = null;
			PutPos = null;
			touching = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if(touching && other.gameObject.tag == "K_Piece"){
			startPos = other.gameObject.transform.position;
			MovingPiece = other.gameObject;
			touching = false;
			MovingPieceCol = other.gameObject.GetComponent<BoxCollider>();
			MovingPieceCol.enabled = false;
			MovingPieceRen = other.gameObject.GetComponent<Renderer>();
			MovingPieceRen.sortingOrder++;
		}
		if(other.gameObject.tag == "K_PutPos"){
			PutPos = other.gameObject;
		}
	}
	IEnumerator countDown(){
		while(timeLimit >= 0){
			yield return new WaitForSeconds(1.0f);
			timeLimit--;
			timeLimitText.text = timeLimit.ToString();
		}
		SceneManager.LoadScene("GameOver");
	}
}
