using UnityEngine;

public class RacketBall : MonoBehaviour
{
    // A referência para o Rigidbody da bola
    public Rigidbody bolaRigidbody;

    // Força de quique aplicada à bola
    public float forcaDeQuique = 10f;

    // Método que é chamado quando a colisão ocorre
    private void OnCollisionEnter(Collision colisao)
    {
        // Verifica se a colisão foi com a bola
        if (colisao.gameObject.CompareTag("Ball"))
        {
            // Obtém a direção da colisão
            Vector3 direcaoDeColisao = colisao.contacts[0].normal;

            // Calcula a direção contrária à direção da colisão
            Vector3 direcaoContraria = -direcaoDeColisao;

            // Aplica uma força à bola na direção contrária
            bolaRigidbody.AddForce(direcaoContraria * forcaDeQuique, ForceMode.Impulse);
        }
    }
}