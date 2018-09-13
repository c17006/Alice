using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButtonController : MonoBehaviour {

    public void RetryButton() {
        SceneManager.LoadScene("Q4");
    }
}
