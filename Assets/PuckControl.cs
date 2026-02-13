using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    public Text PScore;
    public Text EScore;
    public Transform player, enemy;

    private Rigidbody2D rb2d;               // Define o corpo rigido 2D que representa a bola
    public int playerScore;
    public int aiScore;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Inicializa o objeto bola
        playerScore = 0;
        aiScore = 0;
        PScore.text = playerScore.ToString();
        EScore.text = aiScore.ToString();
    }

    // Determina o comportamento da bola nas colisões com os Players (raquetes)
    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 vel = rb2d.linearVelocity;
        if (coll.collider.CompareTag("Player"))
        {
            rb2d.linearVelocity = vel;
        }

        if (coll.collider.CompareTag("Mallet"))
        {
            vel.y *= -1;
            rb2d.linearVelocity = vel;
        }
    }

    void Update()
    {
        Vector2 vel = rb2d.linearVelocity;
        var pos = transform.position;
        if (pos.x > 4.4f)
        {
            pos.x = 4.4f;
            vel.x *= -1;
        }
        else if (pos.x < -4.4f)
        {
            pos.x = -4.4f;
            vel.x *= -1;
        }

        // Limites Verticais (Eixo Y)
        if (pos.y > 7f)
        {
            pos.y = 7f;
            if (pos.x > -2.7f && pos.x < 2.7f)
            {
                playerScore += 1;
                PScore.text = playerScore.ToString();
                pos.x = 0;
                pos.y = 0;
                vel.x = 0;
            }
            vel.y = 0;
            rb2d.linearVelocity = vel;
            transform.position = pos;
            return;
        }
        else if (pos.y < -7f)
        {
            pos.y = -7f;
            if (pos.x > -2.7f && pos.x < 2.7f)
            {
                aiScore += 1;
                pos.y = 0;
                vel.x = 0;
                pos.x = 0;
                EScore.text = aiScore.ToString();
            }
            vel.y = 0;
            rb2d.linearVelocity = vel;
            transform.position = pos;
            return;
        }
        transform.position = pos;
        rb2d.linearVelocity = vel;
    }
    /*
        // Reinicializa a posição e velocidade da bola
        void ResetBall()
        {
            rb2d.linearVelocity = Vector2.zero;
            transform.position = Vector2.zero;
        }

        // Reinicializa o jogo
        void RestartGame()
        {
            ResetBall();
            Invoke("GoBall", 1);
        }
    */
}
