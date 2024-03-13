using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject hud;
    public Sprite[] imagenes;
    public TextMeshProUGUI textoEstrellas;

    internal void CambioVida(int vidas)
    {
        switch (vidas)
        {
            case 0:
                hud.GetComponent<UnityEngine.UI.Image>().sprite = imagenes[0];
                break;
            case 1:
                hud.GetComponent<UnityEngine.UI.Image>().sprite = imagenes[1];
                break;
            case 2:
                hud.GetComponent<UnityEngine.UI.Image>().sprite = imagenes[2];
                break;
            case 3:
                hud.GetComponent<UnityEngine.UI.Image>().sprite = imagenes[3];
                break;
        }
    }

    internal void SetEstrellas(int estrellas)
    {
        textoEstrellas.text= estrellas.ToString();
    }

}
