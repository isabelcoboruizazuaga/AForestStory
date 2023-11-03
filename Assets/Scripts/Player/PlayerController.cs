using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento
    public int velocidad;
    public int fuerzaSalto;
    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    float entradaX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Movimiento
        velocidad = 8;
        fuerzaSalto = 8;

        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Obtención de input de mover hacia los lados
        entradaX = Input.GetAxis("Horizontal");

        //Mecánica de salto
        Salto();
        //Giro de dibujo del personaje
        Flip();
    }

    private void FixedUpdate()
    {
       
        //Movimiento
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
        //Asignamos la velocidad al cuerpo, la velocidad es la velocidad establecida por las flechas pulsadas y se conserva la de y
    }

    public void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            //Se le añade a la figura fuerza para saltar (10) al pulsar espacio
        }
    }

    public void Flip()
    {
        if (fisica.velocity.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (fisica.velocity.x < 0f)
        {
            sprite.flipX = true;
        }
    }

    private bool TocandoSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast(
            transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        //se lanza el rayo desde mas abajo del centro (-2f) de nuestro objeto unos 20 cm (0.2f), va hacia abajo por el down

        return toca.collider != null;
    }
}
