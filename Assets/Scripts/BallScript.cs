using System;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float bouncingForce;
    [SerializeField] private bool isFrozen;
    private GameObject playerRacket;
    private Vector3 initialPosition;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        playerRacket = GameObject.FindGameObjectWithTag("Racket");
    }

    // Update is called once per frame
    void Update()
    {
        SetBall();
    }

    void SetBall() {
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            transform.position = initialPosition;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            isFrozen = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        bool collidedWithRacket = collision.collider.CompareTag("Racket");
        bool collidedWithWall = collision.collider.CompareTag("BouncingWall");

        if (collidedWithRacket || collidedWithWall) {
            if (collidedWithRacket) {
                bouncingForce = 7f;
                if (isFrozen) {
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    isFrozen = false;
                }
            }

            // Obtém a direção da colisão
            Vector3 collisionDirection = collision.contacts[0].normal;

            // Calcula a direção contrária à direção da colisão
            Vector3 oppositeDirection = -collisionDirection;

            if (collidedWithWall) {
                bouncingForce = 5.5f;

                // Calcula a direção para o jogador ignorando o eixo Y.
                Vector3 directionToPlayer = (playerRacket.transform.position - transform.position).normalized;
                oppositeDirection = new Vector3(directionToPlayer.x, oppositeDirection.y, directionToPlayer.z);
            }

            // Aplica uma força à bola na direção contrária
            rb.AddForce(oppositeDirection * bouncingForce, ForceMode.Impulse);
        }
    }
}
