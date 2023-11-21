using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Movimiento
    public int velocidad;
    public int fuerzaSalto;
    public Rigidbody2D fisica;
    public SpriteRenderer sprite;
    float entradaX = 0f;

    public int estrellas;
    public int vidas;
    public bool vulnerable;


    // Start is called before the first frame update
    void Start()
    {
        //Movimiento
        velocidad = 8;
        fuerzaSalto = 8;

        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        //Vidas
        vidas = 3;
        vulnerable = true;
        estrellas = 0;
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
        //Si se le da a salto
        if (Input.GetKeyDown(KeyCode.Space) ){
            //Si toca suelo salta automáticamente
            if (TocandoSuelo())
            {
                fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            }
            //Si no toca suelo debe ser una plataforma y solo salta si está quieto
            else
            { 
                if ((Mathf.Abs(fisica.velocity.y) < 0.5f))
                fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

            }
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

    public void FinJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //El scene manager carga la escena activa, para esto necesita el número de índice de la escena

        //SceneManager.LoadScene("Menu");
    }


}
