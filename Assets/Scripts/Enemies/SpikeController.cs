using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;

    public bool isDeadly = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerController = player.GetComponent<PlayerController>();            

            //Si el player es vulnerable causa daño como un enemigo normal sin transportar
            if (playerController.vulnerable)
            {
                this.playerController.vulnerable = false;

                if (this.playerController.vidas-- <= 1)
                {
                    StartCoroutine(FinJuego(collision));
                }
                else
                {
                    StartCoroutine(QuitaVida(collision));

                    //Solo si es un pozo sin fondo de pinchos
                    if (isDeadly)
                    {
                        //Resetea la posicion al check point, aún no hay asi que me lo invento
                        player.transform.position = new Vector3(122.699997f, -1.39999998f, 0f);
                    }
                }
            }            
        }
    }
    IEnumerator QuitaVida(Collision2D collision)
    {
        player.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(3f);
        this.playerController.vulnerable = true;
        player.GetComponent<SpriteRenderer>().color = Color.white; //blanco lo deja con el color normal
    }

    IEnumerator FinJuego(Collision2D collision)
    {
        Camera.main.transform.parent = null; //dejamos a la cámara huérfana
        player.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90)); //se desmaya el player
        //collision.gameObject.GetComponent<PlayerController>().muerto = true;

        player.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1f);
        this.playerController.FinJuego();
        // collision.gameObject.GetComponent<PlayerController>().Perder();

        //coge el game object con el que chocamos y obtiene el script de ese objeto
        //y ejecuta el método que tiene dentro
    }
}
