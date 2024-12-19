using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject aiRacket;
    [SerializeField] private GameObject playerRacket;
    [SerializeField] private GameObject spawnerCube;

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private float ballForce = 10f;
    [SerializeField] private float offset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(ShootBall), 3f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBallForce(float force) {
        ballForce = force;
    }

    public void ChangeRacketHeight(float height) {
        spawnerCube.transform.position = new Vector3(spawnerCube.transform.position.x, height, spawnerCube.transform.position.z);
    }

    public void ChangeRacketDistance(float distance) {
        spawnerCube.transform.position = new Vector3(distance, spawnerCube.transform.position.y, spawnerCube.transform.position.z);
    }

    public void ShootBall()
    {
        // Calcula uma posição aleatória dentro do spawnerCube
        Vector3 spawnPosition = GetRandomPositionInCube(spawnerCube);

        // Instancia a bola
        GameObject ball = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        // Move o AI Racket para a posição de spawn
        TeleportRacketToBall(ball);

        // Calcula a direção para o eixo Z (em direção ao jogador)
        Vector3 directionToPlayer = playerRacket.transform.position - spawnPosition;

        // Ajusta a força vertical (baixa) e mantém a direção horizontal
        Vector3 shootDirection = new Vector3(
            directionToPlayer.x, // Direção no eixo X
            0.3f,                // Força vertical ajustada (baixa para não subir muito)
            directionToPlayer.z + Random.Range(-offset, offset)  // Direção no eixo Z (para o outro lado da rede)
        ).normalized;

        // Aplica a força ajustável à bola
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector3.zero; // Zera a velocidade inicial
            ballRb.angularVelocity = Vector3.zero;

            float adjustedForce = ballForce; // Use ballForce ajustável no Inspector
            ballRb.AddForce(shootDirection * adjustedForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("A bola precisa ter um componente Rigidbody para aplicar força!");
        }
    }

    private Vector3 GetRandomPositionInCube(GameObject cube)
    {
        // Pega o tamanho do cubo
        Vector3 cubeSize = cube.transform.localScale;
        Vector3 cubeCenter = cube.transform.position;

        // Calcula um ponto aleatório dentro dos limites do cubo
        float randomX = Random.Range(-cubeSize.x / 2f, cubeSize.x / 2f);
        float randomY = Random.Range(-cubeSize.y / 2f, cubeSize.y / 2f);
        float randomZ = Random.Range(-cubeSize.z / 2f, cubeSize.z / 2f);

        // Transforma para a posição mundial
        Vector3 randomPosition = cubeCenter + new Vector3(randomX, randomY, randomZ);
        return randomPosition;
    }

    private void TeleportRacketToBall(GameObject ball) {
        Vector3 newPosition = new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z);
        aiRacket.transform.position = newPosition;
        aiRacket.GetComponent<AIRacket>().startPosition = newPosition;
    }
}
