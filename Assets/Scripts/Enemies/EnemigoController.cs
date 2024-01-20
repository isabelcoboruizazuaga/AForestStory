using System;
using System.Collections;
using UnityEngine;


public class EnemigoController : MonoBehaviour
{
    protected PlayerController playerController;
    protected GameObject player;

    public GameObject enemigo;
    public float velocidad;
    public Vector3 posicionFin;
    public Vector3 posicionInicial;
    private bool moviendoAFin;

    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;
    public bool flip = true; //Marca si hay giro o no basándose en la posición inicial del sprite en unity
    public bool staticFlip = false;

    public int finX, finY;

    // Start is called before the first frame update
    void Start()
    {
        //Se obtiene la posición inicial y se calcula la final
        posicionInicial = transform.position;
        posicionFin = new Vector3(posicionInicial.x + finX, posicionInicial.y + finY, posicionInicial.z);
        moviendoAFin = true;

        //Inicializamos los componentes
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        enemigo = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }

    protected virtual void MoverEnemigo()
    {
        Vector3 posicionDestino;

        if (moviendoAFin)
        {
            posicionDestino = posicionFin;

            if (!staticFlip)
                sprite.flipX = flip;
        }
        else
        {
            posicionDestino = posicionInicial;

            if (!staticFlip)
                sprite.flipX = !flip;
        }


        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        //mueve desde la posicion en la que estemos ahota a la posicion que se le indica a esa velocidad

        if (transform.position == posicionFin)
            moviendoAFin = false;
        if (transform.position == posicionInicial)
            moviendoAFin = true;
    }


    public void Flip()
    {
        if (rb.velocity.x > 0f)
        {
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0f)
        {
            sprite.flipX = true;
        }
    }

    public void DestruyeObjeto()
    {
        Destroy(enemigo);
    }


    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().vulnerable )
        {
            player = collision.gameObject;
            this.playerController = player.GetComponent<PlayerController>();

            playerController.sonidoDolor.Play();

            this.playerController.vulnerable = false;

            //vidas-- <=1 -> prrimero compara y luego resta (por ejemplo 2<1, vidas=1)
            //si es --vidas seria al reves, primero resta y despues compara
            if (this.playerController.vidas-- <= 1)
            {
                //sonido.Play();
                StartCoroutine(FinJuego(collision));
            }
            else
            {
                StartCoroutine(QuitaVida(collision));
            }

            //Para cambiar las vidas en el hud
            playerController.setVidas();

        }
    }

    /**
     * Se llama tras recibir daño, cambia el color a rojo y tras un tiempo vuelve al player vulnerable de nuevo
     */
    protected IEnumerator QuitaVida(Collision2D collision)
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(3f);
        this.playerController.vulnerable = true;
        player.GetComponent<SpriteRenderer>().color = Color.white; //blanco lo deja con el color normal
    }

    protected IEnumerator FinJuego(Collision2D collision)
    {
        Camera.main.transform.parent = null; //dejamos a la cámara huérfana

        //Seteamos la muerte
        playerController.anim.SetBool("dead", true);
        player.GetComponent<SpriteRenderer>().color = Color.red;
        player.GetComponent<Collider2D>().enabled = false; //Para que atraviese el escenario y se caiga

        //Espera antes de terminar el juego
        yield return new WaitForSeconds(1f);
        this.playerController.FinJuego();
        // collision.gameObject.GetComponent<PlayerController>().Perder();
    }
}
