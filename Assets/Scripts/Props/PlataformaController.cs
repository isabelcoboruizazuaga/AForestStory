using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaController : MonoBehaviour
{
    public int velocidad;
    public Vector3 posicionInicio;
    public Vector3 posicionFinal;
    public int topeX, topeY;
    private bool moviendoAFin;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicio = transform.position;
        posicionFinal = new Vector3(posicionInicio.x + topeX, posicionInicio.y + topeY, posicionInicio.z);
        moviendoAFin = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoverPlataforma();
    }

    private void MoverPlataforma()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFinal : posicionInicio;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        if (transform.position == posicionFinal)
            moviendoAFin = false;
        else if (transform.position == posicionInicio)
            moviendoAFin = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
