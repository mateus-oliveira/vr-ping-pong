using UnityEngine;

public class ResetBall : MonoBehaviour
{
    public float force;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ResetBallAndAddForce();
    }

    void ResetBallAndAddForce() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            transform.position = initialPosition;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
        }
    }
}
