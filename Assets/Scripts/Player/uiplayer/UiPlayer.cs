using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerPowerUp playerPowerUp;

    public TMP_Text saludTexto;
    public TMP_Text vidasTexto;
    public TMP_Text powerTexto;

    void Update()
    {
        if (playerHealth != null)
        {
            saludTexto.text = "" + playerHealth.vida;
            vidasTexto.text = "" + playerHealth.vidas;
        }

        if (playerPowerUp != null)
        {
            float porcentaje =((float)playerPowerUp.energiaActual /playerPowerUp.energiaNecesaria) * 100f;
 
            powerTexto.text =
                "" +
                Mathf.RoundToInt(porcentaje) +
                "%";
        }
    }
}
