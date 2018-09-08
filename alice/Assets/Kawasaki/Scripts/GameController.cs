using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject PlayerPrefab;

    // 各種UI
    [SerializeField]
    private GameObject StartButton;
    [SerializeField]
    private GameObject RetryButton;
    [SerializeField]
    private GameObject ReturnButton;
    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private Text GameTimerText;
    [SerializeField]
    private Text CountDownText;

    // サウンド
    private AudioSource audiosource;
    [SerializeField]
    AudioClip countSE;
    [SerializeField]
    AudioClip currectSE;
    [SerializeField]
    AudioClip incurrectSE;

    private GameObject PlayerObject;
    private float gametime = 60.0f;
    private bool IsStarted = false;
    private int cutend = 3;

    // 通過点のフラグ
    private bool[] isPassingPoints = new bool[4] { true, false, false, false };
    // 通過時に表示するイメージ
    [SerializeField]
    private GameObject[] currectImages;

    private int passingCount = 0;


    ////トランプのフラグ
    //private bool flag1 = true;
    //private bool flag4 = false;
    //private bool flag5 = false;
    //private bool flag10 = false;

    ////通過時に表示するイメージ
    //[SerializeField]
    //private GameObject[] currectImages_Point1;
    //[SerializeField]
    //private GameObject currectImage_Point2;
    //[SerializeField]
    //private GameObject currectImage_Point3;
    //[SerializeField]
    //private GameObject currectImage_Point4;


    void Start() {
        audiosource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!IsStarted) { return; }

        CountGameTime();
    }

    public void OnClickStart() {
        Panel.SetActive(false);
        StartButton.SetActive(false);
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown() {
        PlaySound(countSE);
        for (int i = 3; i >= 0; i--) {
            if (i == 0) {
                CountDownText.text = "GO!";
            } else {
                CountDownText.text = i.ToString();
            }
            yield return new WaitForSeconds(1.0f);
        }
        Destroy(CountDownText);
        IsStarted = true;
        PlayerObject = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    // 通過チェック
    public void PassingCheck(string tag) {
        if (tag == "PassingPoints" + (passingCount + 1) && isPassingPoints[passingCount]) {
            PlaySound(currectSE);
            currectImages[passingCount].SetActive(true);
            isPassingPoints[passingCount] = false;
            isPassingPoints[passingCount + 1] = true;
            passingCount++;
        } else {
            PlaySound(incurrectSE);
            Reset();
        }

        // 最後のポイントを通過したらクリア
        if (isPassingPoints[isPassingPoints.Length - 1]) {
            Clear();
        }
    }

    ////通過チェック
    //public void PassingCheck(string tag) {
    //    if (tag == "D_card1" && flag1) {
    //        PlaySound(currectSE);
    //        CurrectImage_h1.SetActive(true);
    //        flag1 = false;
    //        flag4 = true;
    //    } else if (tag == "D_card4" && flag4) {
    //        PlaySound(currectSE);
    //        CurrectImage_d4.SetActive(true);
    //        flag4 = false;
    //        flag5 = true;
    //    } else if (tag == "D_card5" && flag5) {
    //        PlaySound(currectSE);
    //        CurrectImage_c5.SetActive(true);
    //        flag5 = false;
    //        flag10 = true;
    //    } else if (tag == "D_card10" && flag10) {
    //        PlaySound(currectSE);
    //        CurrectImage_s10.SetActive(true);
    //        //flag10 = false;
    //        Clear();
    //    } else {
    //        PlaySound(incurrectSE);
    //        Reset();
    //    }
    //}

    // ゲームの残り時間の計算
    private void CountGameTime() {
        if (gametime > 0) {
            gametime -= Time.deltaTime;
            GameTimerText.text = gametime.ToString("F0");

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
        GameTimerText.text = null;
        RetryButton.SetActive(true);
        ReturnButton.SetActive(true);
    }

    private void Clear() {
        GameMainCtrl.ceGet += cutend;
        GameMainCtrl.f_Q4 = true;
        GameTimerText.text = null;
        Invoke("GoCutEnd", 1.0f);

    }

    private void GoCutEnd() {
        SceneManager.LoadScene("CutEnd");
    }


    //void Update() {
    //    if (!startflag) { return; }

    //    if (countdowntime > 0) {
    //        countdowntime -= Time.deltaTime;
    //        CountDownText.GetComponent<Text>().text = countdowntime.ToString("F0");
    //        if (countdowntime <= 0) {
    //            Destroy(CountDownText);
    //            movestart = true;
    //        }
    //        return;

    //    } else {

    //        if (clear) { return; }

    //        if (time <= 0) {
    //            GameOver();
    //            return;
    //        }


    //        time -= Time.deltaTime;
    //        timeText.text = "残り" + time.ToString("F0") + "秒";


    //    }


    //}

    //public void Reset(GameObject destroy) {

    //    //GameObject Player = GameObject.FindWithTag("Player");
    //    Destroy(destroy);
    //    Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);

    //    CurrectImage_h1.SetActive(false);
    //    Currectimage_d4.SetActive(false);
    //    Currectimage_c5.SetActive(false);
    //    Currectimage_s10.SetActive(false);

    //    flag1 = true;
    //    flag4 = false;
    //    flag5 = false;
    //    flag10 = false;
    //}

    //public void Clear() {
    //    //SceneManager.LoadScene("Clear");
    //    timeText.text = null;
    //    clear = true;
    //    GameMainCtrl.ceGet += cutend;
    //    GameMainCtrl.f_Q4 = true;
    //    //GameObject.Find("RetryButton").SetActive(true);
    //    Invoke("GoCutEnd", 1.0f);
    //    //SceneManager.LoadScene("CutEnd");


    //}

    //void GameOver() {
    //    //SceneManager.LoadScene("GameOver");
    //    timeText.text = null;
    //    gameover = true;
    //    Retrybutton.SetActive(true);
    //    ReturnButton.SetActive(true);

    //}

    //public void StartButton() {
    //    StartButton.SetActive(false);
    //    Panel.SetActive(false);
    //    startflag = true;
    //    audiosource.PlayOneShot(countSE);

    //}

    //public void CurrectSE() {
    //    audiosource.PlayOneShot(currectSE);
    //}

    //public void inCurrectSE() {
    //    audiosource.PlayOneShot(incurrectSE);
    //}
    //void GoCutEnd() {
    //    SceneManager.LoadScene("CutEnd");
    //}
}