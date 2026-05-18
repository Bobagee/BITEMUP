using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 10;

    public bool stuneado;
    public float tiempoStun = 0.35f;

    public float fuerzaKnockback = 2f;

    public void RecibirGolpe(int dano)
    {
        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
        {
            Debug.Log("Player bloqueo el golpe");
            return;
        }

        vida -= dano;

        AplicarStun();

        AplicarKnockback();

        Debug.Log("Vida Player: " + vida);

        if (vida <= 0)
        {
            Debug.Log("Player derrotado");
        }
    }

    public void AplicarStun()
    {
        stuneado = true;

        CancelInvoke("QuitarStun");
        Invoke("QuitarStun", tiempoStun);
    }

    void QuitarStun()
    {
        stuneado = false;
    }

    void AplicarKnockback()
    {
        GameObject enemy =
            GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            return;
        }

        Vector2 direccion =
            (
                -transform.position -
                enemy.transform.position
            ).normalized;

        Knockback knock =
            GetComponent<Knockback>();

        if (knock != null)
        {
            knock.fuerzaGolpe = fuerzaKnockback;

            knock.AplicarGolpe(direccion);
        }
    }
}