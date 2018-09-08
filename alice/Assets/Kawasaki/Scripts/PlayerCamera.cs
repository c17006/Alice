using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    private Vector3 cameraPos;
    private GameObject playerObject;

    // カメラの移動制限
    [SerializeField]
    private float minX = -3.0f;
    [SerializeField]
    private float maxX = 3.0f;
    [SerializeField]
    private float minZ = -0.5f;
    [SerializeField]
    private float maxZ = 0.5f;
    [SerializeField]
    private float y = 10.0f;


    void FixedUpdate() {
        playerObject = GameObject.FindWithTag("Player");

        // プレイヤーがいないなら以下の処理を行わない
        if (playerObject == null) { return; }

        cameraPos = playerObject.transform.position;
        gameObject.transform.position = new Vector3(Mathf.Clamp(cameraPos.x, minX, maxX), y,
                                                    Mathf.Clamp(cameraPos.z, minZ, maxZ));
    }
}
