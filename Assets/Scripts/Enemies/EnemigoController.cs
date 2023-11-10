using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemigoController : MonoBehaviour
{

    public GameObject enemigo;
    public float velocidad;
    public Vector3 posicionFin;
    public Vector3 posicionInicial;
    private bool moviendoAFin;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool flip = true; //Marca si hay giro o no basándose en la posición inicial del sprite en unity
    public bool staticFlip=false;

    public int finX, finY;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        posicionFin = new Vector3(posicionInicial.x + finX, posicionInicial.y + finY, posicionInicial.z);
        moviendoAFin = true;


        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }

    private void MoverEnemigo()
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

        //Vector3 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicial;
        //si moviendo a fin es true va a posicionfin y si no a posicion inicial

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

}
