using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    private Animator anim;

    public bool estrella = false;
    public int vidasExtra = 0;
    private bool entrado;


    public AudioSource sonidoEstrella;
    public AudioSource sonidoZanahoria;

    // Start is called before the first frame update
    void Start()
    {
        entrado = false;

        //Animaciones
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !entrado)
        {
            anim.SetBool("tocada", true);

            entrado = true; //así se evita que al pasar dos veces antes de que desaparezca se dupliquen los puntos

            PlayerController jugador = collision.gameObject.GetComponent<PlayerController>();

            if (estrella)
            {
                sonidoEstrella.Play();
                jugador.estrellas++; //Si es una estrella se suma a las conseguidas
                jugador.SetEstrellas();
            }
            else
            {
                sonidoZanahoria.Play();
                if (jugador.vidas < 3)
                {
                    jugador.vidas += vidasExtra;
                    jugador.setVidas();
                }
            }
            Destroy(gameObject, 1); //destruye el objeto en 1 segundo
        }
    }
}
