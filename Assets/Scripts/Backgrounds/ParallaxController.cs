using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public float efectoParallax;
    private Transform camara;
    private float ultimaPosicionCamaraX;
    private float posicionY;

    // Start is called before the first frame update
    void Start()
    {
        //Obtenemos la posición en y del fondo
        posicionY= transform.position.y;

        camara = Camera.main.transform; //Se asigna la camara principal
        ultimaPosicionCamaraX = camara.position.x; 
    }

    // Update is called once per frame
    void Update()
    {
       //Se edita el movimiento solo en x           
        float movimientoHorizontal= camara.position.x - ultimaPosicionCamaraX;
        float nuevaX=transform.position.x + movimientoHorizontal * efectoParallax;  //se transforma la posición de la imagen multiplicandola por el efecto de cada fondo (asignado en unity)

        transform.position = new Vector3(nuevaX, posicionY, 0); 

        ultimaPosicionCamaraX = camara.position.x;
    }
}
