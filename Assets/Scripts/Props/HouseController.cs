using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HouseController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;
    public Image textoNivel;
    public AudioSource sonidoVictoria;
    public int siguienteNivel=1;

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(pasarNivel());
        }
    }
    IEnumerator pasarNivel()
    {
        sonidoVictoria.Play();

        yield return new WaitForSeconds(sonidoVictoria.clip.length);

        if (siguienteNivel != -1)
        {
            SceneManager.LoadScene("Level" + siguienteNivel.ToString());
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
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
