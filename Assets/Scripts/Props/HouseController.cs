using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HouseController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public Image textoNivel;
    public AudioSource sonidoVictoria;

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            sonidoVictoria.Play();
            Debug.Log("SiguienteNivel!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().estrellas >= 3)
        {
            textoNivel.gameObject.SetActive(true);
            triggerActive = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }
}
