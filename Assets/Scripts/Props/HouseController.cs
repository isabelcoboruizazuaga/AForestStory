using System;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] private bool triggerActive = false;

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("SiguienteNivel!");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().estrellas >= 3)
        {

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
