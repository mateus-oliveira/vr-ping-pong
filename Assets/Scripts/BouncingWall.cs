using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingWall : MonoBehaviour
{
    [SerializeField] private GameObject aiRacket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ball")) {
            TeleportRacketToBall(collision.gameObject);
        }
    }

    private void TeleportRacketToBall(GameObject ball) {
        Vector3 newPosition = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
        aiRacket.transform.position = newPosition;
        aiRacket.GetComponent<AIRacket>().startPosition = newPosition;
    }
}
