using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //source = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = transform.position;

        Vector2 dir = mousePos - playerPos;
        dir.Normalize();

        float force = 10.0f;

        Vector2 forceVec = dir * force;

        Vector2 vel = rb2d.linearVelocity;
        vel.x = forceVec.x;
        vel.y = forceVec.y;
        //pos.x = mousePos.x;
        //pos.y = mousePos.y;

        if (pos.x > 4.4f)
        {
            pos.x = 4.4f;
            vel.x = 0;
        }
        else if (pos.x < -4.4f)
        {
            pos.x = -4.4f;
            vel.x = 0;
        }

        // Limites Verticais (Eixo Y)
        if (pos.y > -1f)
        {
            pos.y = -1f;
            vel.y = 0;
        }
        else if (pos.y < -7f)
        {
            pos.y = -7f;
            vel.y = 0;
        }

        transform.position = pos;
        rb2d.linearVelocity = vel;

    }

    /*void OnCollisionEnter2D(Collision2D coll)
    {
        source.Play();
    }*/
}