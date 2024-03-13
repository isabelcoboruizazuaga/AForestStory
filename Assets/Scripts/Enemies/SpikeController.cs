using UnityEngine;

public class SpikeController : EnemigoController
{
    public bool isDeadly = true;
    public GameObject spawnCartel =null;

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            player = collision.gameObject;
            this.playerController = player.GetComponent<PlayerController>();

            playerController.sonidoDolor.Play();

            if (isDeadly)
            {
                playerController.vulnerable = true;
                player.transform.position = spawnCartel.transform.position;

            }
            base.OnCollisionEnter2D (collision);
           /* if(playerController.vulnerable)
            {

                this.playerController.vulnerable = false;

                if (this.playerController.vidas-- <= 1)
                {
                    //sonido.Play();
                    StartCoroutine(FinJuego(collision));
                }
                else
                {
                    StartCoroutine(QuitaVida(collision));
                }

                //hud.SetVidasTxt(collision.gameObject.GetComponent<PlayerController>().vidas);

                //Para cambiar las vidas en el hud
                playerController.setVidas();
            }*/
        }       
    }

    protected override void MoverEnemigo()
    {
        //no hace nada porque los spikes no se mueven
    }

    private void MecanicaDanio(Collision2D collision)
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
                player.transform.position = spawnCartel.transform.position;
            }
        }
    }
}

