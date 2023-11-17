using UnityEngine;
using UnityEngine.SceneManagement;
//para poder cargar la escena

public class Player2 : MonoBehaviour
{

    public int velocidad;
    private Rigidbody2D fisica;

    float entradaX = 0f;
    public int fuerzaSalto;
    private SpriteRenderer sprite;

    //animaciones
    public Animator animator;

    public int puntos;
    private Transform playerTransform;


    public int vidas;
    public bool vulnerable;
    public bool muerto;


    public int tiempoNivel;
    public float tiempoInicio;
    public float tiempoEmpleado;


    public int powersUp;

    //private ControlDatosJuego datosJuego;


    // Start is called before the first frame update
    void Start()
    {
        velocidad = 10;
        fuerzaSalto = 10;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        puntos = 0;


        vidas = 3;
        vulnerable = true;
        muerto = false;
        playerTransform = GetComponent<Transform>();


        tiempoNivel = 100;
        tiempoInicio = Time.time;

        powersUp = 4;

        //datosJuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
        //Entre "" va el nombre que hemos puesto nosotros en el ide;
    }

    // Update is called once per frame
    void Update()
    {
        entradaX = Input.GetAxis("Horizontal");

        Salto();
        Flip();
        AnimarJugador();

        TiempoEmpleado();
    }

    private void TiempoEmpleado()
    {
        tiempoEmpleado = Time.time - tiempoInicio;

        // hud.SetTiempoTxt((int)(tiempoNivel - tiempoEmpleado));

        if (tiempoNivel - tiempoEmpleado < 0)
        {
            Perder();
        }
    }

    public void Ganado()
    {
        puntos = vidas * 100 + ((int)tiempoNivel - (int)(tiempoEmpleado));
        // datosJuego.Ganado = true;
        // datosJuego.Puntuacion = puntos;
        SceneManager.LoadScene("Ganado");
    }

    public void PowerUpsMenos()
    {
        powersUp--;
        if (powersUp < 1)
        {
            // SceneManager.LoadScene("Ganado");
            Ganado();
        }
    }
    public void IncrementarPuntos(int cantidad)
    {
        puntos += cantidad;

        //hud.SetPuntosTxt(puntos);
    }
    private void Salto()
    {
        if (muerto)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {



            //Esta fisica no es continua, al ser un salto solo es una vez por lo qu eno es necesario en el foxed update
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        }
    }

    private void Flip()
    {

        if (fisica.velocity.x > 0f)
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 0, 0f);

            //El 1 hay que calcularlo, basicamente la distancia entre ambos puntos de disparo

            sprite.flipX = false;
        }
        else if (fisica.velocity.x < 0f)
        {
            Quaternion nuevaRotacion = Quaternion.Euler(0, 180, 0f);


            sprite.flipX = true;
        }
    }
    private void AnimarJugador()
    {
        if (muerto)
        {
            animator.Play("idle");
            return;
        }

        if (!TocandoSuelo())
            animator.Play("jump");
        else
            if (fisica.velocity.x > 1 || fisica.velocity.x < -1 && fisica.velocity.y == 0)
            animator.Play("run");
        else
        {
            if (fisica.velocity.x < 1 || fisica.velocity.x > -1 && fisica.velocity.y == 0)
            {


            }
        }
    }
    //solo cuando estemos usando física
    private void FixedUpdate()
    {
        //float entradaX = Input.GetAxis("Horizontal");
        if (muerto)
        {
            //velocidad = 0;
            return;
        }
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y); //Movimiento
    }

    public bool TocandoSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast(
            transform.position + new Vector3(0, -2f, 0), Vector2.down, 0.2f);
        //se lanza el rayo desde mas abajo del centro (-2f) de nuestro objeto unos 20 cm (0.2f), va hacia abajo por el down

        return toca.collider != null;
    }

    public void FinJuego()
    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //El scene manager carga la escena activa, para esto necesita el número de índice de la escena

        SceneManager.LoadScene("Menu");
    }

    public void Perder()
    {
        //datosJuego.Ganado = false;
        SceneManager.LoadScene("Menu");
    }
}
