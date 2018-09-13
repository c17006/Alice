using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab;

    // 各種UI
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject retryButton;
    [SerializeField]
    private GameObject returnButton;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text gametimerText;
    [SerializeField]
    private Text countdownText;

    // サウンド
    private AudioSource audiosource;
    [SerializeField]
    AudioClip countSE;
    [SerializeField]
    AudioClip currectSE;
    [SerializeField]
    AudioClip incurrectSE;

    private GameObject PlayerObject;
    // ゲーム時間
    private float gametime = 60.0f;
    // ゲームが開始しているか
    private bool isStarted = false;
    // 取得する切れ端の枚数
    private int cutend = 3;
    // クリアシーンに遷移する際のディレイ
    private float clearDelay = 1.0f;

    //カウントダウンの秒数
    private int countSeconds = 3;
    private float countdownInterval = 1.0f;

    // 通過点のフラグ
    private bool[] isPassingPoints = new bool[4] { true, false, false, false };
    // 通過時に表示するイメージ
    [SerializeField]
    private GameObject[] currectImages;
    // 通過点を通過した数
    private int passingCount = 0;


    void Start() {
        audiosource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!isStarted) { return; }
        CountGameTime();
    }

    public void OnClickStart() {
        panel.SetActive(false);
        startButton.SetActive(false);
        StartCoroutine(CountDown());
    }

    // カウントダウンを行う
    IEnumerator CountDown() {
        WaitForSeconds cashedWait = new WaitForSeconds(countdownInterval);
        PlaySound(countSE);
        for (int i = countSeconds; i >= 0; i--) {
            if (i == 0) {
                countdownText.text = "GO!";
            } else {
                countdownText.text = i.ToString();
            }
            yield return cashedWait;
        }
        Destroy(countdownText);
        isStarted = true;
        PlayerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    // 通過チェック
    public void PassingCheck(string tag) {
        if (tag == "PassingPoints" + (passingCount + 1) && isPassingPoints[passingCount]) {
            PlaySound(currectSE);
            currectImages[passingCount].SetActive(true);
            isPassingPoints[passingCount] = false;
            passingCount++;
            // 最後の通過点を通ったら以下の処理を行う
            if (passingCount == isPassingPoints.Length) {
                Clear();
                return;
            }
            isPassingPoints[passingCount] = true;
        } else {
            PlaySound(incurrectSE);
            Reset();
        }
    }

    // ゲームの残り時間の計算
    private void CountGameTime() {
        if (gametime > 0) {
            gametime -= Time.deltaTime;
            gametimerText.text = gametime.ToString("F0");
        }

        // ゲームタイムが0以下のときゲームオーバー
        if (gametime <= 0) {
            GameOver();
        }
    }

    private void PlaySound(AudioClip SE) {
        audiosource.PlayOneShot(SE);
    }

    private void Reset() {
        PlayerObject.transform.position = Vector3.zero;
        passingCount = 0;

        // 通過フラグをリセットする
        for (int i = 0; i < isPassingPoints.Length; i++) {
            if (i == 0) {
                isPassingPoints[i] = true;
            } else {
                isPassingPoints[i] = false;
            }
        }

        // 通過イメージを非表示にする
        for (int j = 0; j < currectImages.Length; j++) {
            currectImages[j].SetActive(false);
        }
    }

    private void GameOver() {
        gametimerText.text = null;
        retryButton.SetActive(true);
        returnButton.SetActive(true);
    }

    private void Clear() {
        GameMainCtrl.ceGet += cutend;
        GameMainCtrl.f_Q4 = true;
        gametimerText.text = null;
        Invoke("GoCutEnd", clearDelay);
    }

    private void GoCutEnd() {
        SceneManager.LoadScene("CutEnd");
    }
}