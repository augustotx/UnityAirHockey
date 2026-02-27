using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var pos = transform.position;

        Vector2 dir = mousePos - playerPos;
        Vector2 vel = rb2d.linearVelocity;

        if ((dir.x <= 0.15f && dir.y <= 0.15f && dir.x >= -0.15f && dir.y >= -0.15f))
        {
            rb2d.linearVelocity = Vector2.zero;
            //pos.x = mousePos.x;
            //pos.y = mousePos.y;
            checkBounds(pos);
            return;
        }
        dir.Normalize();

        float force = 10.0f;

        Vector2 forceVec = dir * force;

        vel.x = forceVec.x;
        vel.y = forceVec.y;
        //pos.x = mousePos.x;
        //pos.y = mousePos.y;

        checkBounds(pos);
        rb2d.linearVelocity = vel;


    }

    void checkBounds(Vector2 pos)
    {
        bool is_out = false;
        if (pos.x > 4.4f)
        {
            pos.x = 4.4f;
            is_out = true;
            //vel.x = 0;
        }
        else if (pos.x < -4.4f)
        {
            pos.x = -4.4f;
            is_out = true;
            //vel.x = 0;
        }

        // Limites Verticais (Eixo Y)
        if (pos.y > -1f)
        {
            pos.y = -1f;
            is_out = true;
            //vel.y = 0;
        }
        else if (pos.y < -7f)
        {
            pos.y = -7f;
            is_out = true;
            //vel.y = 0;
        }
        if(is_out) transform.position = pos;
        //rb2d.linearVelocity = vel;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        source.Play();
    }
}