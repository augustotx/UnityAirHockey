using UnityEngine;

public class AI_Goleiro : MonoBehaviour
{
    public Transform target;
    public float accelerationMagnitude = 50f; // Força de movimento
    public float dampingDistance = 0.5f;     // Distância para começar a frear
    public float maxSpeed = 10f;             // Velocidade máxima para evitar que ele voe

    private Rigidbody2D rb;

    [Header("Limites da Área")]
    public float xLimit = 4.4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Garante que o drag do Rigidbody ajude a parar o objeto
        rb.linearDamping = 2f;
    }

    void FixedUpdate()
    {
        if (target == null || rb == null) return;

        // 1. CALCULAR MOVIMENTO NO EIXO X
        float targetX = target.position.x;
        float currentX = transform.position.x;
        float distanceX = targetX - currentX;

        // Calcula a direção (1 para direita, -1 para esquerda)
        float directionX = Mathf.Sign(distanceX);
        float absDistanceX = Mathf.Abs(distanceX);

        // Se estiver muito perto, reduz a força (Damping) para não passar do ponto
        float speedMultiplier = Mathf.Clamp01(absDistanceX / dampingDistance);

        Vector2 force = new Vector2(directionX * accelerationMagnitude * speedMultiplier, 0);

        // 2. APLICAR FORÇA (Apenas se não ultrapassar a velocidade máxima)
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(force * rb.mass);
        }

        // 3. SISTEMA DE LIMITES (ESTRITO)
        ApplyBoundaries();
    }

    void ApplyBoundaries()
    {
        Vector2 pos = transform.position;
        Vector2 vel = rb.linearVelocity;
        bool outOfBounds = false;

        pos.y = 6.7f;

        // Limite X
        if (pos.x > xLimit) { pos.x = xLimit; vel.x = 0; outOfBounds = true; }
        else if (pos.x < -xLimit) { pos.x = -xLimit; vel.x = 0; outOfBounds = true; }

        if (outOfBounds)
        {
            rb.linearVelocity = vel;
        }
        transform.position = pos;
    }
}
