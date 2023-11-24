using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderDeath : MonoBehaviour
{

    protected PlayerController playerController;
    protected GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject;
        this.playerController = player.GetComponent<PlayerController>();

        this.gameObject.GetComponentInParent<EnemigoController>().DestruyeObjeto();
        Destroy(this.gameObject);

        this.playerController.vulnerable = true;
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }
    
}
