using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MobileKeyboardController : MonoBehaviour {

    private TouchScreenKeyboard keyboard;// キーボード
    private AudioSource audiosource;

    [SerializeField]
    private Text judgmentText;// 正誤を表示するテキスト
    [SerializeField]
    private AudioClip currectSE;// 正解音
    [SerializeField]
    private AudioClip incurrectSE;// 不正解音

    private bool isKeyboard = false;// キーボードが表示されているか
    private int cutend = 4;// 切れ端の枚数
    private float sceneTransitionDelay = 1.0f;



    void Start() {
        audiosource = GetComponent<AudioSource>();
    }

    void Update() {
        // キーボードが存在するのあら以下の処理を行う
        if (!isKeyboard) { return; }
        DetermineInput();
    }

    // キーボードを出す
    public void OpenKeyboard() {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        isKeyboard = true;
    }

    // 入力の判定をする
    private void DetermineInput() {
        if (!keyboard.active) {
            if (keyboard.text == "ウサギ" || keyboard.text == "うさぎ") {
                PlaySound(currectSE);
                judgmentText.text = "正解";
                GameMainCtrl.ceGet += cutend;
                GameMainCtrl.f_Q5 = true;
                isKeyboard = false;
                Invoke("GoCutEnd", sceneTransitionDelay);
            } else {
                PlaySound(incurrectSE);
                judgmentText.text = "不正解";
                isKeyboard = false;
            }
        }
    }

    private void PlaySound(AudioClip SE) {
        audiosource.PlayOneShot(SE);
    }

    // 切れ端取得画面に遷移する
    private void GoCutEnd() {
        SceneManager.LoadScene("CutEnd");
    }
}
