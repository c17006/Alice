using UnityEngine;


public class Player : MonoBehaviour {

    private GameController gamecontroller;
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed = 10.0f;


    void Start() {
        gamecontroller = GameObject.Find("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Move();
    }

    private void Move() {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        rb.AddForce(dir * moveSpeed);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.IndexOf("PassingPoints") == 0) {
            gamecontroller.PassingCheck(other.gameObject.tag);
        }
    }
}
