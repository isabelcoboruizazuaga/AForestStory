using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D fisica;
    public SpriteRenderer sprite;
    public Animator anim;

    //Movimiento
    public int velocidad;
    public int fuerzaSalto;
    float entradaX = 0f;

    public int estrellas;
    public int vidas;
    public bool vulnerable;
    public bool muerto;

    //HUD
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        //Movimiento
        velocidad = 8;
        fuerzaSalto = 8;

        //Vidas
        vidas = 3;
        estrellas = 0;
        vulnerable = true;
        muerto = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Obtenci�n de input de mover hacia los lados
        entradaX = Input.GetAxis("Horizontal");

        //Control de animaci�n
        anim.SetFloat("velocidadX", Mathf.Abs(fisica.velocity.x));
        anim.SetFloat("velocidadY", fisica.velocity.y);

        //Mec�nica de salto
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
            //Si toca suelo salta autom�ticamente
            if (TocandoSuelo())
            {
                fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            }
            //Si no toca suelo debe ser una plataforma y solo salta si est� quieto
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
        //El scene manager carga la escena activa, para esto necesita el n�mero de �ndice de la escena

        //SceneManager.LoadScene("Menu");
    }

    public void setVidas()
    {
        canvas.GetComponent<HUDController>().CambioVida(vidas);
    }
    public void SetEstrellas()
    {
        canvas.GetComponent<HUDController>().SetEstrellas(estrellas);
    }
}
