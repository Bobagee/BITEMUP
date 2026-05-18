using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 10;

    public void RecibirGolpe(int dano)
    {
        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
        {
            Debug.Log("Player bloqueo el golpe");
            return;
        }

        vida -= dano;

        Debug.Log("Vida Player: " + vida);

        if (vida <= 0)
        {
            Debug.Log("Player derrotado");
        }
    }
}