using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject enemigo;
    public float velocidad;
    public Vector3 posicionFin;
    public Vector3 posicionInicial;
    private bool moviendoAFin;

    public int finX, finY;



    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        posicionFin = new Vector3(posicionInicial.x + finX, posicionInicial.y + finY, posicionInicial.z);
        moviendoAFin = true;


    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player2>().vulnerable)
        {
            collision.gameObject.GetComponent<Player2>().vulnerable = false;

            //vidas-- <=1 -> prrimero compara y luego resta (por ejemplo 2<1, vidas=1)
            //si es --vidas seria al reves, primero resta y despues compara
            if (collision.gameObject.GetComponent<Player2>().vidas-- <= 1)
            {
                StartCoroutine(FinJuego(collision));


            }
            else
            {
                StartCoroutine(QuitaVida(collision));
                //Invoke("funcion", 2); llama a la funcion "funcion" en 2 segundos, no admite parámetros, es de unity
            }


        }
    }
    IEnumerator QuitaVida(Collision2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;


        yield return new WaitForSeconds(3f);
        collision.gameObject.GetComponent<Player2>().vulnerable = true;
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //blanco lo deja con el color normal
    }

    IEnumerator FinJuego(Collision2D collision)
    {
        Camera.main.transform.parent = null; //dejamos a la cámara huérfana
        collision.gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 90)); //se desmaya el player
        collision.gameObject.GetComponent<Player2>().muerto = true;

        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(1f);
        //collision.gameObject.GetComponent<Player2>().FinJuego(); CAMBIAMOS A PERDER
        collision.gameObject.GetComponent<Player2>().Perder();

        //coge el game object con el que chocamos y obtiene el script de ese objeto
        //y ejecuta el método que tiene dentro
    }
    private void MoverEnemigo()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicial;
        //si moviendo a fin es true va a posicionfin y si no a posicion inicial

        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        //mueve desde la posicion en la que estemos ahota a la posicion que se le indica a esa velocidad

        if (transform.position == posicionFin)
            moviendoAFin = false;
        if (transform.position == posicionInicial)
            moviendoAFin = true;
    }

    public void EnemigoMuerto()
    {
        Destroy(gameObject);
    }
}
