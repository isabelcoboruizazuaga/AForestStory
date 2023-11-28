using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeController : EnemigoController 
{
    public bool isDeadly = true;
    public GameObject spawnCartel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.player = collision.gameObject;
            this.playerController = player.GetComponent<PlayerController>();            

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
                        player.transform.position = spawnCartel.transform.position;
                    }
                }
            }            
        }
    }

}
