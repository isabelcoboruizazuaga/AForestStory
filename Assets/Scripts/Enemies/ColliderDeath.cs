using UnityEngine;

public class ColliderDeath : MonoBehaviour
{

    protected PlayerController playerController;
    protected GameObject player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //Obtencion del player y su controller
            player = collision.gameObject;
            this.playerController = player.GetComponent<PlayerController>();

            //Se reproduce el sonido de muerte
            this.playerController.sonidoMuerteEnemigo.Play();

            //Se destruye el enemigo
            this.gameObject.GetComponentInParent<EnemigoController>().DestruyeObjeto();
            Destroy(this.gameObject);

            //Se setea el player como vulnerable por si estaba desactivado
            this.playerController.vulnerable = true;
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
